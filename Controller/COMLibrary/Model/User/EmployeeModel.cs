using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMLibrary.Model.User
{
    [Table("COM_MASTER_EMPLOYEE_MAPPING")]
    public class EmployeeModel : StructureController
    {
        public string description { get; set; }
        public string userid { get; set; }
        public string username { get; set; }
    }
}
