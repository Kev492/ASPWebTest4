namespace AspWebTest2.Models
{
    public class Address
    {
        public string LocalAddress { get; set; }
        public string DetailedAddress { get; set; }
        public string CustomerID { get; set; }
    }
    public class TransportViewModel
    {
        public ORDERLIST Order { get; set; }
        public Address CustomerAddress { get; set; }
        public Hub DistributionHub { get; set; }
        public Driver DriverInfo { get; set; }
        public string CityName { get; set; }
        public Hub FirstDistributionHub { get; set; } 
        public Driver FirstDistributionDriverInfo { get; set; }
    }
}