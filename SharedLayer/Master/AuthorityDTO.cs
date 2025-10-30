using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Master
{
    public class AuthorityDTO
    {
        [Display(Name = "معرف جهة التنفيذ")]
        public int AuthorityId { get; set; }

        [Display(Name = "اسم جهة التنفيذ")]
        public string AuthorityName { get; set; }
    }
}
