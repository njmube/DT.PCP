using System.Collections.Generic;
using UniSender;

namespace DT.PCP.BussinesServices.Impl
{
    public  class SmsService:ISmsService
    {
        #region Implementation of ISmsService

        public void SendSms(string phoneNumber, string text)
        {
            dynamic client = new Client("5fftxjqc33fiuw37j4o4gpznwzgmtj8qtqp9bjxe", "ru");
            Dictionary<string, object> args = new Dictionary<string, object>();
            args.Add("phone", phoneNumber);
            args.Add("sender", "naoplatu.kz");
            args.Add("text", text);
            var test = client.sendSms(args);
        }

        #endregion
    }
}
