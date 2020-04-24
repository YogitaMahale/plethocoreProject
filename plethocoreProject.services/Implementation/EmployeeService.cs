using plethocoreProject.entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
 
using plethocoreProject.persistence;
using System.Linq;
using plethocoreProject.services;

namespace paycompute.services.Implementation
{
    public class EmployeeService : IEmployeeServices
    {
        private decimal studentLoanAmount;

        private readonly ApplicationDbContext _context;
        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async  Task CreateAsync(Employee newEmployee)
        {
          await  _context.Employees.AddAsync(newEmployee);
            await _context.SaveChangesAsync();
        }

        public async  Task Delete(int employeeId)
        {
            Employee obj= _context.Employees.Where(x => x.Id == employeeId).FirstOrDefault();
             _context.Employees.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Employee> GetAll() => _context.Employees;

        public Employee GetById(int employeeId) => _context.Employees.Where(x => x.Id == employeeId).FirstOrDefault();

        public async  Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async  Task UpdateAsync(int id)
        {
            var emp = GetById(id);
            _context.Employees.Update(emp);
            await _context.SaveChangesAsync();
        }

        public decimal StudentLoadRepaymentAmount(int id, decimal TotalAmt)
        {
            var employee = GetById(id);
            if (employee.studentLoan  == StudentLoan.Yes && TotalAmt > 1750 && TotalAmt < 2000)
            {
                studentLoanAmount = 15m;
            }
            else if (employee.studentLoan == StudentLoan.Yes && TotalAmt >= 2000 && TotalAmt < 2250)
            {
                studentLoanAmount = 38m;
            }
            else if (employee.studentLoan == StudentLoan.Yes && TotalAmt >= 2250 && TotalAmt < 2500)
            {
                studentLoanAmount = 60m;
            }
            else if (employee.studentLoan == StudentLoan.Yes && TotalAmt >= 2500)
            {
                studentLoanAmount = 83m;
            }
            else
            {
                studentLoanAmount = 0m;
            }
            return studentLoanAmount;
        }

        public decimal UnionFees(int id)
        {
            throw new NotImplementedException();
        }

     
    }
}
