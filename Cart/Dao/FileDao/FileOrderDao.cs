using CartDaoApp.Dao.Interface;
using CartDaoApp.Entities;
using System.Text.Json;

namespace CartDaoApp.Dao.FileDao;

public class FileOrderDao : IOrderDao
{
    private const string DirectoryPath = "Orders";

    public void CreateOrder(Order order)
    {
        SaveOrder(order);
    }

    // todo що за гівно! 
    public Dictionary<Guid, Order>.ValueCollection GetOrders()
    {
        var orders = new Dictionary<Guid, Order>();

        foreach (var filePath in Directory.EnumerateFiles(DirectoryPath, "*.json"))
        {
            var json = File.ReadAllText(filePath);
            var order = JsonSerializer.Deserialize<Order>(json);

            orders.Add(order.Id, order);
        }

        return orders.Values;
    }

    private void SaveOrder(Order order)
    {
        var filePath = $@"{DirectoryPath}\{order.UserId}]({order.DateTime:dd MMMM yyyy, HH-mm}).json";

        var jsonSerializeCart = JsonSerializer.Serialize(order);
        File.WriteAllText(filePath, jsonSerializeCart);
    }
}