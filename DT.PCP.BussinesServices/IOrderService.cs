using DT.PCP.CommonDomain;
using DT.PCP.Domain;


namespace DT.PCP.BussinesServices
{
    public interface IOrderService
    {
        void CreateOrder(Order order);
        Order CreateTransientOrder(string orderNumbers, User currentUser);
        decimal CalculateCost(Order order);
        decimal CalculateCommission(decimal price, double commission);
        decimal CalculateCommission(Order order, double commission);
        
    }
}
