using RepositoryLayer.MasterRepo;
using SharedLayer.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PoliceFollowup.Controllers
{
    public class MasterController : Controller
    {
        #region Repositories

        readonly AccusationTypeRepo ATRepo = new AccusationTypeRepo();
        readonly AuthorityRepo AthRepo = new AuthorityRepo();
        readonly BlameRepo BRepo = new BlameRepo();
        readonly DecisionRepo DRepo = new DecisionRepo();
        readonly DeviceNoRepo DNoRepo = new DeviceNoRepo();
        readonly JudgmentSourceRepo JSRepo = new JudgmentSourceRepo();
        readonly JudgmentTypeRepo JTRepo = new JudgmentTypeRepo();
        readonly MonitoringAreaRepo MRepo = new MonitoringAreaRepo();
        readonly OfficerRepo ORepo = new OfficerRepo();
        readonly PlacesRepo PRepo = new PlacesRepo();
        readonly SupervisorRepo SRepo = new SupervisorRepo();
        readonly VisitsRepo VRepo = new VisitsRepo();
        readonly NationalityRepo NRepo = new NationalityRepo();
        readonly ResidenceRepo RRepo = new ResidenceRepo();


        #endregion

        public ActionResult Master()
        {
            return View();
        }

        #region Master :  AccusationType

        #region AccusationType : List
        public ActionResult AccusationTypeList()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetListOfAccusationTypeJson()
        {
            var nationalities = await ATRepo.GetListAccusationTypeAsync();
            return Json(nationalities, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region AccusationType : Add
        public ActionResult AddAccusationType()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddAccusationType(AccusationTypeDTO N)
        {
            if (ModelState.IsValid)  // Ensure that the model is valid before proceeding
            {
                await ATRepo.AddAccusationTypeAsync(N);  // Add the action type
                return RedirectToAction("AccusationTypeList");  // Redirect to Department List after successful submission
            }
            return View(N);
        }

        #endregion


        #endregion

        #region Master :  Authority

        #region Authority : List
        public ActionResult AuthorityList()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetListOfAuthorityJson()
        {
            var nationalities = await AthRepo.GetListAuthorityAsync();
            return Json(nationalities, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Authority : Add
        public ActionResult AddAuthority()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddAuthority(AuthorityDTO N)
        {
            if (ModelState.IsValid)  // Ensure that the model is valid before proceeding
            {
                await AthRepo.AddAuthorityAsync(N);  // Add the action type
                return RedirectToAction("AuthorityList");  // Redirect to Department List after successful submission
            }
            return View(N);
        }

        #endregion


        #endregion

        #region Master :  Blame

        #region Blame : List
        public ActionResult BlameList()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetListOfBlameJson()
        {
            var nationalities = await BRepo.GetListBlameAsync();
            return Json(nationalities, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Blame : Add
        public ActionResult AddBlame()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddBlame(BlameDTO N)
        {
            if (ModelState.IsValid)  // Ensure that the model is valid before proceeding
            {
                await BRepo.AddBlameAsync(N);  // Add the action type
                return RedirectToAction("BlameList");  // Redirect to Department List after successful submission
            }
            return View(N);
        }

        #endregion


        #endregion

        #region Master :  Decision

        #region Decision : List
        public ActionResult DecisionList()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetListOfDecisionJson()
        {
            var nationalities = await DRepo.GetListDecisionAsync();
            return Json(nationalities, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Decision : Add
        public ActionResult AddDecision()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddDecision(DecisionDTO N)
        {
            if (ModelState.IsValid)  // Ensure that the model is valid before proceeding
            {
                await DRepo.AddDecisionAsync(N);  // Add the action type
                return RedirectToAction("DecisionList");  // Redirect to Department List after successful submission
            }
            return View(N);
        }

        #endregion


        #endregion

        #region Master :  DeviceNo

        #region DeviceNo : List
        public ActionResult DeviceNoList()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetListOfDeviceNoJson()
        {
            var nationalities = await DNoRepo.GetListDeviceNoAsync();
            return Json(nationalities, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region DeviceNo : Add
        public ActionResult AddDeviceNo()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddDeviceNo(DeviceNoDTO N)
        {
            if (ModelState.IsValid)  // Ensure that the model is valid before proceeding
            {
                await DNoRepo.AddDeviceNoAsync(N);  // Add the action type
                return RedirectToAction("DeviceNoList");  // Redirect to Department List after successful submission
            }
            return View(N);
        }

        #endregion


        #endregion

        #region Master :  JudgmentSource

        #region JudgmentSource : List
        public ActionResult JudgmentSourceList()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetListOfJudgmentSourceJson()
        {
            var nationalities = await JSRepo.GetListJudgmentSourceAsync();
            return Json(nationalities, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region JudgmentSource : Add
        public ActionResult AddJudgmentSource()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddJudgmentSource(JudgmentSourceDTO N)
        {
            if (ModelState.IsValid)  // Ensure that the model is valid before proceeding
            {
                await JSRepo.AddJudgmentSourceAsync(N);  // Add the action type
                return RedirectToAction("JudgmentSourceList");  // Redirect to Department List after successful submission
            }
            return View(N);
        }

        #endregion


        #endregion

        #region Master :  JudgmentType

        #region JudgmentType : List
        public ActionResult JudgmentTypeList()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetListOfJudgmentTypeJson()
        {
            var nationalities = await JTRepo.GetListJudgmentTypeAsync();
            return Json(nationalities, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region JudgmentType : Add
        public ActionResult AddJudgmentType()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddJudgmentType(JudgmentTypeDTO N)
        {
            if (ModelState.IsValid)  // Ensure that the model is valid before proceeding
            {
                await JTRepo.AddJudgmentTypeAsync(N);  // Add the action type
                return RedirectToAction("JudgmentTypeList");  // Redirect to Department List after successful submission
            }
            return View(N);
        }

        #endregion


        #endregion

        #region Master :  MonitoringArea

        #region MonitoringArea : List
        public ActionResult MonitoringAreaList()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetListOfMonitoringAreaJson()
        {
            var nationalities = await MRepo.GetListMonitoringAreaAsync();
            return Json(nationalities, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region MonitoringArea : Add
        public ActionResult AddMonitoringArea()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddMonitoringArea(MonitoringAreaDTO N)
        {
            if (ModelState.IsValid)  // Ensure that the model is valid before proceeding
            {
                await MRepo.AddMonitoringAreaAsync(N);  // Add the action type
                return RedirectToAction("MonitoringAreaList");  // Redirect to Department List after successful submission
            }
            return View(N);
        }

        #endregion


        #endregion

        #region Master :  Officer

        #region Officer : List
        public ActionResult OfficerList()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetListOfOfficerJson()
        {
            var nationalities = await ORepo.GetListOfficerAsync();
            return Json(nationalities, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Officer : Add
        public ActionResult AddOfficer()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddOfficer(OfficerDTO N)
        {
            if (ModelState.IsValid)  // Ensure that the model is valid before proceeding
            {
                await ORepo.AddOfficerAsync(N);  // Add the action type
                return RedirectToAction("OfficerList");  // Redirect to Department List after successful submission
            }
            return View(N);
        }

        #endregion


        #endregion

        #region Master :  Places

        #region Places : List
        public ActionResult PlacesList()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetListOfPlacesJson()
        {
            var nationalities = await PRepo.GetListPlacesAsync();
            return Json(nationalities, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Places : Add
        public ActionResult AddPlaces()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddPlaces(PlacesDTO N)
        {
            if (ModelState.IsValid)  // Ensure that the model is valid before proceeding
            {
                await PRepo.AddPlacesAsync(N);  // Add the action type
                return RedirectToAction("PlacesList");  // Redirect to Department List after successful submission
            }
            return View(N);
        }

        #endregion


        #endregion

        #region Master :  Residence

        #region Residence : List
        public ActionResult ResidenceList()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetListOfResidenceJson()
        {
            var nationalities = await RRepo.GetListResidenceAsync();
            return Json(nationalities, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Residence : Add
        public ActionResult AddResidence()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddResidence(ResidenceDTO N)
        {
            if (ModelState.IsValid)  // Ensure that the model is valid before proceeding
            {
                await RRepo.AddResidenceAsync(N);  // Add the action type
                return RedirectToAction("ResidenceList");  // Redirect to Department List after successful submission
            }
            return View(N);
        }

        #endregion


        #endregion


        #region Master :  Supervisor

        #region Supervisor : List
        public ActionResult SupervisorList()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetListOfSupervisorJson()
        {
            var nationalities = await SRepo.GetListSupervisorAsync();
            return Json(nationalities, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Supervisor : Add
        public ActionResult AddSupervisor()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddSupervisor(SupervisorDTO N)
        {
            if (ModelState.IsValid)  // Ensure that the model is valid before proceeding
            {
                await SRepo.AddSupervisorAsync(N);  // Add the action type
                return RedirectToAction("SupervisorList");  // Redirect to Department List after successful submission
            }
            return View(N);
        }

        #endregion


        #endregion

        #region Master :  Visits

        #region Visits : List
        public ActionResult VisitsList()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetListOfVisitsJson()
        {
            var nationalities = await VRepo.GetListVisitsAsync();
            return Json(nationalities, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Visits : Add
        public ActionResult AddVisits()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddVisits(VisitsDTO N)
        {
            if (ModelState.IsValid)  // Ensure that the model is valid before proceeding
            {
                await VRepo.AddVisitsAsync(N);  // Add the action type
                return RedirectToAction("VisitsList");  // Redirect to Department List after successful submission
            }
            return View(N);
        }

        #endregion


        #endregion

        #region Master :  Nationality

        #region Nationality : List
        public ActionResult NationalityList()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetListOfNationalityJson()
        {
            var nationalities = await NRepo.GetListNationalityAsync();
            return Json(nationalities, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Nationality : Add
        public ActionResult AddNationality()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddNationality(NationalityDTO N)
        {
            if (ModelState.IsValid)  // Ensure that the model is valid before proceeding
            {
                await NRepo.AddNationalityAsync(N);  // Add the action type
                return RedirectToAction("NationalityList");  // Redirect to Department List after successful submission
            }
            return View(N);
        }

        #endregion


        #endregion

    }
}