using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using plethocoreProject.services;
using plethocoreProject.services.Implementation;
using plethocoreProject.Models;
using plethocoreProject.entity;

namespace plethocoreProject.Controllers
{
    public class PayController: Controller
    {
        private readonly IpaycompuatuationServices _paycompuatuationServices;
        private readonly IEmployeeServices _employeeServices;

        private readonly INationalInsuranceContributionService _nationalInsuranceContributionService;
        private readonly ITaxservices _taxservices;
        private decimal overtimeHrs;
        private decimal studentloan;
        private decimal totalEarnings;
        private decimal nationalinsurance;
        private decimal tax;
        private decimal unionFee;
        private decimal totalDeduction;

        public decimal ContractualEarnings { get; private set; }
        public decimal OvertimeEarnings { get; private set; }

        public PayController(IpaycompuatuationServices _ppaycompuatuationServices, IEmployeeServices employeeServices, ITaxservices _taxservicess, INationalInsuranceContributionService _nationalInsuranceContributionServicee) 
        {
            _paycompuatuationServices = _ppaycompuatuationServices;
            _employeeServices = employeeServices;
            _taxservices = _taxservicess;
            _nationalInsuranceContributionService = _nationalInsuranceContributionServicee;
        }
        public IActionResult Index()
        {
            var payrecords = _paycompuatuationServices.GetAll().Select(x => new PaymentRecordIndexViewModel
            {
                Id = x.Id,
                EmployeeId = x.EmployeeId,
                FullName = x.FullName,
                PayDate = x.PayDate,
                PayMonth = x.PayMonth,
                TaxYearId = x.TaxYearId,
                Year = _paycompuatuationServices.GetTaxYearById(x.TaxYearId).YearofTax,
                TotalEarnings = x.TotalEarnings,
                TotalDeduction = x.TotalDeduction,
                NetPayment = x.NetPayment,
                Employee = x.Employee

            });
            return View();

        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.employees = _employeeServices.GetAllEmployeeforPayroll();
            ViewBag.taxyears = _paycompuatuationServices.GetAllTaxYear();
            var model = new PaymentRecordCreateViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(PaymentRecordCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var payrecord = new PaymentRecord()
                {
                    Id = model.Id,
                    EmployeeId = model.EmployeeId ,
                    FullName = _employeeServices.GetById(model.EmployeeId).FullName,
                    NiNo = _employeeServices.GetById(model.EmployeeId).NationalInsuranceNo,
                    PayDate = model.PayDate,
                    PayMonth = model.PayMonth,
                    TaxYearId = model.TaxYearId,
                    TaxCode = model.TaxCode,
                    HourlyRate = model.HourlyRate,
                    HoursWorkd = model.HoursWorked,
                    ContractualHours = model.ContractualHours,
                    OverTimeHours =overtimeHrs = _paycompuatuationServices.OvertimeHours(model.HoursWorked, model.ContractualHours),
                    ContractualEarnings = ContractualEarnings = _paycompuatuationServices.ContractualEarnings(model.ContractualHours, model.HoursWorked, model.HourlyRate),
                    OvertimeEarnings = OvertimeEarnings= _paycompuatuationServices.OvertimeEarnings(_paycompuatuationServices.OvertimeRate(model.HourlyRate), overtimeHrs),
                    TotalEarnings = totalEarnings = _paycompuatuationServices.TotalEarnings(OvertimeEarnings, ContractualEarnings),
                    Tax = tax = _taxservices.TaxAmount(totalEarnings),
                    UnionFee = unionFee = _employeeServices.UnionFees(model.EmployeeId),
                    SLC=studentloan   = _employeeServices.StudentLoadRepaymentAmount(model.EmployeeId, totalEarnings),
                    NIC=nationalinsurance = _nationalInsuranceContributionService.NIContribution(totalEarnings),
                    TotalDeduction = totalDeduction = _paycompuatuationServices.TotalDeduction(tax, nationalinsurance, studentloan, unionFee),
                    NetPayment = _paycompuatuationServices.NetPay(totalEarnings, totalDeduction)
                };
                await _paycompuatuationServices .CreateAsync(payrecord);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.employees = _employeeServices .GetAllEmployeeforPayroll();
            ViewBag.taxYears = _paycompuatuationServices.GetAllTaxYear();
            return View();
        }
    }
}
