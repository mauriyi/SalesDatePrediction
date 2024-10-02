namespace WebAPI.Models
{
    public class NewOrderDto
    {
        public int EmpId { get; set; }
        public int CustId { get; set; }
        public int ShipperId { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public decimal Freight { get; set; }
        public string ShipCountry { get; set; }

        // detalle de la orden
        public NewOrderDetailDto OrderDetails { get; set; }
    }
}
