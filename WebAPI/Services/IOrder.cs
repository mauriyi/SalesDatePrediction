﻿using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IOrder
    {
        IEnumerable<Order> GetClientOrders(int customerId);
    }
}
