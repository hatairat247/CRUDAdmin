using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HW3Admin.Models;
using HW3Admin.Data; // อ้างอิง namespace ที่มี DbContext 

namespace HW3Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HW3AdminDBContext _context; // เปลี่ยนให้ตรงตาม DbContext

        public HomeController(ILogger<HomeController> logger, HW3AdminDBContext context)
        {
            _logger = logger;
            _context = context; // ใช้ DbContext
        }

        public IActionResult Index()
        {
            var products = _context.PRODUCT.ToList(); // ดึงข้อมูลผลิตภัณฑ์ทั้งหมด
            return View(products); // ส่งข้อมูลไปยัง View
        }

        public IActionResult Privacy(Login admin)
        {
            if (ModelState.IsValid) // ตรวจสอบว่าโมเดลถูกต้อง
            {
                // เช็ค username และ password
                if (admin.Username == "Admin" && admin.Password == "Password123")
                {
                    // ถ้ารหัสถูกต้อง นำผู้ใช้ไปที่หน้า Admin
                    return RedirectToAction("Index", "Admin"); // เปลี่ยนเส้นทางไปยังหน้า Admin
                }

                // ถ้ารหัสผิด ให้แสดงข้อความผิดพลาด
                ModelState.AddModelError("", "Invalid login attempt.");
            }

            // ถ้าข้อมูลไม่ถูกต้องหรือเข้าสู่ระบบไม่สำเร็จ ให้แสดงผลหน้าเดิมพร้อมข้อความผิดพลาด
            return View(admin);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}