using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Master
{
    public class AccusationTypeDTO
    {
        [Display(Name = "معرف نوع التهمة بموجب الامر او الحكم الصادر")]
        public int AccusationTypeId { get; set; }

        [Display(Name = "اسم نوع التهمة بموجب الامر او الحكم الصادر")]
        public string AccusationTypeName { get; set; }
    }
}
