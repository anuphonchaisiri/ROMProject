using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMLibrary.Model.Company
{
    public class CompanyInfoModel : StructureController
    {
        public string sid { get; set; }
        public string name { get; set; }
        public string description { get; set; } 
        public string remark { get; set; }
    }
}
