// Models/OrderList.cs
namespace AspWebTest2.Models
{
    public class ORDERLIST
    {
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public Customer Customer { get; set; }
        public int TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
    }

    public class CartItem
    {
        public string ProductID { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }    
}