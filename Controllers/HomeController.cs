using Microsoft.AspNetCore.Mvc;
using ProbeArbeit_Heberling.Data;
using ProbeArbeit_Heberling.Models;
using System.Diagnostics;

namespace ProbeArbeit_Heberling.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var allEmployees = _context.Employees.ToList();
            return View(allEmployees);
        }

        public IActionResult CreateEditEmployees(int id)
        {
            if (id != 0)
            {
                var employeeFromDB = _context.Employees.SingleOrDefault(x => x.ID == id);

                if (employeeFromDB != null)
                {
                    return View(employeeFromDB);
                }
                else
                {
                    return NotFound();
                }
            }
            return View();
        }


        public IActionResult CreateEditEmployeeRecord(Employee employee) 
        {
            if(employee.ID ==  0)
            {
                _context.Employees.Add(employee);
            }
            else
            {
                _context.Employees.Update(employee);
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteEmployeeByID(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var employeeFromDB = _context.Employees.SingleOrDefault(x => x.ID==id);

            if (employeeFromDB == null) 
            {
                return NotFound();
            }

            _context.Employees.Remove(employeeFromDB);
            _context.SaveChanges();

            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
