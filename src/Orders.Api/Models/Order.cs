namespace Orders.Api.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Customer { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime CreatedUtc { get; set; }
    }
}
