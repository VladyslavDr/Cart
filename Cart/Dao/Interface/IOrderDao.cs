using CartDaoApp.Entities;

namespace CartDaoApp.Dao.Interface;

public interface IOrderDao
{
    void CreateOrder(Order order);
    Dictionary<Guid, Order>.ValueCollection GetOrders();
}