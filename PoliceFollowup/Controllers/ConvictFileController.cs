using RepositoryLayer.MasterRepo;
using RepositoryLayer.ReportRepo;
using SharedLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static System.Net.WebRequestMethods;

namespace PoliceFollowup.Controllers
{
    public class ConvictFileController : Controller
    {
        #region Repositories and Logs

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(ConvictFileController));
        readonly ConvictFileRepo CFRepo = new ConvictFileRepo();
        readonly CommunityServiceRepo CSRepo = new CommunityServiceRepo();
        readonly ElectronicMonitoringRepo EMRepo = new ElectronicMonitoringRepo();
        readonly CommunityServicePledgeRepo CPRepo = new CommunityServicePledgeRepo();
        readonly FinalReportsRepo FRRepo = new FinalReportsRepo();
        readonly PledgeHandcuffRepo HPRepo = new PledgeHandcuffRepo();
        readonly ReceiveCaseRepo RCRepo = new ReceiveCaseRepo();
        readonly VisitRepo VRepo = new VisitRepo();
        readonly AttachmentRepo aRepo = new AttachmentRepo();
        readonly NationalityRepo MasterN = new NationalityRepo();
        readonly AccusationTypeRepo MasterAT = new AccusationTypeRepo();
        readonly AuthorityRepo MasterAth = new AuthorityRepo();
        readonly BlameRepo MasterB = new BlameRepo();
        readonly DecisionRepo MasterD = new DecisionRepo();
        readonly DeviceNoRepo MasterDN = new DeviceNoRepo();
        readonly JudgmentSourceRepo MasterJS = new JudgmentSourceRepo();
        readonly JudgmentTypeRepo MasterJT = new JudgmentTypeRepo();
        readonly MonitoringAreaRepo MasterM = new MonitoringAreaRepo();
        readonly OfficerRepo MasterO = new OfficerRepo();
        readonly PlacesRepo MasterP = new PlacesRepo();
        readonly SupervisorRepo MasterS = new SupervisorRepo();
        readonly VisitsRepo MasterV = new VisitsRepo();
        readonly ResidenceRepo MasterR = new ResidenceRepo();

        #endregion

        #region Convict File : View All reports
        public ActionResult ConvictFileList()
        {
            return View();
        }


        [HttpGet]
        public async Task<JsonResult> GetListOfConvictFileJson()
        {
            var ConvictFile = await CFRepo.GetListOfConvictFileAsync();
            return Json(ConvictFile, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Convict File : Add new Report

        public async Task<ActionResult> AddConvictFile()

        {
            ViewBag.Nationality = await MasterN.GetListNationalityAsync();
            ViewBag.ResidenceMaster = await MasterR.GetListResidenceAsync();

            var model = new ConvictFileDTO
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
        public async Task<ActionResult> AddConvictFile(ConvictFileDTO CF, IEnumerable<HttpPostedFileBase> Attachments)
        {
            ViewBag.Nationality = await MasterN.GetListNationalityAsync();

            try
            {
                CF.CreatedDate = DateTime.Now;
                CF.CreatedBy = string.IsNullOrWhiteSpace(CF.CreatedBy) ? "مريم سالم الحمادي" : CF.CreatedBy;
                CF.EditedBy = string.IsNullOrWhiteSpace(CF.EditedBy) ? " " : CF.EditedBy;
                CF.EditNote = string.IsNullOrWhiteSpace(CF.EditNote) ? " " : CF.EditNote;
                CF.ApprovedBy = string.IsNullOrWhiteSpace(CF.ApprovedBy) ? " " : CF.ApprovedBy;
                CF.ApproveNotes = string.IsNullOrWhiteSpace(CF.ApproveNotes) ? " " : CF.ApproveNotes;

                #region Logs

                log4net.LogicalThreadContext.Properties["Controller"] = this.ControllerContext.RouteData.Values["controller"];
                log4net.LogicalThreadContext.Properties["Action"] = this.ControllerContext.RouteData.Values["action"];
                log4net.LogicalThreadContext.Properties["UserName"] = "HAJER EISA";

                #endregion
                // Save المرفقات (AttachPhoto)
                if (Attachments != null && Attachments.Any())
                {
                    var file = Attachments.FirstOrDefault();
                    if (file != null && file.ContentLength > 0)
                    {
                        var extension = Path.GetExtension(file.FileName);
                        var fileName = $"{Guid.NewGuid()}{extension}";
                        var path = Server.MapPath("~/Content/Documents/Files");

                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);

                        var filePath = Path.Combine(path, fileName);
                        file.SaveAs(filePath);

                        CF.Attachments = $"~/Content/Documents/Files/{fileName}"; // Adjust property name as needed
                    }

                }
                if (ModelState.IsValid)
                {

                    await CFRepo.AddConvictFileAsync(CF);

                    ViewBag.Success = "تم إضافة البيانات بنجاح!";
                    Log.Info("success");

                    return RedirectToAction("ConvictFileList", "ConvictFile"); ;
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

                    TempData["ConvictFileErrorMessage"] = "يرجى التحقق من جميع الحقول قبل الإدراج!";
                    return View(CF);
                }
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while adding Convict File.", ex);
                ModelState.AddModelError("", ex.Message);   // show the real error
                return View(CF);                            // don't redirect to NotFound/Error
                                                            // or RedirectToAction("Error","Home") if that exists
            }

        }


        #endregion



        #region Convict File : Details
        public async Task<ActionResult> ConvictFileDetails(int id)
        {
            
            ViewBag.AccusationTypeMaster = await MasterAT.GetListAccusationTypeAsync();
            ViewBag.AuthorityMaster = await MasterAth.GetListAuthorityAsync();
            ViewBag.BlameMaster = await MasterB.GetListBlameAsync();
            ViewBag.DecisionMaster = await MasterD.GetListDecisionAsync();
            ViewBag.DeviceNoMaster = await MasterDN.GetListDeviceNoAsync();
            ViewBag.JudgmentSourceMaster = await MasterJS.GetListJudgmentSourceAsync();
            ViewBag.JudgmentTypeMaster = await MasterJT.GetListJudgmentTypeAsync();
            ViewBag.MonitoringAreaMaster = await MasterM.GetListMonitoringAreaAsync();
            ViewBag.OfficerMaster = await MasterO.GetListOfficerAsync();
            ViewBag.PlacesMaster = await MasterP.GetListPlacesAsync();
            ViewBag.SupervisorMaster = await MasterS.GetListSupervisorAsync();
            ViewBag.VisitsMaster = await MasterV.GetListVisitsAsync();

            ViewBag.ListReceiveCase = await RCRepo.GetListOfReceiveCaseAsync();
            ViewBag.ListCommunityService = await CSRepo.GetListOfCommunityServiceAsync();
            ViewBag.ListElectronicMonitoring = await EMRepo.GetListOfElectronicMonitoringAsync();
            ViewBag.ListCommunityPledge = await CPRepo.GetListOfCommunityServicePledgeAsync();
            ViewBag.ListPledgeHandcuff = await HPRepo.GetListOfPledgeHandcuffAsync();
            ViewBag.ListVisit = await VRepo.GetListOfVisitAsync();
            ViewBag.ListFinalReports = await FRRepo.GetListOfFinalReportsAsync();
            ViewBag.ListAttachment = await aRepo.GetListOfAttachmentAsync();

            var model = await CFRepo.GetConvictFileByIdAsync(id);
            
            ViewBag.CommunityService = new CommunityServiceDTO
            {
                FileNo = model.ReportID,
                CreatedDate = DateTime.Now,
                CreatedBy = "مريم سالم الحمادي",
                Status = "جديد",
                //CaseNo = model.ReportID.ToString(),
                EditNote = " ",
                EditedBy = " ",
                EditDate = DateTime.Now,
                ApproveDate = DateTime.Now,
                ApprovedBy = " ",
                ApproveNotes = " "
            };
            ViewBag.ElectronicMonitoring = new ElectronicMonitoringDTO
            {
                FileNo = model.ReportID,
                CreatedDate = DateTime.Now,
                CreatedBy = "مريم سالم الحمادي",
                Status = "جديد",
                //CaseNo = model.ReportID.ToString(),
                EditNote = " ",
                EditedBy = " ",
                EditDate = DateTime.Now,
                ApproveDate = DateTime.Now,
                ApprovedBy = " ",
                ApproveNotes = " "
            };
            ViewBag.CommunityPledge = new CommunityServicePledgeDTO
            {
                FileNo = model.ReportID,
                CreatedDate = DateTime.Now,
                CreatedBy = "مريم سالم الحمادي",
                Status = "جديد",
                //CaseNo = model.ReportID.ToString(),
                EditNote = " ",
                EditedBy = " ",
                EditDate = DateTime.Now,
                ApproveDate = DateTime.Now,
                ApprovedBy = " ",
                ApproveNotes = " "
            };

            ViewBag.FinalReports = new FinalReportsDTO
            {
                FileNo = model.ReportID,
                CreatedDate = DateTime.Now,
                CreatedBy = "مريم سالم الحمادي",
                Status = "جديد",
                //CaseNo = file.Id.ToString(),
                EditNote = " ",
                EditedBy = " ",
                EditDate = DateTime.Now,
                ApproveDate = DateTime.Now,
                ApprovedBy = " ",
                ApproveNotes = " ",
            };

            ViewBag.PledgeHandcuff = new PledgeHandcuffDTO
            {
                FileNo = model.ReportID,
                CreatedDate = DateTime.Now,
                CreatedBy = "مريم سالم الحمادي",
                Status = "جديد",
                //CaseNo = file.Id.ToString(),
                EditNote = " ",
                EditedBy = " ",
                EditDate = DateTime.Now,
                ApproveDate = DateTime.Now,
                ApprovedBy = " ",
                ApproveNotes = " ",
            };

            ViewBag.ReceiveCase = new ReceiveCaseDTO
            {
                FileNo = model.ReportID,
                CreatedDate = DateTime.Now,
                CreatedBy = "مريم سالم الحمادي",
                Status = "جديد",
                //CaseNo = file.Id.ToString(),
                EditNote = " ",
                EditedBy = " ",
                EditDate = DateTime.Now,
                ApproveDate = DateTime.Now,
                ApprovedBy = " ",
                ApproveNotes = " ",
            };

            ViewBag.Visit = new VisitDTO
            {
                FileNo = model.ReportID,
                CreatedDate = DateTime.Now,
                CreatedBy = "مريم سالم الحمادي",
                Status = "جديد",
                //CaseNo = file.Id.ToString(),
                EditNote = " ",
                EditedBy = " ",
                EditDate = DateTime.Now,
                ApproveDate = DateTime.Now,
                ApprovedBy = " ",
                ApproveNotes = " ",
            };
            
            ViewBag.Attachment = new AttachmentDTO
            {
                FileNo = model.ReportID,
                CreatedDate = DateTime.Now,
                CreatedBy = "مريم سالم الحمادي",
                Status = "جديد",
                //CaseNo = file.Id.ToString(),
                EditNote = " ",
                EditedBy = " ",
                EditDate = DateTime.Now,
                ApproveDate = DateTime.Now,
                ApprovedBy = " ",
                ApproveNotes = " ",
            };

            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.Error = "error";

            return View(model);
        }

        #endregion

        #region Convict File : Update Report
        [HttpGet]

        public async Task<ActionResult> UpdateReport(int id)
        {
            ViewBag.NationalityMaster = await MasterN.GetListNationalityAsync();
            ViewBag.ResidenceMaster = await MasterR.GetListResidenceAsync();

            var ConvictFile = await CFRepo.GetConvictFileByIdAsync(id);
            if (ConvictFile == null)
                return HttpNotFound();


            return View("UpdateReport", ConvictFile);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateReport(ConvictFileDTO model, HttpPostedFileBase Attachments)
        {
            ViewBag.NationalityMaster = await MasterN.GetListNationalityAsync();
            ViewBag.ResidenceMaster = await MasterR.GetListResidenceAsync();

            model.EditedBy = "مريم سالم الحمادي";
            model.EditDate = DateTime.Now;
            model.Status = "تم التعديل";
            model.ApprovedBy = " ";
            model.ApproveDate = DateTime.Now;
            model.ApproveNotes = " ";

            // Use the key from the model (ReportID) instead of a separate id param
            var existingReport = await CFRepo.GetConvictFileByIdAsync(model.ReportID);
            if (existingReport == null)
            {
                TempData["ErrorMessage"] = "Report not found";
                return RedirectToAction("ConvictFileList", "ConvictFile");
            }

            if (Attachments != null && Attachments.ContentLength > 0)
            {
                var extension = Path.GetExtension(Attachments.FileName);
                var fileName = $"{Guid.NewGuid()}{extension}";
                var serverPath = Server.MapPath("~/Content/Documents/Files/");
                if (!Directory.Exists(serverPath)) Directory.CreateDirectory(serverPath);
                var filePath = Path.Combine(serverPath, fileName);
                Attachments.SaveAs(filePath);
                model.Attachments = $"~/Content/Documents/Files/{fileName}";
            }
            else
            {
                // keep the old file if no new upload
                model.Attachments = existingReport.Attachments;
            }

            if (!ModelState.IsValid)
            {
                LogErrors();
                TempData["ErrorMessage"] = "Model validation failed";
                return View("UpdateReport", model);
            }

            await CFRepo.UpdateConvictFileAsync(model);
            TempData["SuccessMessage"] = "تم تعديل التقرير بنجاح";
            return RedirectToAction("ConvictFileList", "ConvictFile"); // adjust if you meant another page
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

        #region Community Service : Add new Report

        public ActionResult AddCommunityService(int id)

        {
            //ViewBag.RecruitList = await RecRepo.GetListOfODRecruitAsync();

            var file = CFRepo.GetConvictFileByIdAsync(id);

            ViewBag.CommunityService = new CommunityServiceDTO  
            {
                FileNo = file.Id,
                CreatedDate = DateTime.Now,
                CreatedBy = "مريم سالم الحمادي",
                Status = "جديد",
                //CaseNo = file.Id.ToString(),
                EditNote = " ",
                EditedBy = " ",
                EditDate = null,
                ApproveDate = null,
                ApprovedBy = " ",
                ApproveNotes = " ",
            };

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddCommunityService(CommunityServiceDTO CS)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int? id = CS.ReportID;
                    CS.CreatedBy = string.IsNullOrWhiteSpace(CS.CreatedBy) ? "مريم سالم الحمادي" : CS.CreatedBy;
                    CS.CreatedDate = DateTime.Now;
                    CS.EditedBy = string.IsNullOrWhiteSpace(CS.EditedBy) ? " " : CS.EditedBy;
                    CS.EditNote = string.IsNullOrWhiteSpace(CS.EditNote) ? " " : CS.EditNote;
                    CS.ApprovedBy = string.IsNullOrWhiteSpace(CS.ApprovedBy) ? " " : CS.ApprovedBy;
                    CS.ApproveNotes = string.IsNullOrWhiteSpace(CS.ApproveNotes) ? " " : CS.ApproveNotes;

                    #region Logs

                    log4net.LogicalThreadContext.Properties["Controller"] = this.ControllerContext.RouteData.Values["controller"];
                    log4net.LogicalThreadContext.Properties["Action"] = this.ControllerContext.RouteData.Values["action"];
                    log4net.LogicalThreadContext.Properties["UserName"] = "HAJER EISA";

                    #endregion

                    await CSRepo.AddCommunityServiceAsync(CS);
                    TempData["SuccessMessage"] = "تم إضافة البيانات بنجاح";
                    return RedirectToAction("ConvictFileDetails", new { id = CS.FileNo});
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while adding the procedure: " + ex.Message);
                    TempData["ErrorMessage"] = "حدث خطأ ما أثناء إضافة البيانات";
                }
            }
            return RedirectToAction("ConvictFileDetails", new { id = CS.FileNo });

        }


        #endregion

        #region Electronic Monitoring : Add new Report

        public ActionResult AddElectronicMonitoring(int id)

        {
            //ViewBag.RecruitList = await RecRepo.GetListOfODRecruitAsync();

            var file = CFRepo.GetConvictFileByIdAsync(id);

            ViewBag.ElectronicMonitoring = new ElectronicMonitoringDTO
            {
                FileNo = file.Id,
                CreatedDate = DateTime.Now,
                CreatedBy = "مريم سالم الحمادي",
                Status = "جديد",
                //CaseNo = file.Id.ToString(),
                EditNote = " ",
                EditedBy = " ",
                EditDate = null,
                ApproveDate = null,
                ApprovedBy = " ",
                ApproveNotes = " ",
            };

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddElectronicMonitoring(ElectronicMonitoringDTO EM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int? id = EM.ReportID;
                    EM.CreatedBy = string.IsNullOrWhiteSpace(EM.CreatedBy) ? "مريم سالم الحمادي" : EM.CreatedBy;
                    EM.CreatedDate = DateTime.Now;
                    EM.EditedBy = string.IsNullOrWhiteSpace(EM.EditedBy) ? " " : EM.EditedBy;
                    EM.EditNote = string.IsNullOrWhiteSpace(EM.EditNote) ? " " : EM.EditNote;
                    EM.ApprovedBy = string.IsNullOrWhiteSpace(EM.ApprovedBy) ? " " : EM.ApprovedBy;
                    EM.ApproveNotes = string.IsNullOrWhiteSpace(EM.ApproveNotes) ? " " : EM.ApproveNotes;

                    #region Logs

                    log4net.LogicalThreadContext.Properties["Controller"] = this.ControllerContext.RouteData.Values["controller"];
                    log4net.LogicalThreadContext.Properties["Action"] = this.ControllerContext.RouteData.Values["action"];
                    log4net.LogicalThreadContext.Properties["UserName"] = "HAJER EISA";

                    #endregion

                    await EMRepo.AddElectronicMonitoringAsync(EM);
                    TempData["SuccessMessage"] = "تم إضافة البيانات بنجاح";
                    return RedirectToAction("ConvictFileDetails", new { id = EM.FileNo });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while adding the procedure: " + ex.Message);
                    TempData["ErrorMessage"] = "حدث خطأ ما أثناء إضافة البيانات";
                }
            }
            return RedirectToAction("ConvictFileDetails", new { id = EM.FileNo });

        }
        #endregion

        #region Community Pledge : Add new Report

        public async Task<ActionResult> AddCommunityPledge(int id)

        {
            ViewBag.AccusationTypeMaster = await MasterAT.GetListAccusationTypeAsync();

            var file = CFRepo.GetConvictFileByIdAsync(id);

            ViewBag.CommunityPledge = new CommunityServicePledgeDTO
            {
                FileNo = file.Id,
                CreatedDate = DateTime.Now,
                CreatedBy = "مريم سالم الحمادي",
                Status = "جديد",
                //CaseNo = file.Id.ToString(),
                EditNote = " ",
                EditedBy = " ",
                EditDate = null,
                ApproveDate = null,
                ApprovedBy = " ",
                ApproveNotes = " ",
            };

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddCommunityPledge(CommunityServicePledgeDTO CP)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int? id = CP.ReportID;
                    CP.CreatedBy = string.IsNullOrWhiteSpace(CP.CreatedBy) ? "مريم سالم الحمادي" : CP.CreatedBy;
                    CP.CreatedDate = DateTime.Now;
                    CP.EditedBy = string.IsNullOrWhiteSpace(CP.EditedBy) ? " " : CP.EditedBy;
                    CP.EditNote = string.IsNullOrWhiteSpace(CP.EditNote) ? " " : CP.EditNote;
                    CP.ApprovedBy = string.IsNullOrWhiteSpace(CP.ApprovedBy) ? " " : CP.ApprovedBy;
                    CP.ApproveNotes = string.IsNullOrWhiteSpace(CP.ApproveNotes) ? " " : CP.ApproveNotes;

                    #region Logs

                    log4net.LogicalThreadContext.Properties["Controller"] = this.ControllerContext.RouteData.Values["controller"];
                    log4net.LogicalThreadContext.Properties["Action"] = this.ControllerContext.RouteData.Values["action"];
                    log4net.LogicalThreadContext.Properties["UserName"] = "HAJER EISA";

                    #endregion

                    await CPRepo.AddCommunityServicePledgeAsync(CP);
                    TempData["SuccessMessage"] = "تم إضافة البيانات بنجاح";
                    return RedirectToAction("ConvictFileDetails", new { id = CP.FileNo });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while adding the procedure: " + ex.Message);
                    TempData["ErrorMessage"] = "حدث خطأ ما أثناء إضافة البيانات";
                }
            }
            return RedirectToAction("ConvictFileDetails", new { id = CP.FileNo });

        }


        #endregion

        #region Final Reports : Add new Report

        public ActionResult AddFinalReports(int id)

        {

            var file = CFRepo.GetConvictFileByIdAsync(id);

            ViewBag.FinalReports = new FinalReportsDTO
            {
                FileNo = file.Id,
                CreatedDate = DateTime.Now,
                CreatedBy = "مريم سالم الحمادي",
                Status = "جديد",
                //CaseNo = file.Id.ToString(),
                EditNote = " ",
                EditedBy = " ",
                EditDate = null,
                ApproveDate = null,
                ApprovedBy = " ",
                ApproveNotes = " ",
            };

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddFinalReports(FinalReportsDTO FR)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int? id = FR.ReportID;
                    FR.CreatedBy = string.IsNullOrWhiteSpace(FR.CreatedBy) ? "مريم سالم الحمادي" : FR.CreatedBy;
                    FR.CreatedDate = DateTime.Now;
                    FR.EditedBy = string.IsNullOrWhiteSpace(FR.EditedBy) ? " " : FR.EditedBy;
                    FR.EditNote = string.IsNullOrWhiteSpace(FR.EditNote) ? " " : FR.EditNote;
                    FR.ApprovedBy = string.IsNullOrWhiteSpace(FR.ApprovedBy) ? " " : FR.ApprovedBy;
                    FR.ApproveNotes = string.IsNullOrWhiteSpace(FR.ApproveNotes) ? " " : FR.ApproveNotes;

                    #region Logs

                    log4net.LogicalThreadContext.Properties["Controller"] = this.ControllerContext.RouteData.Values["controller"];
                    log4net.LogicalThreadContext.Properties["Action"] = this.ControllerContext.RouteData.Values["action"];
                    log4net.LogicalThreadContext.Properties["UserName"] = "HAJER EISA";

                    #endregion

                    await FRRepo.AddFinalReportsAsync(FR);
                    TempData["SuccessMessage"] = "تم إضافة البيانات بنجاح";
                    return RedirectToAction("ConvictFileDetails", new { id = FR.FileNo });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while adding the procedure: " + ex.Message);
                    TempData["ErrorMessage"] = "حدث خطأ ما أثناء إضافة البيانات";
                }
            }
            return RedirectToAction("ConvictFileDetails", new { id = FR.FileNo });

        }


        #endregion

        #region Pledge Handcuff : Add new Report

        public ActionResult AddPledgeHandcuff(int id)

        {

            var file = CFRepo.GetConvictFileByIdAsync(id);

            ViewBag.PledgeHandcuff = new PledgeHandcuffDTO
            {
                FileNo = file.Id,
                CreatedDate = DateTime.Now,
                CreatedBy = "مريم سالم الحمادي",
                Status = "جديد",
                //CaseNo = file.Id.ToString(),
                EditNote = " ",
                EditedBy = " ",
                EditDate = null,
                ApproveDate = null,
                ApprovedBy = " ",
                ApproveNotes = " ",
            };

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPledgeHandcuff(PledgeHandcuffDTO HP)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int? id = HP.ReportID;
                    HP.CreatedBy = string.IsNullOrWhiteSpace(HP.CreatedBy) ? "مريم سالم الحمادي" : HP.CreatedBy;
                    HP.CreatedDate = DateTime.Now;
                    HP.EditedBy = string.IsNullOrWhiteSpace(HP.EditedBy) ? " " : HP.EditedBy;
                    HP.EditNote = string.IsNullOrWhiteSpace(HP.EditNote) ? " " : HP.EditNote;
                    HP.ApprovedBy = string.IsNullOrWhiteSpace(HP.ApprovedBy) ? " " : HP.ApprovedBy;
                    HP.ApproveNotes = string.IsNullOrWhiteSpace(HP.ApproveNotes) ? " " : HP.ApproveNotes;

                    #region Logs

                    log4net.LogicalThreadContext.Properties["Controller"] = this.ControllerContext.RouteData.Values["controller"];
                    log4net.LogicalThreadContext.Properties["Action"] = this.ControllerContext.RouteData.Values["action"];
                    log4net.LogicalThreadContext.Properties["UserName"] = "HAJER EISA";

                    #endregion

                    await HPRepo.AddPledgeHandcuffAsync(HP);
                    TempData["SuccessMessage"] = "تم إضافة البيانات بنجاح";
                    return RedirectToAction("ConvictFileDetails", new { id = HP.FileNo });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while adding the procedure: " + ex.Message);
                    TempData["ErrorMessage"] = "حدث خطأ ما أثناء إضافة البيانات";
                }
            }
            return RedirectToAction("ConvictFileDetails", new { id = HP.FileNo });

        }


        #endregion

        #region Receive Case : Add new Report

        public async Task<ActionResult> AddReceiveCase(int id)

        {
            ViewBag.AuthorityMaster = await MasterAth.GetListAuthorityAsync();
            ViewBag.BlameMaster = await MasterB.GetListBlameAsync();

            var file = CFRepo.GetConvictFileByIdAsync(id);

            ViewBag.ReceiveCase = new ReceiveCaseDTO
            {
                StartDate = null,
                EndDate = null,
                Info1 = " ",
                Info2 = " ",
                Info3 = " ",
                Info4 = " ",
                Info5 = " ",
                Info6 = " ",
                Info7 = " ",
             
                FileNo = file.Id,
                CreatedDate = DateTime.Now,
                CreatedBy = "مريم سالم الحمادي",
                Status = "جديد",
                //CaseNo = file.Id.ToString(),
                EditNote = " ",
                EditedBy = " ",
                EditDate = null,
                ApproveDate = null,
                ApprovedBy = " ",
                ApproveNotes = " ",
            };

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddReceiveCase(ReceiveCaseDTO RC)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int? id = RC.ReportID;
                    RC.CreatedBy = string.IsNullOrWhiteSpace(RC.CreatedBy) ? "مريم سالم الحمادي" : RC.CreatedBy;
                    RC.CreatedDate = DateTime.Now;
                    RC.EditedBy = string.IsNullOrWhiteSpace(RC.EditedBy) ? " " : RC.EditedBy;
                    RC.EditNote = string.IsNullOrWhiteSpace(RC.EditNote) ? " " : RC.EditNote;
                    RC.ApprovedBy = string.IsNullOrWhiteSpace(RC.ApprovedBy) ? " " : RC.ApprovedBy;
                    RC.ApproveNotes = string.IsNullOrWhiteSpace(RC.ApproveNotes) ? " " : RC.ApproveNotes;

                    #region Logs

                    log4net.LogicalThreadContext.Properties["Controller"] = this.ControllerContext.RouteData.Values["controller"];
                    log4net.LogicalThreadContext.Properties["Action"] = this.ControllerContext.RouteData.Values["action"];
                    log4net.LogicalThreadContext.Properties["UserName"] = "HAJER EISA";

                    #endregion

                    await RCRepo.AddReceiveCaseAsync(RC);
                    TempData["SuccessMessage"] = "تم إضافة البيانات بنجاح";
                    return RedirectToAction("ConvictFileDetails", new { id = RC.FileNo });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while adding the procedure: " + ex.Message);
                    TempData["ErrorMessage"] = "حدث خطأ ما أثناء إضافة البيانات";
                }
            }
            return RedirectToAction("ConvictFileDetails", new { id = RC.FileNo });

        }


        #endregion

        #region Visit : Add new Report

        public ActionResult AddVisit(int id)

        {

            var file = CFRepo.GetConvictFileByIdAsync(id);

            ViewBag.Visit = new VisitDTO
            {
                FileNo = file.Id,
                CreatedDate = DateTime.Now,
                CreatedBy = "مريم سالم الحمادي",
                Status = "جديد",
                //CaseNo = file.Id.ToString(),
                EditNote = " ",
                EditedBy = " ",
                EditDate = null,
                ApproveDate = null,
                ApprovedBy = " ",
                ApproveNotes = " ",
            };

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddVisit(VisitDTO V)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int? id = V.ReportID;
                    V.CreatedBy = string.IsNullOrWhiteSpace(V.CreatedBy) ? "مريم سالم الحمادي" : V.CreatedBy;
                    V.CreatedDate = DateTime.Now;
                    V.EditedBy = string.IsNullOrWhiteSpace(V.EditedBy) ? " " : V.EditedBy;
                    V.EditNote = string.IsNullOrWhiteSpace(V.EditNote) ? " " : V.EditNote;
                    V.ApprovedBy = string.IsNullOrWhiteSpace(V.ApprovedBy) ? " " : V.ApprovedBy;
                    V.ApproveNotes = string.IsNullOrWhiteSpace(V.ApproveNotes) ? " " : V.ApproveNotes;

                    #region Logs

                    log4net.LogicalThreadContext.Properties["Controller"] = this.ControllerContext.RouteData.Values["controller"];
                    log4net.LogicalThreadContext.Properties["Action"] = this.ControllerContext.RouteData.Values["action"];
                    log4net.LogicalThreadContext.Properties["UserName"] = "HAJER EISA";

                    #endregion

                    await VRepo.AddVisitAsync(V);
                    TempData["SuccessMessage"] = "تم إضافة البيانات بنجاح";
                    return RedirectToAction("ConvictFileDetails", new { id = V.FileNo });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while adding the procedure: " + ex.Message);
                    TempData["ErrorMessage"] = "حدث خطأ ما أثناء إضافة البيانات";
                }
            }
            return RedirectToAction("ConvictFileDetails", new { id = V.FileNo });

        }


        #endregion

        #region Attachment : Add new Report

        public ActionResult AddAttachment(int id)

        {

            var file = CFRepo.GetConvictFileByIdAsync(id);

            ViewBag.Attachment = new AttachmentDTO
            {
                FileNo = file.Id,
                CreatedDate = DateTime.Now,
                CreatedBy = "مريم سالم الحمادي",
                Status = "جديد",
                EditNote = " ",
                EditedBy = " ",
                EditDate = null,
                ApproveDate = null,
                ApprovedBy = " ",
                ApproveNotes = " ",
            };

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddAttachment(AttachmentDTO attch,
    IEnumerable<HttpPostedFileBase> UploadFiles)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int? id = attch.AttachmentID;
                    attch.CreatedBy = string.IsNullOrWhiteSpace(attch.CreatedBy) ? "مريم سالم الحمادي" : attch.CreatedBy;
                    attch.CreatedDate = DateTime.Now;
                    attch.EditedBy = string.IsNullOrWhiteSpace(attch.EditedBy) ? " " : attch.EditedBy;
                    attch.EditNote = string.IsNullOrWhiteSpace(attch.EditNote) ? " " : attch.EditNote;
                    attch.ApprovedBy = string.IsNullOrWhiteSpace(attch.ApprovedBy) ? " " : attch.ApprovedBy;
                    attch.ApproveNotes = string.IsNullOrWhiteSpace(attch.ApproveNotes) ? " " : attch.ApproveNotes;

                    #region Logs

                    log4net.LogicalThreadContext.Properties["Controller"] = this.ControllerContext.RouteData.Values["controller"];
                    log4net.LogicalThreadContext.Properties["Action"] = this.ControllerContext.RouteData.Values["action"];
                    log4net.LogicalThreadContext.Properties["UserName"] = "HAJER EISA";

                    #endregion
                    // Save المرفقات (AttachPhoto)
                    if (UploadFiles != null && UploadFiles.Any())
                    {
                        var file = UploadFiles.FirstOrDefault();
                        if (file != null && file.ContentLength > 0)
                        {
                            var extension = Path.GetExtension(file.FileName);
                            var fileName = $"{Guid.NewGuid()}{extension}";
                            var path = Server.MapPath("~/Content/Documents/Files");

                            if (!Directory.Exists(path))
                                Directory.CreateDirectory(path);

                            var filePath = Path.Combine(path, fileName);
                            file.SaveAs(filePath);

                            attch.Attachments = $"~/Content/Documents/Files/{fileName}"; // Adjust property name as needed
                        }

                    }
                    await aRepo.AddAttachmentAsync(attch);
                    TempData["SuccessMessage"] = "تم إضافة البيانات بنجاح";
                    return RedirectToAction("ConvictFileDetails", new { id = attch.FileNo });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while adding the procedure: " + ex.Message);
                    TempData["ErrorMessage"] = "حدث خطأ ما أثناء إضافة البيانات";
                }
            }
            return RedirectToAction("ConvictFileDetails", new { id = attch.FileNo });

        }


        #endregion

    }
}