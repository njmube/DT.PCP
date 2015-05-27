using System;
using System.Collections.ObjectModel;
using System.Linq;
using DT.PCP.DataAccess;
using DT.PCP.Domain;

namespace DT.PCP.BussinesServices.Impl
{
    public class OrderService : IOrderService
    {
        private readonly IViolationService _violationService;
        private readonly IRepository _repository;

        public OrderService(IViolationService _violationService, IRepository _repository)
        {
            this._violationService = _violationService;
            this._repository = _repository;
        }

        #region Implementation of IOrderService

        public void CreateOrder(Order order)
        {
            string orderId = GetOrderId();
            order.EpayOrderId = orderId;
            order.OrderDate = DateTime.Now;
            // ХАК: ef создает нового пользователя почему то.
            var user = _repository.Query<User>().First(u => u.Id == order.User.Id);
            order.User = user;
            //ХАК

            _repository.Save(order);
            _repository.SaveChanges();
        }

        private string GetOrderId()
        {
            var lastOrderId = _repository.Query<Order>().Max(o => o.EpayOrderId);
            if (lastOrderId == null)
                return "1000000000";

            long nextOrderId;
            if(!long.TryParse(lastOrderId, out nextOrderId))
                throw new Exception("Не удалось получить номер заказа");

            nextOrderId++;
            return nextOrderId.ToString();

        }

        public Order CreateTransientOrder(string orderNumbers, User currentUser)
        {
            Order order = new Order
                {
                    EpayOrderId = Guid.NewGuid().ToString()
                };

            order.Details = new Collection<OrderDetail>();
            foreach (var orderNumber in orderNumbers.Split(','))
            {
                var violationInfo = _violationService.GetViolationsByOrder(orderNumber, currentUser.CarNumber);
               
                order.OrderDate = new DateTime();
                order.Details.Add(new OrderDetail
                    {
                        OrderNumber = violationInfo.OrderNumber,
                        Cost = (decimal)violationInfo.FineCost,
                    });
            }

            order.User = currentUser;

            return order;
        }

        public decimal CalculateCost(Order order)
        {
            return order.Details.Sum(d => d.Cost);
        }

        public decimal CalculateCommission(decimal price, double commission)
        {
            return price + price*(decimal) commission/100m;
        }

        public decimal CalculateCommission(Order order, double commission)
        {
            var price = order.Details.Sum(d => d.Cost);
            return CalculateCommission(price, commission);
        }

        #endregion
    }
}
