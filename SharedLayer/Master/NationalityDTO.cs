using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Master
{
    public class NationalityDTO
    {
        [Display(Name = "معرف الجنسية")]
        public int NationalityId { get; set; }

        [Display(Name = "اسم الجنسية")]
        public string NationalityName { get; set; }
    }
}
