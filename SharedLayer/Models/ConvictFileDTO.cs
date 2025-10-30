using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Models
{
    public class ConvictFileDTO
    {
        [Key]
        [Display(Name = "رقم التقرير")]
        public int ReportID { get; set; }


        [Display(Name = "الاسم")]
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        public string Name { get; set; }

        [Display(Name = "الاسم الوهمي")]
        public string FakeName { get; set; }

        [Display(Name = "الجنسية")]
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        public string Nationality { get; set; }

        [Display(Name = "الجنس")]
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        public string Gender { get; set; }

        [Display(Name = "التاريخ ")]
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "العمر")]
        public int? Age { get; set; }

        [Display(Name = "المستوى التعليمي")]
        public string EducationalLevel { get; set; }

        [Display(Name = "الاعاقة")]
        public string Disability { get; set; }


        [Display(Name = "الحالة الاجتماعية")]
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        public string MaritalStatus { get; set; }

        [Display(Name = "المهنة")]
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        public string Profession { get; set; }

        [Display(Name = "مقر السكن")]
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        public string Residence { get; set; }

        [Display(Name = "الرقم الموحد")]
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        public string UnifiedNo { get; set; }

        [Display(Name = "رقم الهوية")]
        public string EmiratesID { get; set; }

        [Display(Name = "رقم الهاتف")]
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        public string Phone { get; set; }

        [Display(Name = "الملاحظات")]
        public string Notes { get; set; }

        [Display(Name = "المرفقات")]
        public string Attachments { get; set; }

        [Display(Name = "تم الانشاء بواسطة")]
        public string CreatedBy { get; set; }

        [Display(Name = "تاريخ الانشاء ")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "تم التعديل بواسطة")]
        public string EditedBy { get; set; }

        [Display(Name = "تاريخ التعديل ")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime? EditDate { get; set; }

        [Display(Name = "ملاحظات التعديل")]
        public string EditNote { get; set; }

        [Display(Name = "الحالة")]
        public string Status { get; set; }

        [Display(Name = "تم الاعتماد بواسطة")]
        public string ApprovedBy { get; set; }

        [Display(Name = "تاريخ الاعتماد ")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime? ApproveDate { get; set; }

        [Display(Name = "ملاحظات الاعتماد")]
        public string ApproveNotes { get; set; }

    }
}
