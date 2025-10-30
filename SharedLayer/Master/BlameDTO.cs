using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Master
{
    public class BlameDTO
    {
        [Display(Name = "معرف التهمة")]
        public int BlameId { get; set; }

        [Display(Name = "اسم التهمة")]
        public string BlameName { get; set; }
    }
}
