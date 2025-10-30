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
    public class VisitController : Controller
    {


        #region Repositories and Logs

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(ConvictFileController));
        readonly VisitRepo VRepo = new VisitRepo();
        readonly AuthorityRepo MasterAth = new AuthorityRepo();

        #endregion

        #region Visit : View All reports
        public ActionResult VisitList()
        {
            return View();
        }


        [HttpGet]
        public async Task<JsonResult> GetListOfVisitJson()
        {
            var Visit = await VRepo.GetListOfVisitAsync();
            return Json(Visit, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Visit : Update Report
        [HttpGet]

        public async Task<ActionResult> UpdateReport(int id)
        {
            ViewBag.AuthorityMaster = await MasterAth.GetListAuthorityAsync();

            var Visit = await VRepo.GetVisitByIdAsync(id);
            if (Visit == null)
                return HttpNotFound();


            return View("UpdateReport", Visit);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateReport(VisitDTO model)
        {
            ViewBag.AuthorityMaster = await MasterAth.GetListAuthorityAsync();

            model.EditedBy = "مريم سالم الحمادي";
            model.EditDate = DateTime.Now;
            model.Status = "تم التعديل";
            model.ApprovedBy = " ";
            model.ApproveDate = DateTime.Now;
            model.ApproveNotes = " ";

            // Use the key from the model (ReportID) instead of a separate id param
            var existingReport = await VRepo.GetVisitByIdAsync(model.ReportID);
            if (existingReport == null)
            {
                TempData["ErrorMessage"] = "Report not found";
                return RedirectToAction("VisitList", "Visit");
            }


            if (!ModelState.IsValid)
            {
                LogErrors();
                TempData["ErrorMessage"] = "Model validation failed";
                return View("UpdateReport", model);
            }

            await VRepo.UpdateVisitAsync(model);
            TempData["SuccessMessage"] = "تم تعديل التقرير بنجاح";
            return RedirectToAction("VisitList", "Visit");
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

        #region Visit : Details
        public async Task<ActionResult> VisitDetails(int id)
        {
            var model = await VRepo.GetVisitByIdAsync(id);


            return View(model);
        }

        #endregion
        public ActionResult AddVisit()
        {
            return View();
        }


        public ActionResult RecordsVisit()
        {
            var records = new List<VisitDTO>();
            records.AddRange(GetRandomRecords(100));  // Adding 20 random records

            return View(records);

        }

        private List<VisitDTO> GetRandomRecords(int count)
        {
            var rand = new Random();
            var list = new List<VisitDTO>();

            for (int i = 0; i < count; i++)
            {
                list.Add(new VisitDTO
                {
                    ReportID = rand.Next(50000, 100000),
                    CreatedDate = RandomDay(rand),
                    VisitPlace = GetRandomVisitPlace(rand),       
                    VisitDate = RandomDay(rand),
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
           private string GetRandomVisitPlace(Random rand)
        {
            string[] place = {
        "المنزل",
        "العمل"
    };

            return place[rand.Next(place.Length)];
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