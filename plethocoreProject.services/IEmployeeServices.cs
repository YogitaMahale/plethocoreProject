using Microsoft.AspNetCore.Mvc.Rendering;
using plethocoreProject.entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace plethocoreProject.services
{
    public interface IEmployeeServices
    {
        Task CreateAsync(Employee newEmployee);
        Employee GetById(int employeeId);
        Task UpdateAsync(Employee employee);
        Task UpdateAsync(int id);
        Task Delete(int employeeId);
        Decimal UnionFees(int id);
        decimal StudentLoadRepaymentAmount(int id, decimal TotalAmt);
        IEnumerable<Employee> GetAll();
        IEnumerable<SelectListItem> GetAllEmployeeforPayroll();
    }
}
