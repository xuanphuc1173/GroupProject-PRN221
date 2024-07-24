using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class CartDAO : SingletonBase<CartDAO>
    {
        public async Task<Cart> GetCartByUserId(string userId)
        {
            return await _context.Carts
                .Include(c => c.Items)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task AddCart(Cart cart)
        {
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

        public async Task AddCartItem(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteCartItem(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;

        }
        public async Task UpdateCartItem(CartItem cartItem)
        {
            var existingCartItem = await _context.CartItems.FindAsync(cartItem.CartItemId);
            if (existingCartItem != null)
            {
                existingCartItem.Quantity = cartItem.Quantity;
                _context.CartItems.Update(existingCartItem);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> ClearCartByUserId(string userId)
        {
            try
            {
                var cart = await _context.Carts.Include(c => c.Items).FirstOrDefaultAsync(c => c.UserId == userId);
                if (cart != null)
                {
                    _context.CartItems.RemoveRange(cart.Items);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<CartItem> GetCartItemById(int cartItemId)
        {
            using var context = new ApplicationDbContext();
            return await context.CartItems
                                .Include(c => c.Product)
                                .Include(c => c.Cart)
                                .FirstOrDefaultAsync(c => c.CartItemId == cartItemId);
        }

    }
}
