using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using DT.PCP.CommonDomain;
using DT.PCP.DataAccess;
using DT.PCP.Domain;
using DT.PCP.Epay;
using DT.PCP.Logging;

namespace DT.PCP.BussinesServices.Impl
{
    public class EpayService : IEpayService
    {
        private readonly ILogger _logger;
        private readonly IRepository _repository;
        private readonly IViolationService _violationService;
        private readonly IEmailService _emailService;

        public EpayService(ILogger _logger, IRepository _repository, IViolationService _violationService , IEmailService _emailService)
        {
            this._logger = _logger;
            this._repository = _repository;
            this._violationService = _violationService;
            this._emailService = _emailService;
        }

        /// <summary>
        /// Проверяет и подтверждает платеж
        /// </summary>
        /// <param name="response">Ответ от банка в XML формате</param>
        /// <returns></returns>
        public bool FixPayment(string response)
        {
            var epayResponse = ParseResponse(response);

            if (epayResponse == null || epayResponse.ResponseCode != "00")
            {
                _logger.Debug("epayresponse = null or response code != 00");
                return false;
            }

            var order = _repository.Query<Order>().FirstOrDefault(o => o.EpayOrderId == epayResponse.EpayOrderId);
           
            if (order == null)
            {
                _logger.Debug("order = null");
                return false;
            }

            var approveOrderQuery = EPayKkb.BuildApproveOrder(epayResponse.EpayOrderId, epayResponse.CostFull.ToString(),
                                                              epayResponse.ApprovalCode, epayResponse.Reference);

            var approved = ApprovePayment(approveOrderQuery);
            if (!approved)
            {
                _logger.Debug("pay not approved: " + approveOrderQuery );
                return false;
            }

            order.IsPayed = true;
            order.ReferenceCode = epayResponse.Reference;
            order.ApprovalCode = epayResponse.ApprovalCode;
            order.PaymentDate = epayResponse.PaymentDate;

            order.IsConfirmedInBdd = _violationService.RegisterPayInBdd(order);
            _logger.Debug("is confirmed in bdd:" + order.IsConfirmedInBdd);
            _repository.Update(order);
            _repository.SaveChanges();

            //_emailService.SendEmail(order);
            return true;
        }

       
        
        /// <summary>
        /// Парсинг ответа от банка
        /// </summary>
        /// <param name="response">Ответ в формате XML</param>
        public EpayResponse ParseResponse(string response)
        {
            var bankSection = XDocument.Parse(response).Element("document").Element("bank").ToString();
            var signSection = XDocument.Parse(response).Element("document").Element("bank_sign").Value;
            //var isGood = EPayKkb.Verify(bankSection, signSection);

            return ParseBankSection(bankSection);
        }

        /// <summary>
        /// Возвращает процент комиссии при оплате картой
        /// </summary>
        /// <returns>Процент комиссии</returns>
        public double GetCommission()
        {
            //var commission = _repository.Query<Commission>().FirstOrDefault(c => c.PayWay == PayWay.Card);
            //if (commission == null)
            //    throw new Exception("Комиссия не найдена");

            return 2;
        }
        

        /// <summary>
        /// Парсинг секции банка в xml документе
        /// </summary>
        /// <param name="bankSection">XML секция банка</param>
        /// <returns></returns>
        private EpayResponse ParseBankSection(string bankSection)
        {
            if (IsExitsError(bankSection))
                return null;

            var doc = XDocument.Parse(bankSection);
            var results = doc.Descendants("results").FirstOrDefault();
            var code = results.Element("payment").Attribute("response_code").Value;

            var orderId = doc.Descendants("order").FirstOrDefault().Attribute("order_id").Value;
            var time = (DateTime)results.Attribute("timestamp");
            var approvalCode = results.Element("payment").Attribute("approval_code").Value;

            var reference = results.Element("payment").Attribute("reference").Value;
            var amount = results.Element("payment").Attribute("amount").Value;

            return new EpayResponse
                {
                    ResponseCode = code,
                    EpayOrderId = orderId,
                    PaymentDate = time,
                    ApprovalCode = approvalCode,
                    Reference = reference,
                    CostFull = decimal.Parse(amount, CultureInfo.InvariantCulture)
                };
        }

        /// <summary>
        /// Есть ли ошибки в ответе
        /// </summary>
        /// <param name="bankSection">Секция банка в XML документе</param>
        /// <returns></returns>
        private bool IsExitsError(string bankSection)
        {
            var doc = XDocument.Parse(bankSection);
            var errors = doc.Descendants("error").FirstOrDefault();

            if (errors == null)
                return false;

            var errorCode = errors.Attribute("code").Value;
            var orderId = doc.Element("response").Attribute("order_id").Value;

            _logger.Debug(string.Format("Epay error: \n ErrorCode: {0} \n OrderId: {1}", errorCode, orderId));
            return true;

        }

        /// <summary>
        /// Подтверждение платежа в epay
        /// </summary>
        /// <param name="approveOrderQuery">Запрос подтверждения</param>
        /// <returns></returns>
        private bool ApprovePayment(string approveOrderQuery)
        {
            string epayUrl = string.Format("{0}?{1}", ConfigurationManager.AppSettings["EpayRemoteControlUrl"], approveOrderQuery);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(epayUrl);
            request.Method = "GET";
            HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse();
            var responseStream = webResponse.GetResponseStream();
            StreamReader answer = new StreamReader(responseStream);
            var stringAnswer = answer.ReadToEnd();

            var bankElement = XDocument.Parse(stringAnswer.Trim()).Descendants("bank").FirstOrDefault();

            if (bankElement == null)
                return false;

            var code = bankElement.Element("response").Attribute("code").Value;
            if (code == "00")
                return true;

            return false;
        }
    }
}
