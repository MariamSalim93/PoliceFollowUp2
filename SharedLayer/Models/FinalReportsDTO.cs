using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SharedLayer.Models
{
    public class FinalReportsDTO
    {
        [Display(Name = "رقم التقرير")]
        public int ReportID { get; set; }

        [Display(Name = "رقم الملف")]
        public int FileNo { get; set; }

        [Display(Name = "رقم القضية")]
        public string CaseNo { get; set; }

        [Display(Name = "جهة التنفيذ")]
        public string ImplementingAuthority { get; set; }

        [Display(Name = "تاريخ التنفيذ")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime? ImplementingDate { get; set; }

        [Display(Name = "تاريخ الانتهاء")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "موجز التقرير النهائي")]
        public string ReportSummary { get; set; }
        [Display(Name = "تم الانشاء بواسطة")]
        public string CreatedBy { get; set; }

        [Display(Name = "تاريخ الانشاء")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "تم التعديل بواسطة")]
        public string EditedBy { get; set; }

        [Display(Name = "تاريخ التعديل")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime? EditDate { get; set; }

        [Display(Name = "ملاحظات التعديل")]
        public string EditNote { get; set; }

        [Display(Name = "الحالة")]
        public string Status { get; set; }

        [Display(Name = "تم الاعتماد بواسطة")]
        public string ApprovedBy { get; set; }

        [Display(Name = "تاريخ الاعتماد")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime? ApproveDate { get; set; }

        [Display(Name = "ملاحظات الاعتماد")]
        public string ApproveNotes { get; set; }


    }
}