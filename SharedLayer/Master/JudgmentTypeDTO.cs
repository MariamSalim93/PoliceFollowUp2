using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Master
{
    public class JudgmentTypeDTO
    {
        [Display(Name = "معرف نوع الحكم")]
        public int JudgmentTypeId { get; set; }

        [Display(Name = "اسم نوع الحكم")]
        public string JudgmentTypeName { get; set; }
    }
}
