using Microsoft.AspNetCore.Mvc.Rendering;
using plethocoreProject.entity;
using plethocoreProject.persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plethocoreProject.services.Implementation
{
    public class paycompuatuationServices:IpaycompuatuationServices
    {
        private decimal ContractualEarning;
        private decimal OverTimeHours;
        public readonly ApplicationDbContext _context;
        public paycompuatuationServices(ApplicationDbContext context)
        {

            _context = context;
        }


        public decimal ContractualEarnings(decimal contractualHours, decimal hoursWorked, decimal hourlyRate)
        {
            if (hoursWorked < contractualHours)
            {
                ContractualEarning = hoursWorked * hourlyRate;
            }
            else
            {
                ContractualEarning = contractualHours * hourlyRate;
            }
            return ContractualEarning;
        }


        public async Task CreateAsync(PaymentRecord paymentRecord)
        {
            await _context.PaymentRecords.AddAsync(paymentRecord);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<PaymentRecord> GetAll() => _context.PaymentRecords.OrderBy(p => p.EmployeeId);

        public IEnumerable<SelectListItem> GetAllTaxYear()
        {
            var alltaxyear = _context.TaxYears.Select(TaxYear => new SelectListItem
            {
                Text = TaxYear.YearofTax,
                Value = TaxYear.Id.ToString()
            }
            );
            return alltaxyear;
        }

        public PaymentRecord GetById(int id)
       =>        _context.PaymentRecords.Where(pay => pay.Id == id).FirstOrDefault();

        public decimal NetPay(decimal totalEarnings, decimal totalDeduction) => totalEarnings - totalDeduction;

        public decimal OvertimeEarnings(decimal overtimeRate, decimal overtimeHours)
       =>overtimeRate* overtimeHours;

        public decimal OvertimeHours(decimal hoursWorked, decimal contractualHours)
        {
        if(hoursWorked<=contractualHours)
            {
                OverTimeHours = 0.00m;
            }
        else if(hoursWorked <= contractualHours)
            {
                OverTimeHours = hoursWorked - contractualHours;
            }
            return OverTimeHours;
        }


        public decimal OvertimeRate(decimal hourlyRate)
        => hourlyRate * 1.5m;

        public decimal TotalDeduction(decimal tax, decimal nic, decimal studentLoanRepayment, decimal unionFees)
      => tax + studentLoanRepayment + unionFees;
        public decimal TotalEarnings(decimal overtimeEarnings, decimal contractualEarnings)
        => overtimeEarnings + contractualEarnings;
        public TaxYear GetTaxYearById(int id)
           => _context.TaxYears.Where(year => year.Id == id).FirstOrDefault();




    }
}
