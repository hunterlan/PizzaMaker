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
    public class PizzasController : Controller
    {
        private readonly PizzaContext _context = new PizzaContext();
        private List<PizzaCount> ordersPizza = new List<PizzaCount>();

        private void FillPizzaList()
        {
            var pizzas = _context.Pizzas.ToList();
            int i = 0;

            foreach(var pizza in pizzas)
            {
                PizzaCount newPizza = new PizzaCount();

                newPizza.pizza = pizza;
                newPizza.count = 0;

                ordersPizza.Add(newPizza);

                i++;
            }
        }

        private int FindUserListPizza(Pizza pizza)
        {
            int index = 0;

            for(int i = 0; i < ordersPizza.Count; i++)
            {
                if (ordersPizza[i].pizza.ID == pizza.ID)
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        // GET: Pizzas
        public async Task<IActionResult> Index()
        {
            if(ordersPizza.Count == 0)
            {
                FillPizzaList();
            }

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
            int currentIndex = FindUserListPizza(toCart);

            ordersPizza[currentIndex].count++;

            return await Index();
        }

        public IActionResult Cart()
        {
            int countPizzaInCart = 0;

            for (int i = 0; i < ordersPizza.Count; i++)
            {
                if (ordersPizza[i].count != 0)
                {
                    countPizzaInCart += ordersPizza[i].count;
                }
            }

            ViewData["CountList"] = countPizzaInCart;

            return View(ordersPizza);
        }
    }
}
