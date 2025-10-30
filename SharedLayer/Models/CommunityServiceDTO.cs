using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Models
{
    public class CommunityServiceDTO
    {
        [Display(Name = "رقم التقرير")]
        public int ReportID { get; set; }

        [Display(Name = "رقم الملف")]
        public int FileNo { get; set; }

        [Display(Name = "رقم القضية")]
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        public string CaseNo { get; set; }
      
        [Display(Name = "تاريخ الحكم")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        public DateTime? JudgmentDate { get; set; }

        [Display(Name = "اماكن تطبيق أعمال الخدمة المجتمعية")]
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        public string CommunityServicePlaces { get; set; }

        [Display(Name = "اسم المشرف")]
        public string SupervisorName { get; set; }

        [Display(Name = "الضابط المسؤول")]
        public string OfficerName { get; set; }

        [Display(Name = "التهمة")]
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        public string Blame { get; set; }

        [Display(Name = "الحالة الجسدية والنفسية")]
        public string PhysicalPychologicalStatus { get; set; }

        [Display(Name = "الزيارات التنفيذية")]
        public string ExecutiveVisits { get; set; }

        [Display(Name = "تاريخ البدء")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "تاريخ الانتهاء")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime? EndDate { get; set; }

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