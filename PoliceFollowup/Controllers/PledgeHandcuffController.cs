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
    public class PledgeHandcuffController : Controller
    {

        #region Repositories and Logs

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(ConvictFileController));
        readonly PledgeHandcuffRepo PHRepo = new PledgeHandcuffRepo();
        readonly DecisionRepo MasterD = new DecisionRepo();
        readonly DeviceNoRepo MasterDN = new DeviceNoRepo();

        #endregion

        #region Pledge Handcuff : View All reports
        public ActionResult PledgeHandcuffList()
        {
            return View();
        }


        [HttpGet]
        public async Task<JsonResult> GetListOfPledgeHandcuffJson()
        {
            var PledgeHandcuff = await PHRepo.GetListOfPledgeHandcuffAsync();
            return Json(PledgeHandcuff, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Pledge Handcuff : Update Report
        [HttpGet]

        public async Task<ActionResult> UpdateReport(int id)
        {
            ViewBag.DecisionMaster = await MasterD.GetListDecisionAsync();
            ViewBag.DeviceNoMaster = await MasterDN.GetListDeviceNoAsync();

            var PledgeHandcuff = await PHRepo.GetPledgeHandcuffByIdAsync(id);
            if (PledgeHandcuff == null)
                return HttpNotFound();


            return View("UpdateReport", PledgeHandcuff);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateReport(PledgeHandcuffDTO model)
        {
            ViewBag.DecisionMaster = await MasterD.GetListDecisionAsync();
            ViewBag.DeviceNoMaster = await MasterDN.GetListDeviceNoAsync();

            model.EditedBy = "مريم سالم الحمادي";
            model.EditDate = DateTime.Now;
            model.Status = "تم التعديل";
            model.ApprovedBy = " ";
            model.ApproveDate = DateTime.Now;
            model.ApproveNotes = " ";

            // Use the key from the model (ReportID) instead of a separate id param
            var existingReport = await PHRepo.GetPledgeHandcuffByIdAsync(model.ReportID);
            if (existingReport == null)
            {
                TempData["ErrorMessage"] = "Report not found";
                return RedirectToAction("PledgeHandcuffList", "PledgeHandcuff");
            }


            if (!ModelState.IsValid)
            {
                LogErrors();
                TempData["ErrorMessage"] = "Model validation failed";
                return View("UpdateReport", model);
            }

            await PHRepo.UpdatePledgeHandcuffAsync(model);
            TempData["SuccessMessage"] = "تم تعديل التقرير بنجاح";
            return RedirectToAction("PledgeHandcuffList", "PledgeHandcuff");
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
        public async Task<ActionResult> PledgeHandcuffDetails(int id)
        {
            var model = await PHRepo.GetPledgeHandcuffByIdAsync(id);


            return View(model);
        }

        #endregion

        public ActionResult AddPledge()
        {
            return View();
        }


        public ActionResult RecordsPledge()
        {
            var records = new List<PledgeHandcuffDTO>();
            records.AddRange(GetRandomRecords(100));  // Adding 20 random records

            return View(records);

        }

        private List<PledgeHandcuffDTO> GetRandomRecords(int count)
        {
            var rand = new Random();
            var list = new List<PledgeHandcuffDTO>();

            for (int i = 0; i < count; i++)
            {
                list.Add(new PledgeHandcuffDTO
                {
                    ReportID = rand.Next(50000, 100000),
                    CreatedDate = RandomDay(rand),           
                    CaseNo = rand.Next(50000, 100000).ToString(),
                    DeviceNo = rand.Next(50000, 100000).ToString(),       
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