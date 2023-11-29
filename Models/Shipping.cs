
namespace AspWebTest2.Models
{
    public class FirstFreightShipping
    {
        public int TransportNumber { get; set; }
        public string CargoDriverID { get; set; }
        public string LocalAddress { get; set; }
        public int OrderID { get; set; }
        public string DistributionName { get; set; }

        // Navigation property for the ORDERLIST table
        public ORDERLIST Order { get; set; }
    }

    public class SecondFreightShipping
    {
        public int TransportNumber { get; set; }
        public string CargoDriverID { get; set; }
        public string LocalAddress { get; set; }
        public string DetailedAddress { get; set; }
        public int OrderID { get; set; }
        public string DistributionName { get; set; }

        // Navigation property for the ORDERLIST table
        public ORDERLIST Order { get; set; }
    }
}