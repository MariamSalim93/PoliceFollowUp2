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
    public class ElectronicMonitoringRepo
    {
        #region Connections
        private readonly SqlHelper _helper = new SqlHelper();
        #endregion

        #region Get List Of Electronic Monitoring
        public async Task<List<ElectronicMonitoringDTO>> GetListOfElectronicMonitoringAsync()
        {
            DataTable dt = await _helper.GetDataTableAsync("[dbo].[SP_ElectronicMonitoring_GetList]");
            var allElectronicMonitoringList = new List<ElectronicMonitoringDTO>();

            foreach (DataRow dr in dt.Rows)
            {
                var electronicMonitoring = new ElectronicMonitoringDTO
                {
                    ReportID = Int32.Parse(dr["ReportID"].ToString()),
                    FileNo = Int32.Parse(dr["FileNo"].ToString()),
                    CaseNo = dr["CaseNo"].ToString(),
                    JudgmentType = dr["JudgmentType"].ToString(),
                    StartMonitoringDate = DateTime.Parse(dr["StartMonitoringDate"].ToString()),
                    EndMonitoringDate = DateTime.Parse(dr["EndMonitoringDate"].ToString()),
                    CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString()),
                    CreatedBy = dr["CreatedBy"].ToString(),

                };

                allElectronicMonitoringList.Add(electronicMonitoring);
            }

            return allElectronicMonitoringList;
        }
        #endregion

        #region Add Electronic Monitoring
        public async Task AddElectronicMonitoringAsync(ElectronicMonitoringDTO electronicMonitoring)
        {
            IDbDataParameter[] parameters =
            {
                new SqlParameter("@FileNo", electronicMonitoring.FileNo),
                new SqlParameter("@CaseNo", electronicMonitoring.CaseNo),
                new SqlParameter("@JudgmentType", electronicMonitoring.JudgmentType),
                new SqlParameter("@JudgmentDate", electronicMonitoring.JudgmentDate),
                new SqlParameter("@MonitoringArea", electronicMonitoring.MonitoringArea),
                new SqlParameter("@StartMonitoringDate", electronicMonitoring.StartMonitoringDate),
                new SqlParameter("@EndMonitoringDate", electronicMonitoring.EndMonitoringDate),
                new SqlParameter("@Blame", electronicMonitoring.Blame),
                new SqlParameter("@JudgmentSource", electronicMonitoring.JudgmentSource),
                new SqlParameter("@Attachments", electronicMonitoring.Attachments),
                new SqlParameter("@CreatedBy", electronicMonitoring.CreatedBy),
                new SqlParameter("@CreatedDate", electronicMonitoring.CreatedDate),
                new SqlParameter("@EditedBy", electronicMonitoring.EditedBy),
                new SqlParameter("@EditDate", electronicMonitoring.EditDate ?? (object)DBNull.Value),
                new SqlParameter("@EditNote", electronicMonitoring.EditNote),
                new SqlParameter("@Status", electronicMonitoring.Status),
                new SqlParameter("@ApprovedBy", electronicMonitoring.ApprovedBy),
                new SqlParameter("@ApproveDate", electronicMonitoring.ApproveDate ?? (object)DBNull.Value),
                new SqlParameter("@ApproveNotes", electronicMonitoring.ApproveNotes),
            };

            await _helper.ExecuteNonQueryAsync("[dbo].[SP_ElectronicMonitoring_Add]", parameters);
        }
        #endregion

        #region Get Electronic Monitoring By Id
        public async Task<ElectronicMonitoringDTO> GetElectronicMonitoringByIdAsync(int reprotID)
        {
            IDbDataParameter[] parameters =
            {
                new SqlParameter("@ReportID", reprotID)
            };
            DataTable dt = await _helper.GetDataTableAsync("[dbo].[SP_ElectronicMonitoring_GetByID]", parameters);
            ElectronicMonitoringDTO electronicMonitoring = null;

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                electronicMonitoring = new ElectronicMonitoringDTO
                {

                    ReportID = Int32.Parse(dr["ReportID"].ToString()),
                    FileNo = Int32.Parse(dr["FileNo"].ToString()),
                    CaseNo = dr["CaseNo"].ToString(),
                    JudgmentType = dr["JudgmentType"].ToString(),
                    JudgmentDate = DateTime.Parse(dr["JudgmentDate"].ToString()),
                    MonitoringArea = dr["MonitoringArea"].ToString(),
                    StartMonitoringDate = DateTime.Parse(dr["StartMonitoringDate"].ToString()),
                    EndMonitoringDate = DateTime.Parse(dr["EndMonitoringDate"].ToString()),
                    Blame = dr["Blame"].ToString(),
                    JudgmentSource = dr["JudgmentSource"].ToString(),
                    Attachments = dr["Attachments"].ToString(),
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

            return electronicMonitoring;
        }
        #endregion

        #region Update CommunityService
        public async Task UpdateElectronicMonitoringAsync(ElectronicMonitoringDTO electronicMonitoring)
        {
            IDbDataParameter[] parameters =
            {
                new SqlParameter("@ReportID", electronicMonitoring.ReportID),
                new SqlParameter("@FileNo", electronicMonitoring.FileNo),
                new SqlParameter("@CaseNo", electronicMonitoring.CaseNo),
                new SqlParameter("@JudgmentType", electronicMonitoring.JudgmentType),
                new SqlParameter("@JudgmentDate", electronicMonitoring.JudgmentDate),
                new SqlParameter("@MonitoringArea", electronicMonitoring.MonitoringArea),
                new SqlParameter("@StartMonitoringDate", electronicMonitoring.StartMonitoringDate),
                new SqlParameter("@EndMonitoringDate", electronicMonitoring.EndMonitoringDate),
                new SqlParameter("@Blame", electronicMonitoring.Blame),
                new SqlParameter("@JudgmentSource", electronicMonitoring.JudgmentSource),
                new SqlParameter("@Attachments", electronicMonitoring.Attachments),
                new SqlParameter("@CreatedBy", electronicMonitoring.CreatedBy),
                new SqlParameter("@CreatedDate", electronicMonitoring.CreatedDate),
                new SqlParameter("@EditedBy", electronicMonitoring.EditedBy),
                new SqlParameter("@EditDate", electronicMonitoring.EditDate ?? (object)DBNull.Value),
                new SqlParameter("@EditNote", electronicMonitoring.EditNote),
                new SqlParameter("@Status", electronicMonitoring.Status),
                new SqlParameter("@ApprovedBy", electronicMonitoring.ApprovedBy),
                new SqlParameter("@ApproveDate", electronicMonitoring.ApproveDate ?? (object)DBNull.Value),
                new SqlParameter("@ApproveNotes", electronicMonitoring.ApproveNotes),
            };

            await _helper.ExecuteNonQueryAsync("[dbo].[SP_ElectronicMonitoring_Update]", parameters);
        }
        #endregion

    }
}
