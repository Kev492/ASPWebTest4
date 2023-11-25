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
            .Where(o => o.CustomerID == customerId)
            .ToList();

        // Pass the list of ORDERLIST models to the view
        return View(customerOrders);
    }
}