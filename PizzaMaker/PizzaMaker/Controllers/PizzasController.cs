using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PizzaMaker.Context;
using PizzaMaker.Models;

namespace PizzaMaker.Controllers
{
    public class PizzasController : Controller
    {
        private readonly PizzaContext _context = new PizzaContext();
        private const int KEY_CACHING = 51;
        private List<PizzaCount> ordersPizza;
        private IMemoryCache _cache;

        public PizzasController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        private void FillPizzaList()
        {
            ordersPizza = new List<PizzaCount>();
            var pizzas = _context.Pizzas.ToList();
            int i = 0;

            foreach (var pizza in pizzas)
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

            for (int i = 0; i < ordersPizza.Count; i++)
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
            if (ordersPizza == null || ordersPizza.Count == 0 || 
                !_cache.TryGetValue(KEY_CACHING, out ordersPizza))
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
        public IActionResult Details(Pizza toCart)
        {
            toCart = _context.Pizzas.FirstOrDefault(m => m.ID == toCart.ID);

            if (!_cache.TryGetValue(KEY_CACHING, out ordersPizza))
            {
                FillPizzaList();
            }

            //Finding index of pizza, which user choosed
            int currentIndex = FindUserListPizza(toCart);

            ordersPizza[currentIndex].count++;

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetPriority(CacheItemPriority.NeverRemove);

            _cache.Set(KEY_CACHING, ordersPizza, cacheEntryOptions);

            return RedirectToAction("Index");
        }

        public IActionResult Cart()
        {
            decimal totalPrice = 0;

            if (_cache.TryGetValue(KEY_CACHING, out ordersPizza))
            {
                for (int i = 0; i < ordersPizza.Count; i++)
                {
                    if (ordersPizza[i].count != 0)
                    {
                        totalPrice += ordersPizza[i].pizza.Price;
                    }
                }
            }

            ViewData["TotalPrice"] = totalPrice;

            return View(ordersPizza);
        }

        [HttpPost]
        public IActionResult Cart(Order order)
        {
            ordersPizza = (List<PizzaCount>)_cache.Get(KEY_CACHING);
            order.TotalPrice = (decimal)ViewData["TotalPrice"];



            return RedirectToAction("Index");
        }
    }
}
