using Microsoft.AspNetCore.Mvc;
using AspWebTest2.Models;

namespace AspWebTest2.Controllers
{
    public class TransportController : Controller
    {
        public IActionResult Index(int orderId)
        { 
            var order = new ORDERLIST { OrderID = orderId};
            // 여기서는 단순히 orderId를 Transport.cshtml에 전달합니다.
            return View("/Views/Transport/Transport.cshtml",order);
        }
    }
}