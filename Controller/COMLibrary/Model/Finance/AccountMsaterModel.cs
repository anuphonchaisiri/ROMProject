using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMLibrary.Model.Finance
{
    [Table("com_master_account")]
    public class AccountMsaterModel : StructureController
    {
        [Key]
        [Column(Order = 2)]
        [Display(Name = "รหัสบัญชี")]
        public string account_code { get; set; }

        [Display(Name = "ชื่อบัญชี/คำอธิบาย")]
        public string account_name { get; set; }
    }
}
