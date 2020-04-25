using plethocoreProject.entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace plethocoreProject.Models
{
    public class PaymentRecordDetailsViewModel
    {
        public int Id { get; set; }
       
        public int EmployeeId { get; set; }
        [Display(Name = "Full Name")]
        public Employee Employee { get; set; }
        public string FullName { get; set; }
        public string NiNo { get; set; }
        [DataType(DataType.Date), Display(Name = "Pay Date")]
        public DateTime PayDate { get; set; } 
        [Display(Name = "Pay Month")]
        public string PayMonth { get; set; }  
        [Display(Name = "Tax Year")]
        public int TaxYearId { get; set; }
        public string Year { get; set; }
        public TaxYear TaxYear { get; set; }
        [Display(Name = "Tax Code")]
        public string TaxCode { get; set; } 
        [Display(Name = "Hourly Rate")]
        public decimal HourlyRate { get; set; }
        [Display(Name = "Hours Worked")]
        public decimal HoursWorked { get; set; }
        [Display(Name = "Contractual Hours")]
        public decimal ContractualHours { get; set; }
        [Display(Name = "Over Time Hours")]
        public decimal OvertimeHours { get; set; }


        [Display(Name = "Over Time Rate")]
        public decimal OvertimeRate { get; set; }

        [Display(Name = "ContractualEarnings")]
        public decimal ContractualEarnings { get; set; }
        public decimal OvertimeEarnings { get; set; }
        public decimal Tax { get; set; }
        public decimal NIC { get; set; }
        [Display(Name = "UnionFee")]
        public decimal? UnionFee { get; set; }
        public Nullable<decimal> SLC { get; set; }
        public decimal TotalEarnings { get; set; }
        public decimal TotalDeduction { get; set; }
        public decimal NetPayment { get; set; }
    }
}
