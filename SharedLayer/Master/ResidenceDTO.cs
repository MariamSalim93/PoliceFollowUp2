using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Master
{
    public class ResidenceDTO
    {
        [Display(Name = "معرف مقر السكن")]
        public int ResidenceId { get; set; }

        [Display(Name = "اسم مقر السكن")]
        public string ResidenceName { get; set; }
    }
}
