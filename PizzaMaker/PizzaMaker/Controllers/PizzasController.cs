using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaMaker.Context;
using PizzaMaker.Models;

namespace PizzaMaker.Controllers
{
    public class PizzaCount
    {
        public Pizza pizza { get; set; }
        public int count { get; set; }
    }
    public class PizzasController : Controller
    {
        private readonly PizzaContext _context = new PizzaContext();
        private List<PizzaCount> ordersPizza = new List<PizzaCount>();

        // GET: Pizzas
        public async Task<IActionResult> Index()
        {


            return View(await _context.Pizzas.ToListAsync());
        }

        // GET: Pizzas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pizza = await _context.Pizzas
                .FirstOrDefaultAsync(m => m.ID == id);
            if (pizza == null)
            {
                return NotFound();
            }

            return PartialView(pizza);
        }

        [HttpPost]
        public async Task<IActionResult> Details(Pizza toCart)
        {
            toCart = _context.Pizzas.FirstOrDefault(m => m.ID == toCart.ID);
            ordersPizza.Add(toCart);

            return await Index();
        }

        public async Task<IActionResult> Cart()
        {

        }
    }
}
