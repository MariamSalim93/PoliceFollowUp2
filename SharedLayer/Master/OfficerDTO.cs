using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Master
{
    public class OfficerDTO
    {
        [Display(Name = "معرف الضابط المسؤول")]
        public int OfficerId { get; set; }

        [Display(Name = "اسم الضابط المسؤول")]
        public string OfficerName { get; set; }
    }
}
