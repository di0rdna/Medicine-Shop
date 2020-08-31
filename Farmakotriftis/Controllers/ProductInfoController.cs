using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farmakotriftis.Data;
using Microsoft.AspNetCore.Mvc;

namespace Farmakotriftis.Controllers
{
    public class ProductInfoController : Controller
    {
        private ApplicationDbContext applicationDbContext;
        public ProductInfoController(ApplicationDbContext _applicationDbContext)
        {
            applicationDbContext = _applicationDbContext;
        }

        public IActionResult Index(int id)
        {
            var medicine = applicationDbContext.Medicines.Find(id);
            if (medicine != null)
            {
                return View(medicine);
            }

            return RedirectToAction("index", "Medicine");
        }
    }
}