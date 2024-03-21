using System.Text.Json;
using CartDaoApp.Dao.Interface;
using CartDaoApp.Entities;

namespace CartDaoApp.Dao.FileDao;

public class FileCartItemDao : ICartItemDao
{
    private const string DirectoryPath = "CartItems";

    public List<CartItem> GetAllCartItemsByCartId(Guid cartId)
    {
        var files = Directory.GetFiles(DirectoryPath, "*.json");
        var cartItems = new List<CartItem>();

        foreach (var file in files)
        {
            var cartItem = JsonSerializer.Deserialize<CartItem>(File.ReadAllText(file));
            cartItems.Add(cartItem);
        }

        return cartItems;
    }

    public void CreateCartItem(CartItem cartItem) => SaveCartItem(cartItem);

    public void RemoveCartItem(Guid cartItemId)
    {
        var filePath = $@"{DirectoryPath}\{cartItemId}.json";

        File.Delete(filePath);
    }

    public void ChangeQuantityByItemId(Guid cartItemId, uint quantity)
    {
        var file = $@"{DirectoryPath}\{cartItemId}.json";
        var cartItem = JsonSerializer.Deserialize<CartItem>(File.ReadAllText(file));

        cartItem.Quantity = quantity;

        SaveCartItem(cartItem);
    }

    private void SaveCartItem(CartItem cartItem)
    {
        var filePath = $@"{DirectoryPath}\{cartItem.Id}.json";

        var jsonSerializeCartItem = JsonSerializer.Serialize(cartItem);
        File.WriteAllText(filePath, jsonSerializeCartItem);
    }
}