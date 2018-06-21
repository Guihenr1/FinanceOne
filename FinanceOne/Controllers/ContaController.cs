using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceOne.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinanceOne.Controllers
{
    public class ContaController : Controller
    {
        public IActionResult Index()
        {
            ContaModel objConta = new ContaModel();
            ViewBag.ListaConta = objConta.ListaConta();
            return View();
        }
    }
}