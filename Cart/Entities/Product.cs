namespace CartDaoApp.Entities
{
    public abstract class Product
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
    }
}
