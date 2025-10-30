using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Master
{
    public class DeviceNoDTO
    {
        [Display(Name = "معرف رقم الجهاز")]
        public int DeviceNoId { get; set; }

        [Display(Name = "رقم الجهاز")]
        public string DeviceNoName { get; set; }
    }
}
