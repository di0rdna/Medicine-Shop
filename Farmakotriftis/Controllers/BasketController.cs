using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farmakotriftis.Data;
using Microsoft.AspNetCore.Mvc;
using Farmakotriftis.Model;
using Farmakotriftis.ViewModels;
using Farmakotriftis.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace Farmakotriftis.Controllers
{
    public class BasketController : Controller
    {
        private ApplicationDbContext applicationDbContext;
        public BasketController(ApplicationDbContext _applicationDbContext)
        {
            applicationDbContext = _applicationDbContext;
        }
        [Authorize]
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<Cart>(HttpContext.Session, "cart");
            if (cart != null)
            {
                return View(cart.CartItems);
            }
            return View(new List<CartItemViewModel>());
        }
        //[HttpPost("{id}")]
        public IActionResult Add(int id)
        {
            var qty = Request.Form["AddToBasket"].ToString();
            if (int.TryParse(qty, out int quantity) == true)
            {
                var medicine = applicationDbContext.Medicines.Find(id);
                var vm = new CartItemViewModel()
                {
                    Quantity = quantity,
                    Id = medicine.Id,
                    ProductCode = medicine.ProductCode,
                    ProductName = medicine.ProductName,
                    UnitPrice = medicine.Price
                };

                var cart = SessionHelper.GetObjectFromJson<Cart>(HttpContext.Session, "cart");
                if (cart == null)
                {
                    cart = new Cart();
                }
                cart.Add(vm);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }

            return RedirectToAction("Index", "Medicine");
        }

        public IActionResult Change(int id)
        {

            var cart = SessionHelper.GetObjectFromJson<Cart>(HttpContext.Session, "cart");
            if (cart != null)
            {
                if (int.TryParse(Request.Form["ChangeBasket"].ToString(), out int quantity) == true)
                {
                    cart.Change(id, quantity);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                }
            }

            return RedirectToAction("Index", "Basket");
        }

        public IActionResult Delete(int id)
        {
            var cart = SessionHelper.GetObjectFromJson<Cart>(HttpContext.Session, "cart");
            if (cart != null)
            {
                cart.Delete(id);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index", "Basket");
        }
    }
}