using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; } // ID duy nhất cho CartItem
        public int ProductId { get; set; } // Khóa ngoại liên kết đến Product       
        public int Quantity { get; set; } 
        public int CartId { get; set; } // Khóa ngoại liên kết đến Cart
        public Cart Cart { get; set; } // Đối tượng điều hành để truy cập thông tin Cart
        public Product Product { get; set; } // Đối tượng điều hành để truy cập thông tin Product
    }
}
