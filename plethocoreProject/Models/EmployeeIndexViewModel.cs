using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace plethocoreProject.Models
{
    public class EmployeeIndexViewModel
    {
        public int Id { get; set; }
        public string EmployeeNo { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateofJoined { get; set; }
        public string Designation { get; set; }
        public string city { get; set; }

    }
}
