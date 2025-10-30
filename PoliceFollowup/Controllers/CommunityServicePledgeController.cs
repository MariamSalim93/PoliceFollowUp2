using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RepositoryLayer.MasterRepo;
using RepositoryLayer.ReportRepo;
using SharedLayer.Models;


namespace PoliceFollowup.Controllers
{
    public class CommunityServicePledgeController : Controller
    {
        #region Repositories and Logs

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(ConvictFileController));
        readonly CommunityServicePledgeRepo CPRepo = new CommunityServicePledgeRepo();
        readonly AccusationTypeRepo MasterAT = new AccusationTypeRepo();

        #endregion

        #region Community Pledge : View All reports
        public ActionResult CommunityPledgeList()
        {
            return View();
        }


        [HttpGet]
        public async Task<JsonResult> GetListOfCommunityPledgeJson()
        {
            var CommunityPledge = await CPRepo.GetListOfCommunityServicePledgeAsync();
            return Json(CommunityPledge, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Community Pledge : Update Report
        [HttpGet]

        public async Task<ActionResult> UpdateReport(int id)
        {
            ViewBag.AccusationTypeMaster = await MasterAT.GetListAccusationTypeAsync();

            var CommunityPledge = await CPRepo.GetCommunityServicePledgeByIdAsync(id);
            if (CommunityPledge == null)
                return HttpNotFound();


            return View("UpdateReport", CommunityPledge);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateReport(CommunityServicePledgeDTO model)
        {
            ViewBag.AccusationTypeMaster = await MasterAT.GetListAccusationTypeAsync();

            model.EditedBy = "مريم سالم الحمادي";
            model.EditDate = DateTime.Now;
            model.Status = "تم التعديل";
            model.ApprovedBy = " ";
            model.ApproveDate = DateTime.Now;
            model.ApproveNotes = " ";

            // Use the key from the model (ReportID) instead of a separate id param
            var existingReport = await CPRepo.GetCommunityServicePledgeByIdAsync(model.ReportID);
            if (existingReport == null)
            {
                TempData["ErrorMessage"] = "Report not found";
                return RedirectToAction("CommunityPledgeList", "CommunityServicePledge");
            }


            if (!ModelState.IsValid)
            {
                LogErrors();
                TempData["ErrorMessage"] = "Model validation failed";
                return View("UpdateReport", model);
            }

            await CPRepo.UpdateCommunityServicePledgeAsync(model);
            TempData["SuccessMessage"] = "تم تعديل التقرير بنجاح";
            return RedirectToAction("CommunityPledgeList", "CommunityServicePledge"); // adjust if you meant another page
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
        public async Task<ActionResult> CommunityPledgeDetails(int id)
        {
            var model = await CPRepo.GetCommunityServicePledgeByIdAsync(id);


            return View(model);
        }

        #endregion

        public ActionResult AddPledge()
        {
            return View();
        }
        public ActionResult RecordsCase()
        {
            var records = new List<CommunityServicePledgeDTO>();
            records.AddRange(GetRandomRecords(100));

            return View(records);

        }


        private List<CommunityServicePledgeDTO> GetRandomRecords(int count)
        {
            var rand = new Random();
            var list = new List<CommunityServicePledgeDTO>();

            for (int i = 0; i < count; i++)
            {
                list.Add(new CommunityServicePledgeDTO
                {
                    ReportID = rand.Next(50000, 100000),
                   
                    StartDate = RandomDay(rand),
                    EndDate = RandomDay(rand),
                    CreatedBy = rand.Next(26000, 27000).ToString(),
                    CreatedDate = RandomDay(rand),

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



        private string GetRandomUnifiendNo(Random rand)
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