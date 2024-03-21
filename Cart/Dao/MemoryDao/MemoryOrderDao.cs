using CartDaoApp.Dao.Interface;
using CartDaoApp.Entities;
using System;

namespace CartDaoApp.Dao.MemoryDao;

public class MemoryOrderDao : IOrderDao
{
    private readonly Dictionary<Guid, Order> _orders = [];

    public void CreateOrder(Order order) => _orders.Add(order.Id, order);

    public Dictionary<Guid, Order>.ValueCollection GetOrders() => _orders.Values;
}