using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLayer.Models;
using System.Data.SqlClient;

namespace RepositoryLayer.ReportRepo
{
    public class CommunityServiceRepo
    {

        #region Connections
        private readonly SqlHelper _helper = new SqlHelper();
        #endregion

        #region Get List Of CommunityService
        public async Task<List<CommunityServiceDTO>> GetListOfCommunityServiceAsync()
        {
            DataTable dt = await _helper.GetDataTableAsync("[dbo].[SP_CommunityService_GetList]");
            var allCommunityServiceList = new List<CommunityServiceDTO>();

            foreach (DataRow dr in dt.Rows)
            {
                var communityservice = new CommunityServiceDTO
                {
                    ReportID = Int32.Parse(dr["ReportID"].ToString()),
                    FileNo = Int32.Parse(dr["FileNo"].ToString()),   
                    Blame = dr["Blame"].ToString(),
                    CaseNo = dr["CaseNo"].ToString(),
                    StartDate = DateTime.Parse(dr["StartDate"].ToString()),
                    EndDate = DateTime.Parse(dr["EndDate"].ToString()),
                    CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString()),
                    CreatedBy = dr["CreatedBy"].ToString(),

                };

                allCommunityServiceList.Add(communityservice);
            }

            return allCommunityServiceList;
        }
        #endregion

        #region Add CommunityService
        public async Task AddCommunityServiceAsync(CommunityServiceDTO communityservice)
        {
            IDbDataParameter[] parameters =
            {
                new SqlParameter("@FileNo", communityservice.FileNo),             
                new SqlParameter("@CaseNo", communityservice.CaseNo),
                new SqlParameter("@JudgmentDate", communityservice.JudgmentDate),
                new SqlParameter("@CommunityServicePlaces", communityservice.CommunityServicePlaces),
                new SqlParameter("@SupervisorName", communityservice.SupervisorName),
                new SqlParameter("@OfficerName", communityservice.OfficerName),
                new SqlParameter("@Blame", communityservice.Blame),
                new SqlParameter("@PhysicalPychologicalStatus", communityservice.PhysicalPychologicalStatus),
                new SqlParameter("@ExecutiveVisits", communityservice.ExecutiveVisits),
                new SqlParameter("@StartDate", communityservice.StartDate),
                new SqlParameter("@EndDate", communityservice.EndDate),
                new SqlParameter("@CreatedBy", communityservice.CreatedBy),
                new SqlParameter("@CreatedDate", communityservice.CreatedDate),
                new SqlParameter("@EditedBy", communityservice.EditedBy),
                new SqlParameter("@EditDate", (object)communityservice.EditDate ?? DBNull.Value),
                new SqlParameter("@EditNote", communityservice.EditNote),
                new SqlParameter("@Status", communityservice.Status),
                new SqlParameter("@ApprovedBy", communityservice.ApprovedBy),
                new SqlParameter("@ApproveDate", (object)communityservice.ApproveDate ?? DBNull.Value),
                new SqlParameter("@ApproveNotes", communityservice.ApproveNotes),
            };

            await _helper.ExecuteNonQueryAsync("[dbo].[SP_CommunityService_Add]", parameters);
        }
        #endregion

        #region Get CommunityReport By Id
        public async Task<CommunityServiceDTO> GetCommunityServiceByIdAsync(int reprotID)
        {
            IDbDataParameter[] parameters =
            {
                new SqlParameter("@ReportID", reprotID)
            };
            DataTable dt = await _helper.GetDataTableAsync("[dbo].[SP_CommunityService_GetByID]", parameters);
            CommunityServiceDTO communityservice = null;

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                communityservice = new CommunityServiceDTO
                {

                    ReportID = Int32.Parse(dr["ReportID"].ToString()),
                    FileNo = Int32.Parse(dr["FileNo"].ToString()),    
                    JudgmentDate = DateTime.Parse(dr["JudgmentDate"].ToString()),
                    CommunityServicePlaces = dr["CommunityServicePlaces"].ToString(),
                    SupervisorName = dr["SupervisorName"].ToString(),
                    OfficerName = dr["OfficerName"].ToString(),
                    Blame = dr["Blame"].ToString(),
                    PhysicalPychologicalStatus = dr["PhysicalPychologicalStatus"].ToString(),
                    ExecutiveVisits = dr["ExecutiveVisits"].ToString(),
                    CaseNo = dr["CaseNo"].ToString(),
                    StartDate = DateTime.Parse(dr["StartDate"].ToString()),
                    EndDate = DateTime.Parse(dr["EndDate"].ToString()),
                    CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString()),
                    CreatedBy = dr["CreatedBy"].ToString(),
                    EditDate = dr["EditDate"] != DBNull.Value ? Convert.ToDateTime(dr["EditDate"]) : (DateTime?)null,
                    EditedBy = dr["EditedBy"].ToString(),
                    EditNote = dr["EditNote"].ToString(),
                    Status = dr["Status"].ToString(),
                    ApprovedBy = dr["ApprovedBy"].ToString(),
                    ApproveDate = dr["ApproveDate"] != DBNull.Value ? Convert.ToDateTime(dr["ApproveDate"]) : (DateTime?)null,

                };
            }

            return communityservice;
        }
        #endregion

        #region Update CommunityService
        public async Task UpdateCommunityServiceAsync(CommunityServiceDTO communityservice)
        {
            IDbDataParameter[] parameters =
            {
                new SqlParameter("@ReportID", communityservice.ReportID),
                new SqlParameter("@FileNo", communityservice.FileNo),
                new SqlParameter("@CaseNo", communityservice.CaseNo),
                new SqlParameter("@JudgmentDate", communityservice.JudgmentDate),
                new SqlParameter("@CommunityServicePlaces", communityservice.CommunityServicePlaces),
                new SqlParameter("@SupervisorName", communityservice.SupervisorName),
                new SqlParameter("@OfficerName", communityservice.OfficerName),
                new SqlParameter("@Blame", communityservice.Blame),
                new SqlParameter("@PhysicalPychologicalStatus", communityservice.PhysicalPychologicalStatus),
                new SqlParameter("@ExecutiveVisits", communityservice.ExecutiveVisits),
                new SqlParameter("@StartDate", communityservice.StartDate),
                new SqlParameter("@EndDate", communityservice.EndDate),
                new SqlParameter("@CreatedBy", communityservice.CreatedBy),
                new SqlParameter("@CreatedDate", communityservice.CreatedDate),
                new SqlParameter("@EditedBy", communityservice.EditedBy),
                new SqlParameter("@EditDate", communityservice.EditDate),
                new SqlParameter("@EditNote", communityservice.EditNote),
                new SqlParameter("@Status", communityservice.Status),
                new SqlParameter("@ApprovedBy", communityservice.ApprovedBy),
                new SqlParameter("@ApproveDate", communityservice.ApproveDate),
                new SqlParameter("@ApproveNotes", communityservice.ApproveNotes),
            };

            await _helper.ExecuteNonQueryAsync("[dbo].[SP_CommunityService_Update]", parameters);
        }
        #endregion
    }
}
