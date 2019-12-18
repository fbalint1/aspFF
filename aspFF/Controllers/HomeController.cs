using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using aspFF.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using aspFF.Data;

namespace aspFF.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private RoleManager<IdentityRole> roleManager;
        private UserManager<IdentityUser> userManager;
        ApplicationDbContext database;
        List<Item> cart = new List<Item>();

        public HomeController(ILogger<HomeController> logger, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, ApplicationDbContext dbContext)
        {
            _logger = logger;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.database = dbContext;
        }

        public IActionResult LogoutCartDelete()
        {
            foreach (var item in database.Items)
            {
                if (item.Reserved != 0)
                {
                    item.Quantity += item.Reserved;
                    item.Reserved = 0;
                }
            }

            database.SaveChanges();

            return RedirectToAction(nameof(Welcome));
        }

        public IActionResult Welcome()
        {
            return View();
        }

        [Authorize]
        public IActionResult ListItems()
        {
            return View(database.Items);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Admin()
        {
            return View();
        }

        [Authorize]
        public IActionResult Cart()
        {
            var items = database.Items.Where(x => x.Reserved != 0).Select(x => x);    
            
            return View(items);
        }

        [HttpPost]
        public IActionResult DeleteCart()
        {
            foreach (var item in database.Items)
            {
                if(item.Reserved != 0)
                {
                    item.Quantity += item.Reserved;
                    item.Reserved = 0;
                }
            }

            database.SaveChanges();

            return RedirectToAction(nameof(Cart));
        }

        [HttpPost]
        public IActionResult DeleteItemFromCart(int id)
        {
            var item = database.Items.First(x => x.Id == id);

            item.Quantity += item.Reserved;
            item.Reserved = 0;
            database.SaveChanges();

            return RedirectToAction(nameof(Cart));
        }

        [HttpPost]
        public IActionResult AddItem(string type, string name, int price, int quantity)
        {
            Item item = new Item
            {
                Type = type,
                Name = name,
                Price = price,
                Quantity = quantity
            };

            database.Items.Add(item);
            database.SaveChanges();

            return RedirectToAction(nameof(Admin));
        }

        [Authorize]
        public IActionResult AddToCart(int id, int quantity)
        {
            if(quantity != 0)
            {
                var item = database.Items.First(x => x.Id == id);

                item.Quantity -= quantity;
                item.Reserved = quantity;

                database.SaveChanges();

            }

            return RedirectToAction(nameof(ListItems));
        }

        
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckoutData()
        {
            foreach (var item in database.Items)
            {
                if(item.Reserved != 0)
                {
                    item.Reserved = 0;
                }
            }

            database.SaveChanges();

            return RedirectToAction(nameof(Welcome));
        }

        [HttpGet]
        public int Sum()
        {
            int sum = 0;
            foreach (var item in database.Items)
            {
                sum += (item.Reserved * item.Price);
            }
            return sum;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
