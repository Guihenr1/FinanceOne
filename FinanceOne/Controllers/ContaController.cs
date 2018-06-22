using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceOne.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceOne.Controllers
{
    public class ContaController : Controller
    {
        IHttpContextAccessor HttpContextAccessor;

        public ContaController(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            ContaModel objConta = new ContaModel(HttpContextAccessor);
            ViewBag.ListaConta = objConta.ListaConta();
            return View();
        }
    }
}