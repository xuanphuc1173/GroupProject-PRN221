using BusinessObject;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CartRepository : ICartRepository
    {
        public async Task<Cart> GetCartByUserId(string userId)
        {
            return await CartDAO.Instance.GetCartByUserId(userId);
        }

        public async Task AddCart(Cart cart)
        {
            await CartDAO.Instance.AddCart(cart);
        }

        public async Task AddCartItem(CartItem cartItem)
        {
            await CartDAO.Instance.AddCartItem(cartItem);
        }
        public async Task<bool> DeleteCartItem(int cartId)
        {
            return await CartDAO.Instance.DeleteCartItem(cartId);
        }
        public async Task UpdateCartItem(CartItem cartItem)
        {
            await CartDAO.Instance.UpdateCartItem(cartItem);
        }
        public async Task<bool> ClearCartByUserId(string userId)
        {
            return await CartDAO.Instance.ClearCartByUserId(userId);
        }
        public async Task<CartItem> GetCartItemById(int cartItemId)
        {
            return await CartDAO.Instance.GetCartItemById(cartItemId);
        }

    }
}
