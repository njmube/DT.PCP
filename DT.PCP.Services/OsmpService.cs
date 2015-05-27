using System;
using System.Security.Cryptography;
using DT.PCP.BussinesServices;
using DT.PCP.CommonDomain;
using DT.PCP.Logging;
using DT.PCP.Services.Extensions;
using DT.PCP.Services.Objects;
using DT.PCP.ServicesProxies.BddPayRegisterService;
using DT.PCP.ServicesProxies.BddViolationService;
using DT.PCP.Utils;

namespace DT.PCP.Services
{
    public class OsmpService : IOsmpService
    {
        private readonly ILogger _logger;
        private readonly ITrafficViolationService _trafficViolationService;
        private readonly IViolationService _violationService;
        private readonly IEntityCreator _entityCreator;
        private readonly IEmailService _emailService;
        private readonly IOsmpPaymentService _osmpPaymentService;

        public OsmpService(ILogger _logger, ITrafficViolationService _trafficViolationService, IViolationService _violationService, IEntityCreator _entityCreator, IEmailService _emailService, IOsmpPaymentService _osmpPaymentService)
        {
            this._logger = _logger;
            this._trafficViolationService = _trafficViolationService;
            this._violationService = _violationService;
            this._entityCreator = _entityCreator;
            this._emailService = _emailService;
            this._osmpPaymentService = _osmpPaymentService;
        }

        #region Implementation of IOsmpService

        public AccountInfo GetAccountInfo(string opCode, string account)
        {
            var method = OsmpMethod.GetAccountInfo;

            if (string.IsNullOrWhiteSpace(account))
            {
                _osmpPaymentService.AddOperationInfo(account, OsmpOperationStatus.AccountNotFound, opCode, method);
                return new AccountInfo(account, AccountState.AccountNotFound, 0);
            }
            //TODO
            var violation = _trafficViolationService.GetTrafficViolation(account, string.Empty);

            if (violation == null)
            {
                 _osmpPaymentService.AddOperationInfo(account, OsmpOperationStatus.AccountNotFound, opCode, method);
                return new AccountInfo(account, AccountState.AccountNotFound, 0);
            }

            if (violation.ViolationStatus.ToLower() == "оплачено")
            {
                 _osmpPaymentService.AddOperationInfo(account, OsmpOperationStatus.AccountBlocked, opCode, method);

                return new AccountInfo(account, AccountState.AccountBlocked, 0);
            }
            
             _osmpPaymentService.AddOperationInfo(account, OsmpOperationStatus.Success, opCode, method);

             var commission = _osmpPaymentService.GetCommission();
             var totalPrice = (decimal)violation.FineCost + (decimal)violation.FineCost * (decimal)commission / 100m;
            return new AccountInfo(account, AccountState.Success, totalPrice);
        }

        public ProcessPaymentResult ProcessPayment(string opCode, PaymentInfo payInfo)
        {
            var method = OsmpMethod.ProccessPayment;

            //TODO
            var violation = _trafficViolationService.GetTrafficViolation(payInfo.Account, string.Empty);

            if (violation == null)
            {
                _osmpPaymentService.AddOperationInfo(payInfo.Account, OsmpOperationStatus.AccountBlocked, opCode, method);
                return new ProcessPaymentResult(payInfo, OsmpOperationStatus.AccountNotFound, GenerationInt());
            }

            if (violation.ViolationStatus.ToLower() == "оплачено")
            {
                _osmpPaymentService.AddOperationInfo(payInfo.Account, OsmpOperationStatus.AccountBlocked, opCode, method);

                return new ProcessPaymentResult(payInfo, OsmpOperationStatus.AccountBlocked, GenerationInt());
            }

            var commission = _osmpPaymentService.GetCommission();
            var totalPrice = (decimal)violation.FineCost + (decimal)violation.FineCost * (decimal)commission / 100m;
            if (totalPrice != payInfo.Sum)
            {
                _osmpPaymentService.AddOperationInfo(payInfo.Account, OsmpOperationStatus.PaymentRejected, opCode, method);

                return new ProcessPaymentResult(payInfo, OsmpOperationStatus.PaymentRejected, GenerationInt());
            }

            var payedViolation = _entityCreator.Create<PayedViolation, TrafficViolationData>(violation);

            // TODO брать из PaymentInfo
            payedViolation.Email = "sharok@mail.ru";
            payedViolation.TaxNumber = "11111111111";
            payedViolation.PayMethod = PayMethodAlias.ATM;
            payedViolation.Reference = payInfo.RefID;

            _violationService.RegisterPayInBdd(payedViolation);

            _emailService.SendEmail(payedViolation);
            
            _osmpPaymentService.AddOperationInfo(payInfo.Account, OsmpOperationStatus.Success, opCode, method);
            return new ProcessPaymentResult(
                            new PaymentInfo { Account = payInfo.Account, PaymentDate = payInfo.PaymentDate, Sum = (decimal)violation.FineCost },
                            OsmpOperationStatus.Success, GenerationInt());
        }

        #endregion

        private int GenerationInt()
        {
            var val = new Random().Next(1, 255);

            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            var array = new byte[val];

            rng.GetNonZeroBytes(array);

            return BitConverter.ToInt32(array, 0);

        }
    }
}
