using System.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using D7CRUD.NETCrud_Example.Data;
using D7CRUD.NETCrud_Example.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace D7CRUD.NETCrud_Example.Controllers
{
    [Route("[controller]")]
    public class CategoriesController : Controller
    {   
        private readonly ApplicationDbContext _context;
        // private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ApplicationDbContext context
        // ILogger<CategoriesController> logger
        )
        {   
            _context = context;
            // _logger = logger;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {   
            var _Category = await _context.Category.ToListAsync();
            if (_Category.Count < 1)
                await CreateTestData();

            return _context.Category != null ? 
                View(await _context.Category.ToListAsync()) :
                Problem("Entity set 'ApplicationDbContext.Category' is null.");
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Category == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null) {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create

        public IActionResult Create() 
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Category category) 
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Category == null)
            {
                return NotFound();
            }

            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Description")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return View(category);
        }


        [HttpDelete]
        public async Task<JsonResult> Delete(Int64 id)
        {
            try
            {
                var _Categories = await _context.Category.FindAsync(id);

                if (_Categories != null)
                {
                    _context.Category.Remove(_Categories);
                }
                await _context.SaveChangesAsync();
                return new JsonResult(_Categories);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool CategoryExists(long id)
        {
            return (_context.Category?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async Task CreateTestData()
        {
            foreach (var item in GetCategoryList())
            {
                _context.Category.Add(item);
                await _context.SaveChangesAsync();
            }
        }

        private IEnumerable<Category> GetCategoryList()
        {
            return new List<Category>
            {
                new Category { Name = "Item Category 01", Description = "Desccription of your category item: lorem ipsum"},
                new Category { Name = "Item Category 02", Description = "Desccription of your category item: lorem ipsum"},
                new Category { Name = "Item Category 03", Description = "Desccription of your category item: lorem ipsum"},
                new Category { Name = "Item Category 04", Description = "Desccription of your category item: lorem ipsum"},
                new Category { Name = "Item Category 05", Description = "Desccription of your category item: lorem ipsum"},

                new Category { Name = "Item Category 06", Description = "Desccription of your category item: lorem ipsum"},
                new Category { Name = "Item Category 07", Description = "Desccription of your category item: lorem ipsum"},
                new Category { Name = "Item Category 08", Description = "Desccription of your category item: lorem ipsum"},
                new Category { Name = "Item Category 09", Description = "Desccription of your category item: lorem ipsum"},
                new Category { Name = "Item Category 10", Description = "Desccription of your category item: lorem ipsum"},
            };
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}