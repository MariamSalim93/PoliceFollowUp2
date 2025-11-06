using DAL;
using SharedLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.ReportRepo
{
    public class FinalReportsRepo
    {
        #region Connections
        private readonly SqlHelper _helper = new SqlHelper();
        #endregion

        #region Get List Of Final Reports
        public async Task<List<FinalReportsDTO>> GetListOfFinalReportsAsync()
        {
            DataTable dt = await _helper.GetDataTableAsync("[dbo].[SP_FinalReports_GetList]");
            var allFinalReportsList = new List<FinalReportsDTO>();

            foreach (DataRow dr in dt.Rows)
            {
                var finalReports = new FinalReportsDTO
                {
                    ReportID = Int32.Parse(dr["ReportID"].ToString()),
                    FileNo = Int32.Parse(dr["FileNo"].ToString()),     
                    CaseNo = dr["CaseNo"].ToString(),
                    ImplementingAuthority = dr["ImplementingAuthority"].ToString(),
                    ImplementingDate = DateTime.Parse(dr["ImplementingDate"].ToString()),
                    EndDate = DateTime.Parse(dr["EndDate"].ToString()),
                    Status = dr["Status"].ToString(),
                    CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString()),
                    CreatedBy = dr["CreatedBy"].ToString(),

                };

                allFinalReportsList.Add(finalReports);
            }

            return allFinalReportsList;
        }
        #endregion

        #region Add Final Reports
        public async Task AddFinalReportsAsync(FinalReportsDTO finalReports)
        {
            IDbDataParameter[] parameters =
            {
                new SqlParameter("@FileNo", finalReports.FileNo),
                new SqlParameter("@CaseNo", finalReports.CaseNo),
                new SqlParameter("@ImplementingAuthority", finalReports.ImplementingAuthority),
                new SqlParameter("@ImplementingDate", finalReports.ImplementingDate),
                new SqlParameter("@EndDate", finalReports.EndDate),
                new SqlParameter("@ReportSummary", finalReports.ReportSummary?? (object)DBNull.Value),
                new SqlParameter("@CreatedBy", finalReports.CreatedBy),
                new SqlParameter("@CreatedDate", finalReports.CreatedDate),
                new SqlParameter("@EditedBy", finalReports.EditedBy?? (object)DBNull.Value),
                new SqlParameter("@EditDate", finalReports.EditDate ?? (object)DBNull.Value),
                new SqlParameter("@EditNote", finalReports.EditNote ??(object) DBNull.Value),
                new SqlParameter("@Status", finalReports.Status ??(object) DBNull.Value),
                new SqlParameter("@ApprovedBy", finalReports.ApprovedBy ??(object) DBNull.Value),
                new SqlParameter("@ApproveDate", finalReports.ApproveDate ?? (object)DBNull.Value),
                new SqlParameter("@ApproveNotes", finalReports.ApproveNotes ??(object) DBNull.Value),
            };

            await _helper.ExecuteNonQueryAsync("[dbo].[SP_FinalReports_Add]", parameters);
        }
        #endregion

        #region Get Final Reports By Id
        public async Task<FinalReportsDTO> GetFinalReportsByIdAsync(int reprotID)
        {
            IDbDataParameter[] parameters =
            {
                new SqlParameter("@ReportID", reprotID)
            };
            DataTable dt = await _helper.GetDataTableAsync("[dbo].[SP_FinalReports_GetByID]", parameters);
            FinalReportsDTO finalReports = null;

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                finalReports = new FinalReportsDTO
                {

                    ReportID = Int32.Parse(dr["ReportID"].ToString()),
                    FileNo = Int32.Parse(dr["FileNo"].ToString()),
                    CaseNo = dr["CaseNo"].ToString(),
                    ImplementingAuthority = dr["ImplementingAuthority"].ToString(),
                    ImplementingDate = DateTime.Parse(dr["ImplementingDate"].ToString()),
                    EndDate = DateTime.Parse(dr["EndDate"].ToString()),
                    ReportSummary = dr["ReportSummary"] != DBNull.Value ? Convert.ToString(dr["ReportSummary"]) : null,
                    CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString()),
                    CreatedBy = dr["CreatedBy"].ToString(),
                    EditDate = dr["EditDate"] != DBNull.Value ? Convert.ToDateTime(dr["EditDate"]) : (DateTime?)null,
                    EditedBy = dr["EditedBy"] != DBNull.Value ? Convert.ToString(dr["EditedBy"]) : null,
                    EditNote = dr["EditNote"] != DBNull.Value ? Convert.ToString(dr["EditNote"]) : null,
                    Status = dr["Status"] != DBNull.Value ? Convert.ToString(dr["Status"]) : null,
                    ApprovedBy = dr["ApprovedBy"] != DBNull.Value ? Convert.ToString(dr["ApprovedBy"]) : null,
                    ApproveNotes = dr["ApproveNotes"] != DBNull.Value ? dr["ApproveNotes"].ToString() : null,
                    ApproveDate = dr["ApproveDate"] != DBNull.Value ? Convert.ToDateTime(dr["ApproveDate"]) : (DateTime?)null,

                };
            }

            return finalReports;
        }
        #endregion

        #region Update Final Reports
        public async Task UpdateFinalReportsAsync(FinalReportsDTO finalReports)
        {
            IDbDataParameter[] parameters =
            {
                new SqlParameter("@ReportID", finalReports.ReportID),
                new SqlParameter("@FileNo", finalReports.FileNo),
                new SqlParameter("@CaseNo", finalReports.CaseNo),
                new SqlParameter("@ImplementingAuthority", finalReports.ImplementingAuthority),
                new SqlParameter("@ImplementingDate", finalReports.ImplementingDate),
                new SqlParameter("@EndDate", finalReports.EndDate),
                new SqlParameter("@ReportSummary", finalReports.ReportSummary?? (object)DBNull.Value),
                new SqlParameter("@CreatedBy", finalReports.CreatedBy),
                new SqlParameter("@CreatedDate", finalReports.CreatedDate),
                new SqlParameter("@EditedBy", finalReports.EditedBy?? (object)DBNull.Value),
                new SqlParameter("@EditDate", finalReports.EditDate ?? (object)DBNull.Value),
                new SqlParameter("@EditNote", finalReports.EditNote ??(object) DBNull.Value),
                new SqlParameter("@Status", finalReports.Status ??(object) DBNull.Value),
                new SqlParameter("@ApprovedBy", finalReports.ApprovedBy ??(object) DBNull.Value),
                new SqlParameter("@ApproveDate", finalReports.ApproveDate ?? (object)DBNull.Value),
                new SqlParameter("@ApproveNotes", finalReports.ApproveNotes ??(object) DBNull.Value),
            };

            await _helper.ExecuteNonQueryAsync("[dbo].[SP_FinalReports_Update]", parameters);
        }
        #endregion

        #region Approve Final Reports
        public async Task ApproveFinalReportsAsync(FinalReportsDTO finalReports)
        {
            IDbDataParameter[] parameters =
            {
            new SqlParameter("@ReportID", finalReports.ReportID),
            new SqlParameter("@Status", finalReports.Status),
            new SqlParameter("@ApprovedBy", finalReports.ApprovedBy),
            new SqlParameter("@ApproveDate", finalReports.ApproveDate),
            new SqlParameter("@ApproveNotes", finalReports.ApproveNotes)
        };

            await _helper.ExecuteNonQueryAsync("[dbo].[SP_FinalReports_Approve]", parameters);
        }
        #endregion

    }
}
