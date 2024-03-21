using CartDaoApp.Entities;
using CartDaoApp.Services;
using log4net;
using System.Text.Json;

namespace CartDaoApp;

public class Solution(ProductService productService, CartService cartService, OrderService orderService, ILog log)
{
    public void Run()
    {
        var jsonSerializeUser = File.ReadAllText("user.json");
        var user = JsonSerializer.Deserialize<User>(jsonSerializeUser);

        var cartController = new CartController(productService, cartService, orderService, log);
        var orderController = new OrderController(productService, orderService);

        var options = new JsonSerializerOptions();
        options.Converters.Add(new ProductConverter());

        var jsonSerializeBook1 = File.ReadAllText(@"Products\5f1f21f0-0598-4198-aed6-6c32fd28b46e.json");
        var jsonSerializeBook2 = File.ReadAllText(@"Products\7d6be0c5-cba7-43be-8f9e-bcf546836cb0.json");
        var jsonSerializeBook3 = File.ReadAllText(@"Products\7e78f676-0eec-4535-b40c-15006af5f270.json");
        var jsonSerializeBook4 = File.ReadAllText(@"Products\79cf8f01-1518-40c7-803a-12419edbd157.json");
        var jsonSerializeBook5 = File.ReadAllText(@"Products\fec18aa6-2404-4b5e-bc27-4298656a051e.json");

        var book1 = JsonSerializer.Deserialize<Book>(jsonSerializeBook1, options);
        var book2 = JsonSerializer.Deserialize<Book>(jsonSerializeBook2, options);
        var book3 = JsonSerializer.Deserialize<Book>(jsonSerializeBook3, options);
        var book4 = JsonSerializer.Deserialize<Book>(jsonSerializeBook4, options);
        var book5 = JsonSerializer.Deserialize<Book>(jsonSerializeBook5, options);

        //cartController.ClearCartByUserId(user.Id);

        //cartController.AddProduct(book3.Id, user.Id);
        //cartController.AddProduct(book3.Id, user.Id);
        //cartController.AddProduct(book5.Id, user.Id);

        cartController.CreateOrder(user.Id);
    }
}