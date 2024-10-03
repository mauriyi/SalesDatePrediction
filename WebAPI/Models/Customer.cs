namespace WebAPI.Models
{
    public class Customer
    {
        public long CustId { get; set; }
        public string CustomerName { get; set; }
        public DateTime LastOrderDate { get; set; }
        public DateTime NextPredictedOrder { get; set; }
    }
}
