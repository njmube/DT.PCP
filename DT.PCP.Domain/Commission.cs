using DT.PCP.CommonDomain;

namespace DT.PCP.Domain
{
    /// <summary>
    /// Комиссия при оплате
    /// </summary>
   public class Commission:Entity
    {
       public PayWay PayWay { get; set; }
       public double Procent { get; set; }
    }
}
