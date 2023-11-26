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

            var transportViewModel = new TransportViewModel
            {
                Order = order,
                CustomerAddress = customerAddress,
                DistributionHub = distributionHub
            };

            return View("/Views/Transport/Transport.cshtml", transportViewModel);
        }
    }
}