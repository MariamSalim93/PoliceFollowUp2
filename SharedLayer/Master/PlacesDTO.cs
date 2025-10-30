using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Master
{
    public class PlacesDTO
    {
        [Display(Name = "معرف اماكن تطبيق أعمال الخدمة المجتمعية")]
        public int PlacesId { get; set; }

        [Display(Name = "اسم اماكن تطبيق أعمال الخدمة المجتمعية")]
        public string PlacesName { get; set; }
    }
}
