namespace CartDaoApp.Entities;

public class OrderItem(Guid productId, uint quantity)
{
    public Guid Id { get; } = Guid.NewGuid();
    public Guid ProductId { get; set; } = productId;
    public uint Quantity { get; set; } = quantity;
}