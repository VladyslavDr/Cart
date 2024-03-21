namespace CartDaoApp.Entities;

//public class Cart(Guid userId)
//{
//    public Guid Id { get; } = Guid.NewGuid();
//    public Guid UserId => userId;
//    public List<CartItem> CartItems { get; } = [];
//    public decimal TotalPrice { get; set; }
//}

public class Cart
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal TotalPrice { get; set; }
}