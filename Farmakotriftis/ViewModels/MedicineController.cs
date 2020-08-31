using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Farmakotriftis.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Farmakotriftis.ViewModels
{
    public class MedicineController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;
        public MedicineController(ApplicationDbContext _applicationDbContext)
        {
            applicationDbContext = _applicationDbContext;
        }
        [Authorize]
        public IActionResult Index()
        {
            var medicine = applicationDbContext.Medicines.ToList();
            return View(medicine);
        }
    }
}