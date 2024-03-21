using CartDaoApp.Services;

namespace CartDaoApp;

public class OrderController(ProductService productService, OrderService orderService)
{
    public void ViewAllOrders(Guid userId)
    {
        var orders = orderService.GetAllOrdersByUserId(userId);

        foreach (var (order, orderOrdinal) in orders.Select((order, index) => (order, index + 1)))
        {
            Console.WriteLine($"Order {orderOrdinal}: {order.Id}");
            Console.WriteLine($"Time: {order.DateTime}");

            foreach (var (orderItem, orderItemOrdinal) in order.OrderItems.Select((orderItem, index) => (orderItem, index + 1)))
            {
                var productId = orderItem.ProductId;
                var product = productService.GetProductById(productId);

                Console.WriteLine($"{orderItemOrdinal}) \"{product.Title}\" : {product.Price}UAH : {orderItem.Quantity}pcs.");
            }

            Console.WriteLine($"Total: {order.TotalPrice}");
            Console.WriteLine();
        }
    }
}