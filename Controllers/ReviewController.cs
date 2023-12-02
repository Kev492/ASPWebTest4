using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspWebTest2.Models;
using System.Linq;

public class ReviewController : Controller
{
    private readonly ApplicationDbContext _context;

    public ReviewController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(string productId)
    {
        // Filter reviews based on the productId parameter
        var reviews = _context.REVIEW
            .Where(r => r.ProductID == productId)
            .ToList();

        return View("Review",reviews);
    }
}