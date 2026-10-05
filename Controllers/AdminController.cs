using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HW3Admin.Data;
using HW3Admin.Models;

namespace HW3Admin.Controllers
{
    public class AdminController : Controller
    {
        private readonly HW3AdminDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AdminController(HW3AdminDBContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
            //return View(await _context.PRODUCT.ToListAsync());
            return _context.PRODUCT != null ? 
                          View(await _context.PRODUCT.ToListAsync()) :
                          Problem("Entity set 'HW3AdminDBContext.PRODUCT'  is null.");
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PRODUCT == null)
            {
                return NotFound();
            }

            var pRODUCTS = await _context.PRODUCT
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pRODUCTS == null)
            {
                return NotFound();
            }

            return View(pRODUCTS);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Descrition,Price,ImagesPD")] PRODUCTS pRODUCTS)
        {
            if (ModelState.IsValid)
            {
                if (pRODUCTS.ImageFile != null)
                {
                    // กำหนด path ไปยังโฟลเดอร์ wwwroot/images
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(pRODUCTS.ImageFile.FileName);
                    string extension = Path.GetExtension(pRODUCTS.ImageFile.FileName);
                    pRODUCTS.ImagesPD = fileName + DateTime.Now.ToString("yymmssfff") + extension; // ตั้งชื่อไฟล์พร้อม timestamp
                    string path = Path.Combine(wwwRootPath + "/images/", pRODUCTS.ImagesPD);

                    // บันทึกไฟล์ไปยังโฟลเดอร์ wwwroot/images
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await pRODUCTS.ImageFile.CopyToAsync(fileStream);
                    }
                }

                _context.Add(pRODUCTS);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pRODUCTS);
        }


        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PRODUCT == null)
            {
                return NotFound();
            }

            var pRODUCTS = await _context.PRODUCT.FindAsync(id);
            if (pRODUCTS == null)
            {
                return NotFound();
            }
            return View(pRODUCTS);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Descrition,Price,ImagesPD")] PRODUCTS pRODUCTS)
        {
            if (id != pRODUCTS.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pRODUCTS);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PRODUCTSExists(pRODUCTS.Id))
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
            return View(pRODUCTS);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PRODUCT == null)
            {
                return NotFound();
            }

            var pRODUCTS = await _context.PRODUCT
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pRODUCTS == null)
            {
                return NotFound();
            }

            return View(pRODUCTS);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PRODUCT == null)
            {
                return Problem("Entity set 'HW3AdminDBContext.PRODUCT'  is null.");
            }
            var pRODUCTS = await _context.PRODUCT.FindAsync(id);
            if (pRODUCTS != null)
            {
                _context.PRODUCT.Remove(pRODUCTS);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PRODUCTSExists(int id)
        {
          return (_context.PRODUCT?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
