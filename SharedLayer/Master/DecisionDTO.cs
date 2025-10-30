using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Master
{
    public class DecisionDTO
    {
        [Display(Name = "معرف الحكم أو القرار الصادر")]
        public int DecisionId { get; set; }

        [Display(Name = "اسم الحكم أو القرار الصادر")]
        public string DecisionName { get; set; }
    }
}
