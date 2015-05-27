using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DT.PCP.CommonDomain;
using DT.PCP.DataAccess;
using DT.PCP.Domain;

namespace DT.PCP.BussinesServices.Impl
{
    public class OsmpPaymentService : IOsmpPaymentService
    {
        private readonly IRepository _repository;

        public OsmpPaymentService(IRepository _repository)
        {
            this._repository = _repository;
        }

        #region Implementation of IOsmpPaymentService

        public void AddOperationInfo(string account, OsmpOperationStatus status, string opCode, OsmpMethod method)
        {
            var stringStatus = GetStringStatus<OsmpOperationStatus>(status);
            var stringMethod = GetStringStatus<OsmpMethod>(method);
            var osmpPayment = new OsmpPayment
                {
                    OrderNumber = account,
                    OperationCode = opCode,
                    OperationName = stringMethod,
                    Status = stringStatus,
                    OperationDate = DateTime.Now
                };

            _repository.Save(osmpPayment);
            _repository.SaveChanges();
        }

        public double GetCommission()
        {
            var commission = _repository.Query<Commission>().FirstOrDefault(c => c.PayWay == PayWay.Terminal);
            if (commission == null)
                throw new Exception("Комиссия не найдена");

            return commission.Procent;
        }

        #endregion

        public static string GetStringStatus<T>(object value)
        {
            return Enum.GetName(typeof(T), value);
        }
    }
}
