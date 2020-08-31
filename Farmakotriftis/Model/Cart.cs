using Farmakotriftis.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmakotriftis.Model
{
    public class Cart
    {
        public List<CartItemViewModel> CartItems { get; private set; }

        public Cart()
        {
            CartItems = new List<CartItemViewModel>();
        }

        public void Add(CartItemViewModel item)
        {
            var cartItem = CartItems.Where(it => it.Id == item.Id).FirstOrDefault();
            if (cartItem == null)
            {
                CartItems.Add(item);
            }
            else
            {
                cartItem.Quantity += item.Quantity;
            }
        }

        internal void Change(int id, int qty)
        {
            var cartItem = CartItems.Where(x => x.Id == id).FirstOrDefault();
            if (cartItem != null)
            {
                cartItem.Quantity = qty;
                cartItem.Price = qty * cartItem.UnitPrice;
            }
        }

        internal void Delete(int id)
        {
            var cartItem = CartItems.Where(x => x.Id == id).FirstOrDefault();
            if (cartItem != null)
            {
                CartItems.Remove(cartItem);
            }
        }
    }
}
