using COMUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMLibrary.Model
{
    public class StructureController
    {
        [Key]
        [Column(Order = 1)]
        [Display(Name = "รหัสบริษัท")]
        public string sid { get; set; }
        [NotMapped]
        [Display(Name = "รหัสองค์กร")]
        public string company_code { get; set; }
        [Display(Name = "สร้างโดย")]
        public string created_by { get; set; }
        [Display(Name = "สร้างข้อมูลเมื่อ")]
        public string created_on { get; set; }
        [Display(Name = "แก้ไขโดย")]
        public string updated_by { get; set; }
        [Display(Name = "แก้ไขข้อมูลเมื่อ")]
        public string updated_on { get; set; }



        private string _created_on_display { get; set; }
        private string _updated_on_display { get; set; }
        

        [NotMapped]
        [Display(Name = "ผู้สร้าง")]
        public string created_by_name { get; set; }
        [NotMapped]
        [Display(Name = "ผู้แก้ไข")]
        public string updated_by_name { get; set; }
        [NotMapped]
        [Display(Name = "สร้างข้อมูลเมื่อ")]
        public string created_on_display
        {
            get
            {
                return !string.IsNullOrEmpty(_created_on_display) ? _created_on_display :  !string.IsNullOrEmpty(this.created_on) ? ObjectUtil.Convert2DateTimeDisplay(this.created_on) : "";
            }
            set{ _created_on_display = value; }
        }

        
        [NotMapped]
        [Display(Name = "แก้ไขข้อมูลเมื่อ")]
        public string updated_on_display
        {
            get
            {
                return !string.IsNullOrEmpty(_updated_on_display) ? _updated_on_display :  !string.IsNullOrEmpty(this.updated_on) ? ObjectUtil.Convert2DateTimeDisplay(this.updated_on) : "";
            }
            set{ _updated_on_display = value; }
        }
        
    }
}
