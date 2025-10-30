using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Master
{
    public class MonitoringAreaDTO
    {
        [Display(Name = "معرف نطاق المراقبة")]
        public int MonitoringAreaId { get; set; }

        [Display(Name = "اسم نطاق المراقبة")]
        public string MonitoringAreaName { get; set; }
    }
}
