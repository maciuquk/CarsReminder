using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarsReminder.Data;
using CarsReminder.Models;
using CarsReminder.ModelView;
using Microsoft.AspNetCore.Routing;

namespace CarsReminder.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index(string data)
        {
            var currentContext = new List<Item>();

            if (!string.IsNullOrEmpty(data))
            {
                currentContext = await _context.ItemsToRemind.Where(n => n.Mark == data || n.Model == data).ToListAsync();
            }
            else
                currentContext = await _context.ItemsToRemind.ToListAsync();


            var itemsModelView = new IndexContextModelView();
            var groups = new Dictionary<string, string>();
            
            //group by mark
            var markgroups = currentContext.GroupBy(n => n.Mark).ToList();
            foreach (var item in markgroups)
            {
                groups.Add(item.Key, "mark");
            }
            
            //group by model
            var modelgroups = currentContext.GroupBy(n => n.Model).ToList();
            foreach (var item in modelgroups)
            {
                groups.Add(item.Key, "model");
            }

            //send data to modelview
            itemsModelView.ItemList = currentContext;
            itemsModelView.Groups = groups;

            return View(itemsModelView);
        }

       [HttpGet]
        public async Task<IActionResult> Filtered(string data)
        {
            

            var routeValues = new RouteValueDictionary {
                { "data", data},
            };

            //return RedirectToAction("Index", filteredContext);

            return RedirectToAction("Index", routeValues);
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.ItemsToRemind
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Mark,Model,Price,Distance,Description,PhotoUrl")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.ItemsToRemind.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Mark,Model,Price,Distance,Description,PhotoUrl")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.ItemsToRemind
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.ItemsToRemind.FindAsync(id);
            _context.ItemsToRemind.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.ItemsToRemind.Any(e => e.Id == id);
        }
    }
}
