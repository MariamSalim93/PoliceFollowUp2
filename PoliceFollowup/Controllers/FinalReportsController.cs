using RepositoryLayer.MasterRepo;
using RepositoryLayer.ReportRepo;
using SharedLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PoliceFollowup.Controllers
{
    public class FinalReportsController : Controller
    {
        #region Repositories and Logs

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(ConvictFileController));
        readonly FinalReportsRepo FRRepo = new FinalReportsRepo();
        readonly AuthorityRepo MasterAth = new AuthorityRepo();

        #endregion

        #region Final Reports : View All reports
        public ActionResult FinalReportsList()
        {
            return View();
        }


        [HttpGet]
        public async Task<JsonResult> GetListOfFinalReportsJson()
        {
            var FinalReports = await FRRepo.GetListOfFinalReportsAsync();
            return Json(FinalReports, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region Final Reports : Update Report
        [HttpGet]

        public async Task<ActionResult> UpdateReport(int id)
        {
            ViewBag.AuthorityMaster = await MasterAth.GetListAuthorityAsync();

            var FinalReports = await FRRepo.GetFinalReportsByIdAsync(id);
            if (FinalReports == null)
                return HttpNotFound();


            return View("UpdateReport", FinalReports);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateReport(FinalReportsDTO model)
        {
            ViewBag.AuthorityMaster = await MasterAth.GetListAuthorityAsync();

            model.EditedBy = "مريم سالم الحمادي";
            model.EditDate = DateTime.Now;
            model.Status = "تم التعديل";
            model.ApprovedBy = " ";
            model.ApproveDate = DateTime.Now;
            model.ApproveNotes = " ";

            // Use the key from the model (ReportID) instead of a separate id param
            var existingReport = await FRRepo.GetFinalReportsByIdAsync(model.ReportID);
            if (existingReport == null)
            {
                TempData["ErrorMessage"] = "Report not found";
                return RedirectToAction("FinalReportsList", "FinalReports");
            }


            if (!ModelState.IsValid)
            {
                LogErrors();
                TempData["ErrorMessage"] = "Model validation failed";
                return View("UpdateReport", model);
            }

            await FRRepo.UpdateFinalReportsAsync(model);
            TempData["SuccessMessage"] = "تم تعديل التقرير بنجاح";
            return RedirectToAction("FinalReportsList", "FinalReports"); // adjust if you meant another page
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


        #region Final Reports : Details
        public async Task<ActionResult> FinalReportsDetails(int id)
        {
            var model = await FRRepo.GetFinalReportsByIdAsync(id);


            return View(model);
        }

        #endregion

        public ActionResult AddReport()
        {
            return View();
        }

        public ActionResult RecordsReport()
        {
            var records = new List<FinalReportsDTO>();
            records.AddRange(GetRandomRecords(100));  // Adding 20 random records

            return View(records);

        }

        private List<FinalReportsDTO> GetRandomRecords(int count)
        {
            var rand = new Random();
            var list = new List<FinalReportsDTO>();

            for (int i = 0; i < count; i++)
            {
                list.Add(new FinalReportsDTO
                {
                    ReportID = rand.Next(50000, 100000),
                    CreatedDate = RandomDay(rand),
                
                    CaseNo = rand.Next(20000, 100000).ToString(),
                    ImplementingDate = RandomDay(rand),
                    EndDate = RandomDay(rand),
                    CreatedBy = rand.Next(26000, 27000).ToString(),

                });
            }

            return list;
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




        private DateTime RandomDay(Random rand)
        {
            DateTime start = new DateTime(2010, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(rand.Next(range));
        }
        private string GetRandomUnifiedNo(Random rand)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string digits = "0123456789";

            // Generate 6 digits
            char[] digitChars = new char[6];
            for (int i = 0; i < 6; i++)
            {
                digitChars[i] = digits[rand.Next(digits.Length)];
            }

            // Generate 3 letters
            char[] letterChars = new char[3];
            for (int i = 0; i < 3; i++)
            {
                letterChars[i] = letters[rand.Next(letters.Length)];
            }

            // Combine and shuffle
            var allChars = digitChars.Concat(letterChars).ToList();
            allChars = allChars.OrderBy(x => rand.Next()).ToList();

            return new string(allChars.ToArray());
        }

        public ActionResult Details()
        {
            return View();
        }
    }
}