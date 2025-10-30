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
    public class PledgeHandcuffRepo
    {
        #region Connections
        private readonly SqlHelper _helper = new SqlHelper();
        #endregion

        #region Get List Of PledgeHandcuff
        public async Task<List<PledgeHandcuffDTO>> GetListOfPledgeHandcuffAsync()
        {
            DataTable dt = await _helper.GetDataTableAsync("[dbo].[SP_PledgeHandcuff_GetList]");
            var allPledgeHandcuffList = new List<PledgeHandcuffDTO>();

            foreach (DataRow dr in dt.Rows)
            {
                var pledgeHandcuff = new PledgeHandcuffDTO
                {
                    ReportID = Int32.Parse(dr["ReportID"].ToString()),
                    FileNo = Int32.Parse(dr["FileNo"].ToString()),
                    CaseNo = dr["CaseNo"].ToString(),
                    JudgmentIssued = dr["JudgmentIssued"].ToString(),
                    DeviceNo = dr["DeviceNo"].ToString(),
                    CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString()),
                    CreatedBy = dr["CreatedBy"].ToString(),

                };

                allPledgeHandcuffList.Add(pledgeHandcuff);
            }

            return allPledgeHandcuffList;
        }
        #endregion

        #region Add PledgeHandcuff
        public async Task AddPledgeHandcuffAsync(PledgeHandcuffDTO pledgeHandcuff)
        {
            IDbDataParameter[] parameters =
            {
                new SqlParameter("@FileNo", pledgeHandcuff.FileNo),
                new SqlParameter("@CaseNo", pledgeHandcuff.CaseNo),
                new SqlParameter("@JudgmentIssued", pledgeHandcuff.JudgmentIssued),
                new SqlParameter("@DeviceNo", pledgeHandcuff.DeviceNo),
                new SqlParameter("@NumberOfCharger", pledgeHandcuff.NumberOfCharger?? (object)DBNull.Value),
                new SqlParameter("@Signature", pledgeHandcuff.Signature ?? (object)DBNull.Value),
                new SqlParameter("@CreatedBy", pledgeHandcuff.CreatedBy),
                new SqlParameter("@CreatedDate", pledgeHandcuff.CreatedDate),
                new SqlParameter("@EditedBy", pledgeHandcuff.EditedBy ??(object) DBNull.Value),
                new SqlParameter("@EditDate", pledgeHandcuff.EditDate?? (object)DBNull.Value),
                new SqlParameter("@EditNote", pledgeHandcuff.EditNote ??(object) DBNull.Value),
                new SqlParameter("@Status", pledgeHandcuff.Status ??(object) DBNull.Value),
                new SqlParameter("@ApprovedBy", pledgeHandcuff.ApprovedBy ??(object) DBNull.Value),
                new SqlParameter("@ApproveDate", pledgeHandcuff.ApproveDate ?? (object)DBNull.Value),
                new SqlParameter("@ApproveNotes", pledgeHandcuff.ApproveNotes ??(object) DBNull.Value),
            };

            await _helper.ExecuteNonQueryAsync("[dbo].[SP_PledgeHandcuff_Add]", parameters);
        }
        #endregion

        #region Get PledgeHandcuff By Id
        public async Task<PledgeHandcuffDTO> GetPledgeHandcuffByIdAsync(int reprotID)
        {
            IDbDataParameter[] parameters =
            {
                new SqlParameter("@ReportID", reprotID)
            };
            DataTable dt = await _helper.GetDataTableAsync("[dbo].[SP_PledgeHandcuff_GetByID]", parameters);
            PledgeHandcuffDTO pledgeHandcuff = null;

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                pledgeHandcuff = new PledgeHandcuffDTO
                {

                    ReportID = Int32.Parse(dr["ReportID"].ToString()),
                    FileNo = Int32.Parse(dr["FileNo"].ToString()),
                    CaseNo = dr["CaseNo"].ToString(),
                    JudgmentIssued = dr["JudgmentIssued"].ToString(),
                    DeviceNo = dr["DeviceNo"].ToString(),      
                    NumberOfCharger = dr["NumberOfCharger"] != DBNull.Value ? Convert.ToString(dr["NumberOfCharger"]) : null,
                    Signature = dr["Signature"] != DBNull.Value ? Convert.ToString(dr["Signature"]) : null,
                    CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString()),
                    CreatedBy = dr["CreatedBy"].ToString(),
                    EditDate = dr["EditDate"] != DBNull.Value ? Convert.ToDateTime(dr["EditDate"]) : (DateTime?)null,
                    EditedBy = dr["EditedBy"] != DBNull.Value ? Convert.ToString(dr["EditedBy"]) : null,
                    EditNote = dr["EditNote"] != DBNull.Value ? Convert.ToString(dr["EditNote"]) : null,
                    Status = dr["Status"] != DBNull.Value ? Convert.ToString(dr["Status"]) : null,
                    ApprovedBy = dr["ApprovedBy"] != DBNull.Value ? Convert.ToString(dr["ApprovedBy"]) : null,
                    ApproveDate = dr["ApproveDate"] != DBNull.Value ? Convert.ToDateTime(dr["ApproveDate"]) : (DateTime?)null,

                };
            }

            return pledgeHandcuff;
        }
        #endregion

        #region Update PledgeHandcuff
        public async Task UpdatePledgeHandcuffAsync(PledgeHandcuffDTO pledgeHandcuff)
        {
            IDbDataParameter[] parameters =
            {
                new SqlParameter("@ReportID", pledgeHandcuff.ReportID),
                new SqlParameter("@FileNo", pledgeHandcuff.FileNo),
                new SqlParameter("@CaseNo", pledgeHandcuff.CaseNo),
                new SqlParameter("@JudgmentIssued", pledgeHandcuff.JudgmentIssued),
                new SqlParameter("@DeviceNo", pledgeHandcuff.DeviceNo),
                new SqlParameter("@NumberOfCharger", pledgeHandcuff.NumberOfCharger?? (object)DBNull.Value),
                new SqlParameter("@Signature", pledgeHandcuff.Signature ?? (object)DBNull.Value),
                new SqlParameter("@CreatedBy", pledgeHandcuff.CreatedBy),
                new SqlParameter("@CreatedDate", pledgeHandcuff.CreatedDate),
                new SqlParameter("@EditedBy", pledgeHandcuff.EditedBy ??(object) DBNull.Value),
                new SqlParameter("@EditDate", pledgeHandcuff.EditDate?? (object)DBNull.Value),
                new SqlParameter("@EditNote", pledgeHandcuff.EditNote ??(object) DBNull.Value),
                new SqlParameter("@Status", pledgeHandcuff.Status ??(object) DBNull.Value),
                new SqlParameter("@ApprovedBy", pledgeHandcuff.ApprovedBy ??(object) DBNull.Value),
                new SqlParameter("@ApproveDate", pledgeHandcuff.ApproveDate ?? (object)DBNull.Value),
                new SqlParameter("@ApproveNotes", pledgeHandcuff.ApproveNotes ??(object) DBNull.Value),
            };

            await _helper.ExecuteNonQueryAsync("[dbo].[SP_PledgeHandcuff_Update]", parameters);
        }
        #endregion

    }
}
