using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SharedLayer.Models
{
    public class ReceiveCaseDTO
    {
        [Key]
        [Display(Name = "رقم التقرير")]
        public int ReportID { get; set; }

        [Display(Name = "رقم الملف")]
        public int FileNo { get; set; }

        [Display(Name = "نوع الحالة")]
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        public string StatusType { get; set; }

        [Display(Name = "القرار الصادر")]
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        public string Decision { get; set; }

        [Display(Name = "جهة التنفيذ")]
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        public string ImplementingAuthority { get; set; }

        [Display(Name = "مصدر الحكم")]
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        public string JudgmentSource { get; set; }

        [Display(Name = "تاريخ بدء التنفيذ")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "تاريخ الانتهاء")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "منطوق الحكم أو القرار الصادر")]
        public string Info1 { get; set; }

        [Display(Name = "الجهة المعنية بالتنفيذ")]
        public string Info2 { get; set; }

        [Display(Name = "المدة القانونية بالتنفيذ")]
        public string Info3 { get; set; }

        [Display(Name = "الإجراءات الواجب اتباعها")]
        public string Info4 { get; set; }

        [Display(Name = "العقوبات المترتبة في حال المخالفة")]
        public string Info5 { get; set; }

        [Display(Name = "طريقة التواصل مع جهات التنفيذ و الإشراف")]
        public string Info6 { get; set; }
        
        [Display(Name = "الهدف العام من تطبيق الخدمة المجتمعية")]
        public string Info7 { get; set; }

        [Display(Name = "تم الانشاء بواسطة")]
        public string CreatedBy { get; set; }

        [Display(Name = "تاريخ الانشاء")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime? CreatedDate { get; set; }

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