using AspWebTest2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AspWebTest2.Controllers
{
    public class InquiryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InquiryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string customerId)
        {
            var inquiryList = _context.INQUIRY
                .Where(i => i.CustomerID == customerId)
                .ToList();

            ViewBag.CustomerId = customerId;
            return View("Inquiry", inquiryList);
        }
    }
}