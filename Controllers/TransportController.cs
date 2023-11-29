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
            var SeconddistributionHub = _context.DistributionHUB
                .FirstOrDefault(hub => hub.AllocatedArea == customerAddress.LocalAddress);
            var seconddriverInfo = _context.DriverInformation
                .FirstOrDefault(driver => driver.AffiliatedCompany == SeconddistributionHub.DistributionName);
            
            var cityName = customerAddress.LocalAddress.Substring(0, 2);
            var firstDistributionHub = _context.DistributionHUB
                .FirstOrDefault(hub => hub.AllocatedArea == cityName);
            var firstDistributionDriverInfo = _context.DriverInformation
                .FirstOrDefault(driver => driver.AffiliatedCompany == firstDistributionHub.DistributionName);

            var transportViewModel = new TransportViewModel
            {
                Order = order,
                CustomerAddress = customerAddress,
                SecondDistributionHub = SeconddistributionHub,
                SecondDriverInfo = seconddriverInfo,
                CityName = cityName,
                FirstDistributionHub = firstDistributionHub, 
                FirstDistributionDriverInfo = firstDistributionDriverInfo
            };

            // Add data to FirstFreightShipping table
            var firstFreightShipping = new FirstFreightShipping
            {
                TransportNumber = transportViewModel.Order.OrderID + 100000,
                CargoDriverID = transportViewModel.FirstDistributionDriverInfo.CargoDriverID,
                LocalAddress = transportViewModel.CustomerAddress.LocalAddress,
                OrderID = transportViewModel.Order.OrderID,
                DistributionName = transportViewModel.FirstDistributionHub.DistributionName
            };

            _context.Add(firstFreightShipping);

            // Add data to SecondFreightShipping table
            var secondFreightShipping = new SecondFreightShipping
            {
                TransportNumber = transportViewModel.Order.OrderID + 100000,
                CargoDriverID = transportViewModel.SecondDriverInfo.CargoDriverID,
                LocalAddress = transportViewModel.CustomerAddress.LocalAddress,
                DetailedAddress = transportViewModel.CustomerAddress.DetailedAddress,
                OrderID = transportViewModel.Order.OrderID,
                DistributionName = transportViewModel.SecondDistributionHub.DistributionName
            };

            _context.Add(secondFreightShipping);

            _context.SaveChanges(); // Save changes to the database

            return View("/Views/Transport/Transport.cshtml", transportViewModel);
        }
    }
}