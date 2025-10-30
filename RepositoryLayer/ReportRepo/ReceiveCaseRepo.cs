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
    public class ReceiveCaseRepo
    {
        #region Connections
        private readonly SqlHelper _helper = new SqlHelper();
        #endregion

        #region Get List Of ReceiveCase
        public async Task<List<ReceiveCaseDTO>> GetListOfReceiveCaseAsync()
        {
            DataTable dt = await _helper.GetDataTableAsync("[dbo].[SP_ReceiveCase_GetList]");
            var allReceiveCaseList = new List<ReceiveCaseDTO>();

            foreach (DataRow dr in dt.Rows)
            {
                var receiveCase = new ReceiveCaseDTO
                {
                    ReportID = Int32.Parse(dr["ReportID"].ToString()),
                    FileNo = Int32.Parse(dr["FileNo"].ToString()),
                    StatusType = dr["StatusType"].ToString(),
                    Decision = dr["Decision"].ToString(),
                    JudgmentSource = dr["JudgmentSource"].ToString(),
                    StartDate = dr["StartDate"] != DBNull.Value ? Convert.ToDateTime(dr["StartDate"]) : (DateTime?)null,
                    EndDate = dr["EndDate"] != DBNull.Value ? Convert.ToDateTime(dr["EndDate"]) : (DateTime?)null,
                    CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString()),
                    CreatedBy = dr["CreatedBy"].ToString(),

                };

                allReceiveCaseList.Add(receiveCase);
            }

            return allReceiveCaseList;
        }
        #endregion

        #region Add ReceiveCase
        public async Task AddReceiveCaseAsync(ReceiveCaseDTO receiveCase)
        {
            IDbDataParameter[] parameters =
            {
                new SqlParameter("@FileNo", receiveCase.FileNo),
                new SqlParameter("@StatusType", receiveCase.StatusType),
                new SqlParameter("@Decision", receiveCase.Decision),
                new SqlParameter("@ImplementingAuthority", receiveCase.ImplementingAuthority),
                new SqlParameter("@JudgmentSource", receiveCase.JudgmentSource),
                new SqlParameter("@StartDate", receiveCase.StartDate?? (object)DBNull.Value),
                new SqlParameter("@EndDate", receiveCase.EndDate?? (object)DBNull.Value),
                new SqlParameter("@Info1", receiveCase.Info1?? (object)DBNull.Value),
                new SqlParameter("@Info2", receiveCase.Info2?? (object)DBNull.Value),
                new SqlParameter("@Info3", receiveCase.Info3?? (object)DBNull.Value),
                new SqlParameter("@Info4", receiveCase.Info4 ??(object) DBNull.Value),
                new SqlParameter("@Info5", receiveCase.Info5 ??(object) DBNull.Value),
                new SqlParameter("@Info6", receiveCase.Info6 ??(object) DBNull.Value),
                new SqlParameter("@Info7", receiveCase.Info7 ??(object) DBNull.Value),
                new SqlParameter("@CreatedBy", receiveCase.CreatedBy ??(object) DBNull.Value),
                new SqlParameter("@CreatedDate", receiveCase.CreatedDate ??(object) DBNull.Value),
                new SqlParameter("@EditedBy", receiveCase.EditedBy?? (object)DBNull.Value),
                new SqlParameter("@EditDate", receiveCase.EditDate ??(object) DBNull.Value),
                new SqlParameter("@EditNote", receiveCase.EditNote ??(object) DBNull.Value),
                new SqlParameter("@Status", receiveCase.Status ??(object) DBNull.Value),
                new SqlParameter("@ApprovedBy", receiveCase.ApprovedBy ??(object) DBNull.Value),
                new SqlParameter("@ApproveDate", receiveCase.ApproveDate ??(object) DBNull.Value),
                new SqlParameter("@ApproveNotes", receiveCase.ApproveNotes ??(object) DBNull.Value),
            };

            await _helper.ExecuteNonQueryAsync("[dbo].[SP_ReceiveCase_Add]", parameters);
        }
        #endregion

        #region Get ReceiveCase By Id
        public async Task<ReceiveCaseDTO> GetReceiveCaseByIdAsync(int reprotID)
        {
            IDbDataParameter[] parameters =
            {
                new SqlParameter("@ReportID", reprotID)
            };
            DataTable dt = await _helper.GetDataTableAsync("[dbo].[SP_ReceiveCase_GetByID]", parameters);
            ReceiveCaseDTO receiveCase = null;

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                receiveCase = new ReceiveCaseDTO
                {

                    ReportID = Int32.Parse(dr["ReportID"].ToString()),
                    FileNo = Int32.Parse(dr["FileNo"].ToString()),
                    StatusType = dr["StatusType"].ToString(),           
                    Decision = dr["Decision"].ToString(),
                    ImplementingAuthority = dr["ImplementingAuthority"].ToString(),
                    JudgmentSource = dr["JudgmentSource"].ToString(),
                    StartDate = dr["StartDate"] != DBNull.Value ? Convert.ToDateTime(dr["StartDate"]) : (DateTime?)null,
                    EndDate = dr["EndDate"] != DBNull.Value ? Convert.ToDateTime(dr["EndDate"]) : (DateTime?)null,
                    CreatedBy = dr["CreatedBy"].ToString(),
                    CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString()),
                    Info1 = dr["Info1"] != DBNull.Value ? dr["Info1"].ToString() : null,
                    Info2 = dr["Info2"] != DBNull.Value ? dr["Info2"].ToString() : null,
                    Info3 = dr["Info3"] != DBNull.Value ? dr["Info3"].ToString() : null,
                    Info4 = dr["Info4"] != DBNull.Value ? dr["Info4"].ToString() : null,
                    Info5 = dr["Info5"] != DBNull.Value ? dr["Info5"].ToString() : null,
                    Info6 = dr["Info6"] != DBNull.Value ? dr["Info6"].ToString() : null,
                    Info7 = dr["Info7"] != DBNull.Value ? dr["Info7"].ToString() : null,
                    EditDate = dr["EditDate"] != DBNull.Value ? Convert.ToDateTime(dr["EditDate"]) : (DateTime?)null,
                    EditedBy = dr["EditedBy"] != DBNull.Value ? dr["EditedBy"].ToString() : null,
                    EditNote = dr["EditNote"] != DBNull.Value ? dr["EditNote"].ToString() : null,
                    Status = dr["Status"].ToString(),
                    ApprovedBy = dr["ApprovedBy"].ToString(),
                    ApproveDate = dr["ApproveDate"] != DBNull.Value ? Convert.ToDateTime(dr["ApproveDate"]) : (DateTime?)null,

                };
            }

            return receiveCase;
        }
        #endregion

        #region Update ReceiveCase
        public async Task UpdateReceiveCaseAsync(ReceiveCaseDTO receiveCase)
        {
            IDbDataParameter[] parameters =
            {
                new SqlParameter("@ReportID", receiveCase.ReportID),
                new SqlParameter("@FileNo", receiveCase.FileNo),
                new SqlParameter("@StatusType", receiveCase.StatusType),
                new SqlParameter("@Decision", receiveCase.Decision),
                new SqlParameter("@ImplementingAuthority", receiveCase.ImplementingAuthority),
                new SqlParameter("@JudgmentSource", receiveCase.JudgmentSource),
                new SqlParameter("@StartDate", receiveCase.StartDate?? (object)DBNull.Value),
                new SqlParameter("@EndDate", receiveCase.EndDate?? (object)DBNull.Value),
                new SqlParameter("@Info1", receiveCase.Info1?? (object)DBNull.Value),
                new SqlParameter("@Info2", receiveCase.Info2?? (object)DBNull.Value),
                new SqlParameter("@Info3", receiveCase.Info3?? (object)DBNull.Value),
                new SqlParameter("@Info4", receiveCase.Info4 ??(object) DBNull.Value),
                new SqlParameter("@Info5", receiveCase.Info5 ??(object) DBNull.Value),
                new SqlParameter("@Info6", receiveCase.Info6 ??(object) DBNull.Value),
                new SqlParameter("@Info7", receiveCase.Info7 ??(object) DBNull.Value),
                new SqlParameter("@CreatedBy", receiveCase.CreatedBy ??(object) DBNull.Value),
                new SqlParameter("@CreatedDate", receiveCase.CreatedDate ??(object) DBNull.Value),
                new SqlParameter("@EditedBy", receiveCase.EditedBy?? (object)DBNull.Value),
                new SqlParameter("@EditDate", receiveCase.EditDate ??(object) DBNull.Value),
                new SqlParameter("@EditNote", receiveCase.EditNote ??(object) DBNull.Value),
                new SqlParameter("@Status", receiveCase.Status ??(object) DBNull.Value),
                new SqlParameter("@ApprovedBy", receiveCase.ApprovedBy ??(object) DBNull.Value),
                new SqlParameter("@ApproveDate", receiveCase.ApproveDate ??(object) DBNull.Value),
                new SqlParameter("@ApproveNotes", receiveCase.ApproveNotes ??(object) DBNull.Value),
            };

            await _helper.ExecuteNonQueryAsync("[dbo].[SP_ReceiveCase_Update]", parameters);
        }
        #endregion

    }
}
