using AspWebTest2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AspWebTest2.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ApplicationDbContext context, ILogger<ProductController> logger)
        {
            _context = context;
            _logger = logger;
        }


        public IActionResult Index(string customerId)
        {
            var customer = _context.CUSTOMER.FirstOrDefault(c => c.CustomerID == customerId);

            if (customer != null)
            {
                var products = _context.PRODUCT.ToList();
                var model = Tuple.Create(products, customer);

                return View("~/Views/Product/Products.cshtml", model);
            }
            else
            {
                // Handle the case where the customer is not found
                return View("~/Views/Product/CustomerNotFound.cshtml");
            }
        }
        [HttpPost]
        public IActionResult AddToTotal(List<CartItem> cart)
        {
           // Get existing cart list from the session or create a new one
            var cartList = HttpContext.Session.GetObject<List<CartItem>>("Cart") ?? new List<CartItem>();

            // Add the current cart to the list
            cartList.AddRange(cart);

            // Store the updated cart list in the session
            HttpContext.Session.SetObject("Cart", cartList);

            // You can also perform additional processing here if needed

            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Buy(int totalAmount, string customerId)
        {
            try
            {
                if (string.IsNullOrEmpty(customerId))
                {
                    return BadRequest("Invalid customerId");
                }

                int orderId = GenerateOrderId(); // Use a larger data type for OrderID

                // Create a new ORDERLIST object
                var order = new ORDERLIST
                {
                    OrderID = orderId,
                    CustomerID = customerId,
                    TotalAmount = totalAmount,
                    OrderDate = DateTime.UtcNow
                };

                // Add the new order to the context
                _context.ORDERLIST.Add(order);

                // Save changes to the database
                _context.SaveChanges();

                // Insert cart details into ORDERDETAILS table
                var cartList = HttpContext.Session.GetObject<List<CartItem>>("Cart");
                if (cartList != null && cartList.Any())
                {
                    // HashSet을 사용하여 중복된 OrderDetail 방지
                    var uniqueOrderDetails = new HashSet<OrderDetail>();

                    foreach (var cartItem in cartList)
                    {
                        var orderDetail = new OrderDetail
                        {
                            OrderID = orderId,
                            ProductID = cartItem.ProductID,
                            Quantity = cartItem.Quantity,
                            SubTotal = cartItem.Quantity * cartItem.Price
                        };
                        // 중복을 방지하기 위해 HashSet에 추가
                        uniqueOrderDetails.Add(orderDetail);
                        //_context.ORDERDETAILS.Add(orderDetail);
                    }
                    // Add unique order details to the context
                    foreach (var uniqueOrderDetail in uniqueOrderDetails)
                    {
                        _context.ORDERDETAILS.Add(uniqueOrderDetail);
                    }

                    // Save changes to the database
                    _context.SaveChanges();

                    // Clear the cart after successful purchase
                    HttpContext.Session.Remove("Cart");
                }

                // Return the OrderID to the client
                return Json(new { orderId = order.OrderID });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during purchase");

                // Provide a more detailed error message to the client
                return BadRequest($"An error occurred during purchase. Details: {ex.Message}");
            }
        }

        private int GenerateOrderId()
        {
            // Use a larger data type for OrderID (long) and include milliseconds for additional uniqueness
            return (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

    }

    public static class SessionExtensions
    {
        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
    }
}