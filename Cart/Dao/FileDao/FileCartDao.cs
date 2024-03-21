using System.Text.Json;
using CartDaoApp.Dao.Interface;
using CartDaoApp.Entities;

namespace CartDaoApp.Dao.FileDao;

public class FileCartDao : ICartDao
{
    private const string DirectoryPath = "Carts";

    public Cart? GetCartByUserId(Guid id)
    {
        string filePath = $@"{DirectoryPath}\{id}.json";
        if (File.Exists(filePath))
        {
            string jsonSerializeCart = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Cart>(jsonSerializeCart);
        }
        return null;
    }

    private Cart? GetCartByCartId(Guid cartId)
    {
        var files = Directory.GetFiles(DirectoryPath, "*.json");

        foreach (var file in files)
        {
            var cart = JsonSerializer.Deserialize<Cart>(File.ReadAllText(file));

            if (cart.Id == cartId)
            {
                return cart;
            }
        }

        return null;
    }

    public void CreateCartByUserId(Guid userId)
    {
        var cart = new Cart
        {
            Id = Guid.NewGuid(),
            UserId = userId,
        };

        SaveCart(cart);
    }

    public void UpdateTotalPrice(Guid cartId, decimal newTotalPrice)
    {
        var cart = GetCartByCartId(cartId);

        cart.TotalPrice = newTotalPrice;
        SaveCart(cart);
    }

    private void SaveCart(Cart cart)
    {
        var filePath = $@"{DirectoryPath}\{cart.UserId}.json";

        var jsonSerializeCart = JsonSerializer.Serialize(cart);
        File.WriteAllText(filePath, jsonSerializeCart);
    }
}