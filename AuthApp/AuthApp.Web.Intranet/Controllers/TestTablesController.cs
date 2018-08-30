using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuthApp.Data;
using AuthApp.Domain;
using AuthApp.Security;
using AuthApp.Common;

namespace AuthApp.Web.Intranet.Controllers
{
    [RequiresPermission(Permission.TestTable)]
    public class TestTablesController : Controller
    {
        private readonly AuthAppDbContext _context;

        public TestTablesController(AuthAppDbContext context)
        {
            _context = context;
        }

        // GET: TestTables
        public async Task<IActionResult> Index()
        {
            return View(await _context.TestTables.ToListAsync());
        }

        // GET: TestTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testTable = await _context.TestTables
                .FirstOrDefaultAsync(m => m.TestTableId == id);
            if (testTable == null)
            {
                return NotFound();
            }

            return View(testTable);
        }

        // GET: TestTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TestTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TestTableId,Name")] TestTable testTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(testTable);
        }

        // GET: TestTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testTable = await _context.TestTables.FindAsync(id);
            if (testTable == null)
            {
                return NotFound();
            }
            return View(testTable);
        }

        // POST: TestTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TestTableId,Name")] TestTable testTable)
        {
            if (id != testTable.TestTableId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestTableExists(testTable.TestTableId))
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
            return View(testTable);
        }

        // GET: TestTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testTable = await _context.TestTables
                .FirstOrDefaultAsync(m => m.TestTableId == id);
            if (testTable == null)
            {
                return NotFound();
            }

            return View(testTable);
        }

        // POST: TestTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testTable = await _context.TestTables.FindAsync(id);
            _context.TestTables.Remove(testTable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestTableExists(int id)
        {
            return _context.TestTables.Any(e => e.TestTableId == id);
        }
    }
}
