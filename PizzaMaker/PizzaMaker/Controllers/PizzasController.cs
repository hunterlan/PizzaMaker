using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger _logger;

        public PizzasController(IMemoryCache memoryCache, ILogger<PizzasController> logger)
        {
            _logger = logger;
            _cache = memoryCache;
        }

        private void FillPizzaList()
        {
            ordersPizza = new List<PizzaCount>();
            var pizzas = _context.Pizzas.ToList();
            int i = 0;

            _logger.LogInformation("Fill user list with all pizza");

            foreach (var pizza in pizzas)
            {
                PizzaCount newPizza = new PizzaCount();

                newPizza.pizza = pizza;
                newPizza.count = 0;

                ordersPizza.Add(newPizza);

                i++;
            }

            _logger.LogInformation("Operation done.");
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

        private decimal getTotalPrice()
        {
            decimal totalPrice = 0;

            for (int i = 0; i < ordersPizza.Count; i++)
            {
                if (ordersPizza[i].count != 0)
                {
                    for (int j = 0; j < ordersPizza[i].count; j++)
                    {
                        totalPrice += ordersPizza[i].pizza.Price;
                    }
                }
            }

            return totalPrice;
        }

        // GET: Pizzas
        public async Task<IActionResult> Index()
        {
            if (ordersPizza == null || ordersPizza.Count == 0 || 
                !_cache.TryGetValue(KEY_CACHING, out ordersPizza))
            {
                _logger.LogInformation("Cache is empty.");
                FillPizzaList();
            }
            else
            {
                _logger.LogInformation("Current data has read from cache.");
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
                _logger.LogInformation("Cache is empty.");
                FillPizzaList();
            }
            else
            {
                _logger.LogInformation("Current data has read from cache.");
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
            int countList = 0;

            if (_cache.TryGetValue(KEY_CACHING, out ordersPizza))
            {
                _logger.LogInformation("We have something here. Starting count.");
                for (int i = 0; i < ordersPizza.Count; i++)
                {
                    if (ordersPizza[i].count != 0)
                    {
                        countList += ordersPizza[i].count;
                        for (int j = 0; j < ordersPizza[i].count; j++)
                        {
                            totalPrice += ordersPizza[i].pizza.Price;
                        }
                    }
                }
                _logger.LogInformation("End");
            }
            else
            {
                _logger.LogInformation("Cart is empty, dude.");
            }

            totalPrice = Math.Round(totalPrice, 2);

            ViewData["CountList"] = countList;
            ViewData["TotalPrice"] = totalPrice;

            return View(ordersPizza);
        }

        [HttpPost]
        public IActionResult Cart(string[] datas)
        {
            OrderContext orderContext = new OrderContext();
            List<Order> userOrders = new List<Order>();
            Order order = new Order();
            Random rand = new Random();

            order.NameReciver = datas[0];
            order.Adress = datas[1];
            order.Phone = datas[2];
            order.NumberOrder = rand.Next(0, 1000);

            _logger.LogInformation("Getting data about cart from cache.");

            ordersPizza = (List<PizzaCount>)_cache.Get(KEY_CACHING);
            order.TotalPrice = getTotalPrice();

            _logger.LogInformation("Finally, fill data about order.");
            for (int i = 0; i < ordersPizza.Count; i++)
            {
                Order nonChangableOrder = new Order(order);
                if (ordersPizza[i].count != 0)
                {
                    nonChangableOrder.PizzaID = ordersPizza[i].pizza.ID;
                    nonChangableOrder.Count = ordersPizza[i].count;

                    userOrders.Add(nonChangableOrder);
                }
            }

            _logger.LogInformation("Adding to DB data.");
            foreach (Order userOrder in userOrders)
            {
                orderContext.Add(userOrder);
            }

            if(orderContext.SaveChanges() != 0)
            {
                _logger.LogInformation("Clear cache");
                _cache.Remove(KEY_CACHING);
            }
            else
            {
                _logger.LogWarning("Data not written to DB.");
            }

            return RedirectToAction("Index");
        }
    }
}
