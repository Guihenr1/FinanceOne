using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceOne.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceOne.Controllers
{
    public class PlanoContaController : Controller
    {
        IHttpContextAccessor HttpContextAccessor;

        public PlanoContaController(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            PlanoContaModel objPlanoContas = new PlanoContaModel(HttpContextAccessor);
            ViewBag.ListaPlanoContas = objPlanoContas.ListaPlanoContas();
            return View();
        }

        [HttpPost]
        public IActionResult CriarPlanoConta(PlanoContaModel formulario)
        {
            if (ModelState.IsValid)
            {
                formulario.HttpContextAccessor = HttpContextAccessor;
                formulario.Insert();
                return RedirectToAction("index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult CriarPlanoConta(int? id)
        {
            if(id != null)
            {
                PlanoContaModel objPlanoContas = new PlanoContaModel(HttpContextAccessor);
                ViewBag.Registro = objPlanoContas.CarregarRegistro(id);
            }
            return View();
        }

        [HttpGet]
        public IActionResult ExcluirPlanoConta(int id)
        {
            PlanoContaModel objPlanoConta = new PlanoContaModel(HttpContextAccessor);
            objPlanoConta.Excluir(id);
            return RedirectToAction("index");
        }
    }
}