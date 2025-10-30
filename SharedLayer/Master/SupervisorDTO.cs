using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Master
{
    public class SupervisorDTO
    {
        [Display(Name = "معرف المشرف")]
        public int SupervisorId { get; set; }

        [Display(Name = "اسم المشرف")]
        public string SupervisorName { get; set; }
    }
}
