using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RepositoryLayer.MasterRepo;
using RepositoryLayer.ReportRepo;
using SharedLayer.Models;

namespace PoliceFollowup.Controllers
{
    public class ReceiveCaseController : Controller
    {
        #region Repositories and Logs

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(ConvictFileController));
        readonly ReceiveCaseRepo RCRepo = new ReceiveCaseRepo();
        readonly DecisionRepo MasterD = new DecisionRepo();
        readonly AuthorityRepo MasterAth = new AuthorityRepo();
        readonly JudgmentSourceRepo MasterJS = new JudgmentSourceRepo();

        #endregion


        # region ReceiveCase : View All reports
        public ActionResult ReceiveCaseList()
        {
            return View();
        }


        [HttpGet]
        public async Task<JsonResult> GetListOfReceiveCaseJson()
        {
            var ReceiveCase = await RCRepo.GetListOfReceiveCaseAsync();
            return Json(ReceiveCase, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ReceiveCase : Update Report
        [HttpGet]

        public async Task<ActionResult> UpdateReport(int id)
        {
            ViewBag.DecisionMaster = await MasterD.GetListDecisionAsync();
            ViewBag.AuthorityMaster = await MasterAth.GetListAuthorityAsync();
            ViewBag.JudgmentSourceMaster = await MasterJS.GetListJudgmentSourceAsync();

            var ReceiveCase = await RCRepo.GetReceiveCaseByIdAsync(id);
            if (ReceiveCase == null)
                return HttpNotFound();


            return View("UpdateReport", ReceiveCase);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateReport(ReceiveCaseDTO model)
        {
            ViewBag.DecisionMaster = await MasterD.GetListDecisionAsync();
            ViewBag.AuthorityMaster = await MasterAth.GetListAuthorityAsync();
            ViewBag.JudgmentSourceMaster = await MasterJS.GetListJudgmentSourceAsync();

            model.EditedBy = "مريم سالم الحمادي";
            model.EditDate = DateTime.Now;
            model.Status = "تم التعديل";
            model.ApprovedBy = " ";
            model.ApproveDate = DateTime.Now;
            model.ApproveNotes = " ";

            // Use the key from the model (ReportID) instead of a separate id param
            var existingReport = await RCRepo.GetReceiveCaseByIdAsync(model.ReportID);
            if (existingReport == null)
            {
                TempData["ErrorMessage"] = "Report not found";
                return RedirectToAction("ReceiveCaseList", "ReceiveCase");
            }


            if (!ModelState.IsValid)
            {
                LogErrors();
                TempData["ErrorMessage"] = "Model validation failed";
                return View("UpdateReport", model);
            }

            await RCRepo.UpdateReceiveCaseAsync(model);
            TempData["SuccessMessage"] = "تم تعديل التقرير بنجاح";
            return RedirectToAction("ReceiveCaseList", "ReceiveCase"); // adjust if you meant another page
        }




        private void LogErrors()
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Log.Error(error.ErrorMessage);
            }
        }

        #endregion


        #region ReceiveCase : Details
        public async Task<ActionResult> ReceiveCaseDetails(int id)
        {
            var model = await RCRepo.GetReceiveCaseByIdAsync(id);
            if (model == null)
            {
                TempData["Error"] = "لم يتم العثور على التقرير";
                return RedirectToAction("ReceiveCaseList");
            }

            // تعبئة افتراضية للعرض الأولي في صفحة التفاصيل
            if (model.ApproveDate == null) model.ApproveDate = DateTime.Now;
            if (string.IsNullOrWhiteSpace(model.ApprovedBy)) model.ApprovedBy = "مريم سالم الحمادي";

            return View(model);
        }

        #endregion




        #region Visit : Aprove

        // ReceiveCaseController.cs

        public async Task<ActionResult> Approve(int id)
        {
            try
            {
                // Properly await the async method
                var existingCase = await RCRepo.GetReceiveCaseByIdAsync(id);

                if (existingCase == null)
                {
                    TempData["Error"] = "لم يتم العثور على التقرير";
                    return RedirectToAction("ReceiveCaseList");
                }

                // Create the approval model with the ReportID
                var approveModel = new ReceiveCaseDTO
                {
                    ReportID = id, // IMPORTANT: Set the ReportID
                    FileNo = existingCase.FileNo,
                    ApproveDate = DateTime.Now,
                    ApprovedBy = "مريم سالم الحمادي",

                };

                return View(approveModel);
            }
            catch (Exception ex)
            {
                log4net.LogicalThreadContext.Properties["Controller"] = "ReceiveCase";
                log4net.LogicalThreadContext.Properties["Action"] = "Approve";
                log4net.LogicalThreadContext.Properties["Error"] = ex.Message;

                TempData["Error"] = "حدث خطأ في تحميل البيانات";
                return RedirectToAction("ReceiveCaseList");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Approve(ReceiveCaseDTO rc)
        {
            try
            {
                // Validate ReportID
                if (rc.ReportID <= 0)
                {
                    TempData["Error"] = "رقم التقرير غير صحيح";
                    return RedirectToAction("ReceiveCaseList");
                }

                // Set default values if not provided
                rc.ApprovedBy = string.IsNullOrWhiteSpace(rc.ApprovedBy) ? "مريم سالم الحمادي" : rc.ApprovedBy;
                rc.ApproveDate = rc.ApproveDate ?? DateTime.Now;

                #region Logs
                log4net.LogicalThreadContext.Properties["Controller"] = this.ControllerContext.RouteData.Values["controller"];
                log4net.LogicalThreadContext.Properties["Action"] = this.ControllerContext.RouteData.Values["action"];
                log4net.LogicalThreadContext.Properties["UserName"] = "HAJER EISA";
                log4net.LogicalThreadContext.Properties["ReportID"] = rc.ReportID;
                #endregion

                await RCRepo.ApproveReceiveCaseAsync(rc);

                TempData["Success"] = "تم اعتماد التقرير بنجاح";
                return RedirectToAction("ReceiveCaseDetails", new { id = rc.ReportID });
            }
            catch (Exception ex)
            {
                log4net.LogicalThreadContext.Properties["Error"] = ex.Message;

                ModelState.AddModelError("", "حدث خطأ أثناء اعتماد التقرير: " + ex.Message);
                TempData["Error"] = "حدث خطأ ما أثناء اعتماد التقرير";

                // Return the view with the model to show errors
                return View(rc);
            }
        }

        #endregion


        public ActionResult AddCase()
        {
            return View();
        }


        public ActionResult RecordsCase()
        {
            var records = new List<ReceiveCaseDTO>();
            records.AddRange(GetRandomRecords(100));  // Adding 20 random records

            return View(records);

        }

        private List<ReceiveCaseDTO> GetRandomRecords(int count)
        {
            var rand = new Random();
            var list = new List<ReceiveCaseDTO>();

            for (int i = 0; i < count; i++)
            {
                list.Add(new ReceiveCaseDTO
                {
                    ReportID = rand.Next(50000, 100000),
                    CreatedDate = RandomDay(rand),
                    StatusType = GetRandomStatusType(rand),
                    CreatedBy = rand.Next(26000, 27000).ToString(),

                });
            }

            return list;
        }

        private string GetRandomStatusType(Random rand)
        {
            string[] statusType = {
        "مراقبة الكترونية",
        "خدمة مجتمعية", 
    };

            return statusType[rand.Next(statusType.Length)];
        }
          private string GetRandomName(Random rand)
        {
            string[] names = {
        "عبدالله علي ناصر",
        "أمجد محمد أمجد",
        "عبدالوهاب علي أحمد",
        "يوسف سلطان محمد",
        "خالد عبدالعزيز سالم",
        "عمر محمود عبدالرحمن",
        "سالم إبراهيم حسين",
        "محمد طارق عبدالكريم",
        "إبراهيم فهد عبدالله",
        "حسن سعيد علي",
        "أحمد عبدالرحمن الفضلي",
        "علي ناصر أحمد",
        "سعيد راشد سالم",
        "عبدالله خالد القحطاني",
        "يوسف جمال العتيبي",
        "عبدالرحمن فيصل عبدالله",
        "أحمد حسين يوسف",
        "علي عبدالله إبراهيم",
        "طارق زيد عبدالرحمن",
        "سالم جمعة خالد"
    };

            return names[rand.Next(names.Length)];
        }



        private string GetRandomNationality(Random rand)
        {
            string[] nationalities = {

        "الامارات",
        "السعودية",
        "عمان",
        "الكويت",
        "البحرين",
        "قطر",
        "مصر",
        "الاردن",
         "العراق",
        "لبنان",
        "فلسطين"
    };

            return nationalities[rand.Next(nationalities.Length)];
        }


        private string GetRandomPhone(Random rand)
        {
            // List of allowed prefixes
            string[] prefixes = { "050", "052", "054", "055" };

            // Select one prefix at random
            string prefix = prefixes[rand.Next(prefixes.Length)];

            // Generate 7 random digits
            int randomNumber = rand.Next(1000000, 10000000); // 7 digits

            // Combine prefix and random number
            return $"{prefix}{randomNumber}";
        }

        private string GetRandomUnifiendNo(Random rand)
        {
            // Start with "784"
            string prefix = "784";

            // Randomly select a year between 1950 and 2006
            int year = rand.Next(1950, 2010);

            // Generate a random number with exactly 9 digits
            int randomNumber = rand.Next(100000000, 1000000000); // 9 digits

            // Combine them into the ID number and ensure year is 4 digits
            return $"{prefix}{year:D4}{randomNumber:D9}";
        }

        private DateTime RandomDay(Random rand)
        {
            DateTime start = new DateTime(2010, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(rand.Next(range));
        }

        public ActionResult Details()
        {
            return View();
        }
    }
}