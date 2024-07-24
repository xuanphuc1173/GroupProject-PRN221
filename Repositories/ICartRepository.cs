using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByUserId(string userId);
        Task<CartItem> GetCartItemById(int cartItemId);
        Task AddCart(Cart cart);
        Task AddCartItem(CartItem cartItem);
        Task<bool> DeleteCartItem(int cartItemId);
        Task UpdateCartItem(CartItem cartItem);
        Task<bool> ClearCartByUserId(string userId);
    }
}
