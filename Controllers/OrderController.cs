using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspWebTest2.Models;
using System.Linq;

public class OrderController : Controller
{
    private readonly ApplicationDbContext _context;

    public OrderController(ApplicationDbContext context)
    {
        _context = context;
    }

     public IActionResult OrderInfo(string customerId)
    {
        // Retrieve order information from the database using only ORDERLIST table
        var customerOrders = _context.ORDERLIST
            .Where(o => o.CustomerID == customerId && o.Refund != true)
            .OrderByDescending(o => o.OrderDate)
            .ToList();
        
         // Create a list to store OrderDetails for each ORDERLIST item
        var orderDetailsList = new List<List<OrderDetail>>();

        // Retrieve order details from the database using ORDERDETAILS table
        foreach (var order in customerOrders)
        {
            var details = _context.ORDERDETAILS
                .Where(d => d.OrderID == order.OrderID)
                .ToList();

            orderDetailsList.Add(details);
        }

        // Pass both ORDERLIST and ORDERDETAILS lists to the view
        ViewData["CustomerOrders"] = customerOrders;
        ViewData["OrderDetailsList"] = orderDetailsList;
        // Pass the list of ORDERLIST models to the view
        return View();
    }
}