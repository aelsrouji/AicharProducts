using Services.OrderAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.OrderAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public Task<bool> AddOrder(OrderHeader orderHeader)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrderPaymentStatus(int orderHeaderId, bool paid)
        {
            throw new NotImplementedException();
        }
    }
}
