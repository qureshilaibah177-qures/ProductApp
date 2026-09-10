using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApp.Models;

namespace ProductApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        // READ ALL & SEARCH (By ProductId, Name, Barcode, or Category)
        public async Task<IActionResult> Index(string searchString)
        {
            var products = _context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                if (int.TryParse(searchString, out int id))
                {
                    products = products.Where(p => p.ProductId == id);
                }
                else
                {
                    products = products.Where(p => p.Name.Contains(searchString)
                                                || p.Barcode.Contains(searchString)
                                                || p.Category.Contains(searchString));
                }
            }
            return View(await products.ToListAsync());  //Converting all matched results into c# list and let clients view it
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null) return NotFound();
            return View(product);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]      // So that attributes can only be triggerd by HTTP POST
        [ValidateAntiForgeryToken] // Security Feature Against CSRF Attack
        public async Task<IActionResult> Create([Bind("ProductId,Barcode,Name,Category,Price,StockQuantity")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FindAsync(id);    //Returning a Single Product by ID
            if (product == null) return NotFound();
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Barcode,Name,Category,Price,StockQuantity")] Product product)
        {
            if (id != product.ProductId) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null) return NotFound();
            return View(product);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);   //Returns true/ false based on existance of id
        }
    }
}