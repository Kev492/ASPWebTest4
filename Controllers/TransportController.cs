using Microsoft.AspNetCore.Mvc;
using AspWebTest2.Models;

namespace AspWebTest2.Controllers
{
    public class TransportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransportController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int orderId)
        {
            var order = _context.ORDERLIST.FirstOrDefault(o => o.OrderID == orderId);

            var customerAddress = _context.CustomerAddress.FirstOrDefault(a => a.CustomerID == order.CustomerID);
            var distributionHub = _context.DistributionHUB
                .FirstOrDefault(hub => hub.AllocatedArea == customerAddress.LocalAddress);
            var driverInfo = _context.DriverInformation
                .FirstOrDefault(driver => driver.AffiliatedCompany == distributionHub.DistributionName);
            
            var cityName = customerAddress.LocalAddress.Substring(0, 2);
            var firstDistributionHub = _context.DistributionHUB
                .FirstOrDefault(hub => hub.AllocatedArea == cityName);
            var firstDistributionDriverInfo = _context.DriverInformation
                .FirstOrDefault(driver => driver.AffiliatedCompany == firstDistributionHub.DistributionName);

            var transportViewModel = new TransportViewModel
            {
                Order = order,
                CustomerAddress = customerAddress,
                DistributionHub = distributionHub,
                DriverInfo = driverInfo,
                CityName = cityName,
                FirstDistributionHub = firstDistributionHub, 
                FirstDistributionDriverInfo = firstDistributionDriverInfo
            };

            return View("/Views/Transport/Transport.cshtml", transportViewModel);
        }
    }
}