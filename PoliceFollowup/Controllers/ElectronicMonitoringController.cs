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
    public class ElectronicMonitoringController : Controller
    {
        #region Repositories and Logs

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(ConvictFileController));
        readonly ElectronicMonitoringRepo EMRepo = new ElectronicMonitoringRepo();
        readonly JudgmentSourceRepo MasterJS = new JudgmentSourceRepo();
        readonly JudgmentTypeRepo MasterJT = new JudgmentTypeRepo();
        readonly MonitoringAreaRepo MasterM = new MonitoringAreaRepo();
        readonly BlameRepo MasterB = new BlameRepo();

        #endregion

        #region Electronic Monitoring : View All reports
        public ActionResult ElectronicMonitoringList()
        {
            return View();
        }


        [HttpGet]
        public async Task<JsonResult> GetListOfElectronicMonitoringJson()
        {
            var ElectronicMonitoring = await EMRepo.GetListOfElectronicMonitoringAsync();
            return Json(ElectronicMonitoring, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Electronic Monitoring : Add new Report

        public ActionResult AddElectronicMonitoring()

        {

            var model = new ElectronicMonitoringDTO
            {
                CreatedDate = DateTime.Now,
                CreatedBy = "مريم سالم الحمادي",
                Status = "جديد",

                EditNote = " ",
                EditedBy = " "


            };
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> AddElectronicMonitoring(ElectronicMonitoringDTO EM)
        {

            try
            {
                EM.CreatedDate = DateTime.Now;
                EM.CreatedBy = string.IsNullOrWhiteSpace(EM.CreatedBy) ? "مريم سالم الحمادي" : EM.CreatedBy;
                EM.EditDate = DateTime.Now;
                EM.EditedBy = string.IsNullOrWhiteSpace(EM.EditedBy) ? " " : EM.EditedBy;
                EM.EditNote = string.IsNullOrWhiteSpace(EM.EditNote) ? " " : EM.EditNote;
                EM.ApproveDate = DateTime.Now;
                EM.ApprovedBy = string.IsNullOrWhiteSpace(EM.ApprovedBy) ? " " : EM.ApprovedBy;
                EM.ApproveNotes = string.IsNullOrWhiteSpace(EM.ApproveNotes) ? " " : EM.ApproveNotes;

                #region Logs

                log4net.LogicalThreadContext.Properties["Controller"] = this.ControllerContext.RouteData.Values["controller"];
                log4net.LogicalThreadContext.Properties["Action"] = this.ControllerContext.RouteData.Values["action"];
                log4net.LogicalThreadContext.Properties["UserName"] = "HAJER EISA";

                #endregion

                if (ModelState.IsValid)
                {

                    await EMRepo.AddElectronicMonitoringAsync(EM);

                    ViewBag.Success = "تم إضافة البيانات بنجاح!";
                    Log.Info("success");

                    return RedirectToAction("ElectronicMonitoringList", "CommunityService"); ;
                }
                else
                {

                    foreach (var state in ModelState)
                    {
                        foreach (var error in state.Value.Errors)
                        {
                            System.Diagnostics.Debug.WriteLine($"Field: {state.Key}, Error: {error.ErrorMessage}");
                        }
                    }

                    TempData["CommunityServiceErrorMessage"] = "يرجى التحقق من جميع الحقول قبل الإدراج!";
                    return View(EM);
                }
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while adding Convict File.", ex);
                ViewBag.error = ex.Message;
                return RedirectToAction("Error", "NotFound");
            }
        }


        #endregion

        #region Electronic Monitoring : Update Report
        [HttpGet]

        public async Task<ActionResult> UpdateReport(int id)
        {
            ViewBag.JudgmentSourceMaster = await MasterJS.GetListJudgmentSourceAsync();
            ViewBag.JudgmentTypeMaster = await MasterJT.GetListJudgmentTypeAsync();
            ViewBag.MonitoringAreaMaster = await MasterM.GetListMonitoringAreaAsync();
            ViewBag.BlameMaster = await MasterB.GetListBlameAsync();

            var ElectronicMonitoring = await EMRepo.GetElectronicMonitoringByIdAsync(id);
            if (ElectronicMonitoring == null)
                return HttpNotFound();


            return View("UpdateReport", ElectronicMonitoring);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateReport(ElectronicMonitoringDTO model)
        {
            ViewBag.JudgmentSourceMaster = await MasterJS.GetListJudgmentSourceAsync();
            ViewBag.JudgmentTypeMaster = await MasterJT.GetListJudgmentTypeAsync();
            ViewBag.MonitoringAreaMaster = await MasterM.GetListMonitoringAreaAsync();
            ViewBag.BlameMaster = await MasterB.GetListBlameAsync();

            model.EditedBy = "مريم سالم الحمادي";
            model.EditDate = DateTime.Now;
            model.Status = "تم التعديل";
            model.ApprovedBy = " ";
            model.ApproveDate = DateTime.Now;
            model.ApproveNotes = " ";

            // Use the key from the model (ReportID) instead of a separate id param
            var existingReport = await EMRepo.GetElectronicMonitoringByIdAsync(model.ReportID);
            if (existingReport == null)
            {
                TempData["ErrorMessage"] = "Report not found";
                return RedirectToAction("ElectronicMonitoringList", "ElectronicMonitoring");
            }


            if (!ModelState.IsValid)
            {
                LogErrors();
                TempData["ErrorMessage"] = "Model validation failed";
                return View("UpdateReport", model);
            }

            await EMRepo.UpdateElectronicMonitoringAsync(model);
            TempData["SuccessMessage"] = "تم تعديل التقرير بنجاح";
            return RedirectToAction("ElectronicMonitoringList", "ElectronicMonitoring"); // adjust if you meant another page
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


        #region Electronic Monitoring : Details
        public async Task<ActionResult> ElectronicMonitoringDetails(int id)
        {
            var model = await EMRepo.GetElectronicMonitoringByIdAsync(id);


            return View(model);
        }

        #endregion

        #region Electronic Monitoring : View oneWeek
        public ActionResult OneWeekList()
        {
            return View();
        }


        [HttpGet]
        public async Task<JsonResult> GetListOfOneWeekJson()
        {
            var ElectronicMonitoring = await EMRepo.GetListOfElectronicMonitoringAsync();
            return Json(ElectronicMonitoring, JsonRequestBehavior.AllowGet);
        }

        #endregion
        public ActionResult AddMonitor1()
        {
            return View();
        }


        public ActionResult RecordsCase()
        {
            var records = new List<ElectronicMonitoringDTO>();
            records.AddRange(GetRandomRecords(100));  // Adding 20 random records

            return View(records);

        }

        private List<ElectronicMonitoringDTO> GetRandomRecords(int count)
        {
            var rand = new Random();
            var list = new List<ElectronicMonitoringDTO>();

            for (int i = 0; i < count; i++)
            {
                list.Add(new ElectronicMonitoringDTO
                {
                    ReportID = rand.Next(50000, 100000),
                    CreatedDate = RandomDay(rand),
                    JudgmentType = GetRandomJudgmentType(rand),
                    CaseNo = rand.Next(20000, 100000).ToString(),
                    StartMonitoringDate = RandomDay(rand),
                    EndMonitoringDate = RandomDay(rand),
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


      

        private string GetRandomEmiratesID(Random rand)
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
        private string GetRandomJudgmentType(Random rand)
        {
            string[] judgmentType = {

        "حبس احتياطي",
        "حكم قضائي",
        "بقوة القانون"
       
    };

            return judgmentType[rand.Next(judgmentType.Length)];
        }
        private DateTime RandomDay(Random rand)
        {
            DateTime start = new DateTime(2010, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(rand.Next(range)).Date;
        }


        public ActionResult Details()
        {
            return View();
        }
    }
}