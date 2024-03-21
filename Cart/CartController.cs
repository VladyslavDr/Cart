using CartDaoApp.Entities;
using CartDaoApp.Services;
using log4net;

namespace CartDaoApp;

public class CartController(ProductService productService, CartService cartService, OrderService orderService, ILog log)
{
    public void AddProduct(Guid productId, Guid userId)
    {
        var product = productService.GetProductById(productId);
        var cart = cartService.GetCartByUserId(userId);

        cartService.AddProduct(cart, product);
        cartService.AddProductPriceToTotal(cart, product.Price);

        var message = $"Product: \"{product.Title}\" was added to the cart";
        log.Info(message);
    }

    public void RemoveProduct(Guid productId, Guid userId)
    {
        var product = productService.GetProductById(productId);
        var cart = cartService.GetCartByUserId(userId);
        var cartItems = cartService.GetCartItems(cart);

        var cartItem = cartItems.FirstOrDefault(cartItem => cartItem.ProductId == productId);

        if (cartItem != null)
        {
            cartService.RemoveProduct(cart, product);
            cartService.SubtractProductPriceFromTotal(cart, product.Price * cartItem.Quantity);
            var message = $"Product {product.Title} was removed from the cart";
            log.Info(message);
        }
    }

    public void CreateOrder(Guid userId)
    {
        var cart = cartService.GetCartByUserId(userId);
        var cartItems = cartService.GetCartItems(cart);
        var totalPrice = cart.TotalPrice;

        orderService.CreateOrder(cartItems, userId, totalPrice);

        cartService.ClearCart(cart);
    }

    public void ClearCartByUserId(Guid userId)
    {
        var cart = cartService.GetCartByUserId(userId);
        cartService.ClearCart(cart);
    }

    public void ViewCart(Guid userId)
    {
        var cart = cartService.GetCartByUserId(userId);
        var cartItems = cartService.GetCartItems(cart);

        if (cartItems.Count == 0)
        {
            Console.WriteLine("[CART]");
            Console.WriteLine("Cart is empty;");
            Console.WriteLine();
            return;
        }

        Console.WriteLine("[CART]");
        foreach (var (cartItem, ordinal) in cartItems.Select((cartItem, index) => (cartItem, index + 1)))
        {
            var productId = cartItem.ProductId;
            var product = productService.GetProductById(productId);

            Console.WriteLine($"{ordinal}) \"{product.Title}\" : {product.Price}UAH : {cartItem.Quantity}pcs.");
        }

        Console.WriteLine($"Total: {cart.TotalPrice}");
        Console.WriteLine();
    }
}