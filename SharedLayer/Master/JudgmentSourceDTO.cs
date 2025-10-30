using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Master
{
    public class JudgmentSourceDTO
    {

        [Display(Name = "معرف مصدر الحكم")]
        public int JudgmentSourceId { get; set; }

        [Display(Name = "اسم مصدر الحكم")]
        public string JudgmentSourceName { get; set; }
    }
}
