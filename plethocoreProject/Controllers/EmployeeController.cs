using plethocoreProject.entity;
using plethocoreProject.Models;
using plethocoreProject.services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;

namespace plethocoreProject.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeServices _employeeservices;
        private readonly HostingEnvironment _hostingEnvironment;

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
        [HttpGet]
        public IActionResult Create()
        {
            var model = new EmployeeCreateViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//prevent crooss site attaxk
        public async Task<IActionResult> Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    Id = model.Id,
                    EmployeeNo = model.EmployeeNo,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    FullName = model.FullName,
                    Gender = model.Gender,
                    Email = model.Email,
                    DOB = model.DOB,
                    DateofJoined = model.DateJoined,
                    NationalInsuranceNo = model.NationalInsuranceNo,
                    paymentMethod = model.PaymentMethod,
                    studentLoan = model.StudentLoan,
                    UnionMember = model.UnionMember,
                    Address = model.Address,
                    City = model.City,
                    Phone = model.City,
                    PostalCode = model.Postcode,
                    MiddleName = model.MiddleName,
                    Designation = model.Designation,

                };
                if (model.ImageUrl != null && model.ImageUrl.Length > 0)
                {
                    var uploadDir = @"images/employee";
                    var filname = Path.GetFileNameWithoutExtension(model.ImageUrl.FileName);
                    var extension = Path.GetExtension(model.ImageUrl.FileName);
                    var webroot = _hostingEnvironment.ContentRootPath;
                    filname = DateTime.UtcNow.ToString("yymmssfff") + filname.ToString() + extension;
                    var path = Path.Combine(webroot, uploadDir);
                    await model.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));
                    employee.ImageUrl = "/" + uploadDir + "/" + filname;
                }
                await _employeeservices.CreateAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _employeeservices.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            var model = new EmployeeEditViewModel()
            {
                Id = employee.Id,
                EmployeeNo = employee.EmployeeNo,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                Gender = employee.Gender,
                Email = employee.Email,
                DOB = employee.DOB,
                DateJoined = employee.DateofJoined,
                NationalInsuranceNo = employee.NationalInsuranceNo,
                PaymentMethod = employee.paymentMethod,
                StudentLoan = employee.studentLoan,
                UnionMember = employee.UnionMember,
                Address = employee.Address,
                City = employee.City,
                Phone = employee.Phone,
                Postcode = employee.PostalCode,
                Designation = employee.Designation,
            };
            return View(model);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeEditViewModel emp)
        {
            if (ModelState.IsValid)
            {
                var employee = _employeeservices.GetById(emp.Id);
                if (employee != null)
                {
                    employee.Id = emp.Id;
                    employee.EmployeeNo = emp.EmployeeNo;
                    employee.FirstName = emp.FirstName;
                    employee.MiddleName = emp.MiddleName;
                    employee.LastName = emp.LastName;
                    employee.Gender = emp.Gender;
                    employee.Email = emp.Email;
                    employee.DOB = emp.DOB;
                    employee.DateofJoined = emp.DateJoined;
                    employee.NationalInsuranceNo = emp.NationalInsuranceNo;
                    employee.paymentMethod = emp.PaymentMethod;
                    employee.studentLoan = emp.StudentLoan;
                    employee.UnionMember = emp.UnionMember;
                    employee.Address = emp.Address;
                    employee.City = emp.City;
                    employee.Phone = emp.Phone;
                    employee.PostalCode = emp.Postcode;
                    employee.Designation = emp.Designation;
                    if (emp.ImageUrl != null && emp.ImageUrl.Length > 0)
                    {
                        //var uploadDir = @"images/employee";
                        //var filname = Path.GetFileNameWithoutExtension(emp.ImageUrl.FileName);
                        //var extension = Path.GetExtension(emp.ImageUrl.FileName);
                        //var webroot = _hostingEnvironment.ContentRootPath;
                        //filname = DateTime.UtcNow.ToString("yymmssfff") + filname.ToString() + extension;
                        //var path = Path.Combine(webroot, uploadDir);
                        //await emp.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));
                        //employee.ImageUrl = "/" + uploadDir + "/" + filname;
                    }
                    await _employeeservices.UpdateAsync(employee);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var emp = _employeeservices.GetById(id);
            if (emp == null)
            {
                return NotFound();
            }
            EmployeeDetailstViewModel model = new EmployeeDetailstViewModel()
            {
                Id = emp.Id,
                EmployeeNo = emp.EmployeeNo,
                FullName = emp.FullName,
                Gender = emp.Gender,
                ImageUrl = emp.ImageUrl,
                DOB = emp.DOB,
                DateJoined = emp.DateofJoined,
                Phone = emp.Phone,
                Designation = emp.Designation,
                Email = emp.Email,
                NationalInsuranceNo = emp.NationalInsuranceNo,
                PaymentMethod = emp.paymentMethod,
                StudentLoan = emp.studentLoan,
                UnionMember = emp.UnionMember,
                Address = emp.Address,
                City = emp.City,
                Postcode = emp.PostalCode
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var emp = _employeeservices.GetById(id);
            if (emp == null)
            {
                return NotFound();

            }
            EmployeeDeleteViewModel model = new EmployeeDeleteViewModel()
            {
                Id = emp.Id,
                FullName = emp.FullName
            };


            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeDeleteViewModel model)
        {
            await _employeeservices.Delete(model.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
