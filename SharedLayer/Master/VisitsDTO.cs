using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Master
{
    public class VisitsDTO
    {
        [Display(Name = "معرف الزيارات التنفيذية")]
        public int VisitsId { get; set; }

        [Display(Name = "اسم الزيارات التنفيذية")]
        public string VisitsName { get; set; }
    }
}
