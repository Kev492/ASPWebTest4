using AspWebTest2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AspWebTest2.Controllers
{
    public class MembershipController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MembershipController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string customerId)
        {
            var membershipInfo = _context.MEMBERSHIP
                .Where(m => m.CustomerID == customerId)
                .FirstOrDefault();

            ViewBag.CustomerId = customerId;
            return View("Membership", membershipInfo);
        }
    }
}