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
    public class CommunityServiceController : Controller
    {
        #region Repositories and Logs

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(ConvictFileController));
        readonly CommunityServiceRepo CSRepo = new CommunityServiceRepo();
        readonly BlameRepo MasterB = new BlameRepo();
        readonly VisitsRepo MasterV = new VisitsRepo();
        readonly SupervisorRepo MasterS = new SupervisorRepo();
        readonly OfficerRepo MasterO = new OfficerRepo();
        readonly PlacesRepo MasterP = new PlacesRepo();

        #endregion

        #region Community Service : View All reports
        public ActionResult CommunityServiceList()
        {
            return View();
        }


        [HttpGet]
        public async Task<JsonResult> GetListOfCommunityServiceJson()
        {
            var CommunityService = await CSRepo.GetListOfCommunityServiceAsync();
            return Json(CommunityService, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Community Service : Add new Report

        public ActionResult AddCommunityService()

        {

            var model = new CommunityServiceDTO
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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddCommunityService(CommunityServiceDTO CS)
        {
            try
            {
                CS.CreatedDate = DateTime.Now;
                CS.CreatedBy = string.IsNullOrWhiteSpace(CS.CreatedBy) ? "مريم سالم الحمادي" : CS.CreatedBy;
                CS.EditDate = DateTime.Now;
                CS.ApproveDate = DateTime.Now;

                if (!ModelState.IsValid)
                {
                    TempData["Error"] = "يرجى التحقق من جميع الحقول قبل الإدراج!";
                    return View(CS); // same request -> ViewBag visible
                }

                await CSRepo.AddCommunityServiceAsync(CS);

                TempData["Success"] = "تم إضافة البيانات بنجاح!";
                ModelState.Clear();                 // so the form fields reset
                return View(new CommunityServiceDTO()); // stay on same page with success msg
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View(CS);
            }
        }


        #endregion

        #region Community Service : Update Report
        [HttpGet]

        public async Task<ActionResult> UpdateReport(int id)
        {
            ViewBag.OfficerMaster = await MasterO.GetListOfficerAsync();
            ViewBag.PlacesMaster = await MasterP.GetListPlacesAsync();
            ViewBag.SupervisorMaster = await MasterS.GetListSupervisorAsync();
            ViewBag.VisitsMaster = await MasterV.GetListVisitsAsync();
            ViewBag.BlameMaster = await MasterB.GetListBlameAsync();

            var CommunityService = await CSRepo.GetCommunityServiceByIdAsync(id);
            if (CommunityService == null)
                return HttpNotFound();


            return View("UpdateReport", CommunityService);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateReport(CommunityServiceDTO model)
        {
            ViewBag.OfficerMaster = await MasterO.GetListOfficerAsync();
            ViewBag.PlacesMaster = await MasterP.GetListPlacesAsync();
            ViewBag.SupervisorMaster = await MasterS.GetListSupervisorAsync();
            ViewBag.VisitsMaster = await MasterV.GetListVisitsAsync();
            ViewBag.BlameMaster = await MasterB.GetListBlameAsync();

            model.EditedBy = "مريم سالم الحمادي";
            model.EditDate = DateTime.Now;
            model.Status = "تم التعديل";
            model.ApprovedBy = " ";
            model.ApproveDate = DateTime.Now;
            model.ApproveNotes = " ";

            // Use the key from the model (ReportID) instead of a separate id param
            var existingReport = await CSRepo.GetCommunityServiceByIdAsync(model.ReportID);
            if (existingReport == null)
            {
                TempData["ErrorMessage"] = "Report not found";
                return RedirectToAction("CommunityServiceList", "CommunityService");
            }


            if (!ModelState.IsValid)
            {
                LogErrors();
                TempData["ErrorMessage"] = "Model validation failed";
                return View("UpdateReport", model);
            }

            await CSRepo.UpdateCommunityServiceAsync(model);
            TempData["SuccessMessage"] = "تم تعديل التقرير بنجاح";
            return RedirectToAction("CommunityServiceList", "CommunityService"); // adjust if you meant another page
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


        #region Community Service : Details
        public async Task<ActionResult> CommunityServiceDetails(int id)
        {
            var model = await CSRepo.GetCommunityServiceByIdAsync(id);


            return View(model);
        }

        #endregion

        public ActionResult AddCommunityService1()
        {
            return View();
        }

        public ActionResult RecordsCase()
        {
            var records = new List<CommunityServiceDTO>();
            records.AddRange(GetRandomRecords(100));  

            return View(records);

        }

        private List<CommunityServiceDTO> GetRandomRecords(int count)
        {
            var rand = new Random();
            var list = new List<CommunityServiceDTO>();

            for (int i = 0; i < count; i++)
            {
                list.Add(new CommunityServiceDTO
                {
                    ReportID = rand.Next(50000, 100000),
                  
                    Blame = GetRandomBlame(rand),
                    CaseNo = rand.Next(20000, 100000).ToString(),
                    StartDate = RandomDay(rand),
                    EndDate = RandomDay(rand),
                    CreatedDate = RandomDay(rand),
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



        private string GetRandomBlame(Random rand)
        {
            string[] blame = {

       "هتك عرض",
        "مادة تعريفية",
        "تحسين المعصية",
        "شرب الخمر، تعاطي",
        "السرقة",
        "امر سامي",
        "مالية - شيكات",
        "التهديد",
        "اعتداء بسيط",
        "قضية مرورية",
        "الرشوة",
        "انتهاك حرمة ملك الغير"
    };

            return blame[rand.Next(blame.Length)];
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