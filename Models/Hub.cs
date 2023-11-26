namespace AspWebTest2.Models
{
    public class Hub
    {
        public string DistributionName { get; set; }
        public int DistributionClassification { get; set; }
        public string AllocatedArea { get; set; }
    }
    public class Driver
    {
        public string CargoDriverID { get; set; }
        public string DriverName { get; set; }
        public string DriverPhone { get; set; }
        public string AffiliatedCompany { get; set; }
    }
}