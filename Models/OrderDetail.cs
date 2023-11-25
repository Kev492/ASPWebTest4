// Models/OrderDetail.cs
namespace AspWebTest2.Models
{
    public class OrderDetail
    {
        public int OrderID { get; set; }
        public string ProductID { get; set; }
        public int Quantity { get; set; }
        public int SubTotal { get; set; }

        // 추가: 복합 키 설정
        public override int GetHashCode()
        {
            return OrderID.GetHashCode() ^ ProductID.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is OrderDetail))
                return false;

            var other = (OrderDetail)obj;
            return OrderID == other.OrderID && ProductID == other.ProductID;
        }
    }
}