using System.Runtime.Serialization;

namespace DT.PCP.Services.Objects
{
    [DataContract]
    public class AccountInfo
    {
        public AccountInfo(string account, AccountState state, decimal sum)
        {
            Account = account;
            State = state;
            Sum = sum;
        }

        [DataMember]
        public string Account { get; private set; }
        [DataMember]
        public AccountState State { get; private set; }
        [DataMember]
        public decimal Sum { get; set; }

        
    }
}
