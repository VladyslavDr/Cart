using CartDaoApp.Dao.Interface;
using CartDaoApp.Entities;
using Newtonsoft.Json;

namespace CartDaoApp.Services;

public class OrderService(IOrderDao orderDao)
{
    public void CreateOrder(List<CartItem> cartItems, Guid userId, decimal totalPriceCart)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            DateTime = DateTime.Now,
            OrderItems = []
        };

        foreach (var orderItem in cartItems
                     .Select(cartItem => new OrderItem(cartItem.ProductId, cartItem.Quantity)))
        {
            order.OrderItems.Add(orderItem);
        }
        order.TotalPrice = totalPriceCart;

        orderDao.CreateOrder(order);
    }

    public IEnumerable<Order> GetAllOrdersByUserId(Guid userId)
    {
        var allOrders = orderDao.GetOrders();
        IEnumerable<Order> ordersByUserId = allOrders.Where(order => order.UserId == userId);
        return ordersByUserId;
    }
}