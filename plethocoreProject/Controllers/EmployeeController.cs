using Microsoft.AspNetCore.Mvc;
using plethocoreProject.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace plethocoreProject.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeServices _employeeservices;
        

        public EmployeeController(IEmployeeServices employeeservices)
        {
            _employeeservices = employeeservices;

        }
        public IActionResult Index()
        {
            var emp = _employeeservices.GetAll().Select(employee => new EmployeeIndexViewModel
            {
                Id = employee.Id,
                EmployeeNo = employee.EmployeeNo,
                ImageUrl = employee.ImageUrl,
                FullName = employee.FullName,
                Gender = employee.Gender,
                Designation = employee.Designation,
                city = employee.City,
                DateofJoined = employee.DateofJoined

            }).ToList();
            return View(emp);
        }
    }
}
