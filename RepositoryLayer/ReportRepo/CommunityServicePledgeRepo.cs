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
    public class CommunityServicePledgeRepo
    {
        #region Connections
        private readonly SqlHelper _helper = new SqlHelper();
        #endregion

        #region Get List Of CommunityServicePledge
        public async Task<List<CommunityServicePledgeDTO>> GetListOfCommunityServicePledgeAsync()
        {
            DataTable dt = await _helper.GetDataTableAsync("[dbo].[SP_CommunityServicePledge_GetList]");
            var allCommunityServicePledgeList = new List<CommunityServicePledgeDTO>();

            foreach (DataRow dr in dt.Rows)
            {
                var communityservicepledge = new CommunityServicePledgeDTO
                {
                    ReportID = Int32.Parse(dr["ReportID"].ToString()),
                    FileNo = Int32.Parse(dr["FileNo"].ToString()),               
                    AccusationType = dr["AccusationType"].ToString(),
                    StartDate = DateTime.Parse(dr["StartDate"].ToString()),
                    EndDate = dr["EndDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(dr["EndDate"]) : null,
                    CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString()),
                    CreatedBy = dr["CreatedBy"].ToString(),

                };

                allCommunityServicePledgeList.Add(communityservicepledge);
            }

            return allCommunityServicePledgeList;
        }
        #endregion

        #region Add CommunityServicePledge
        public async Task AddCommunityServicePledgeAsync(CommunityServicePledgeDTO communityservicepledge)
        {
            IDbDataParameter[] parameters =
            {
                new SqlParameter("@FileNo", communityservicepledge.FileNo),
                new SqlParameter("@AccusationType", communityservicepledge.AccusationType),
                new SqlParameter("@StartDate", communityservicepledge.StartDate),
                new SqlParameter("@EndDate", communityservicepledge.EndDate ?? (object)DBNull.Value),
                new SqlParameter("@Signature", communityservicepledge.Signature ?? (object)DBNull.Value),
                new SqlParameter("@CreatedBy", communityservicepledge.CreatedBy),
                new SqlParameter("@CreatedDate", communityservicepledge.CreatedDate),
                new SqlParameter("@EditedBy", communityservicepledge.EditedBy ??(object) DBNull.Value),
                new SqlParameter("@EditDate", communityservicepledge.EditDate ?? (object)DBNull.Value),
                new SqlParameter("@EditNote", communityservicepledge.EditNote ??(object) DBNull.Value),
                new SqlParameter("@Status", communityservicepledge.Status ??(object) DBNull.Value),
                new SqlParameter("@ApprovedBy", communityservicepledge.ApprovedBy),
                new SqlParameter("@ApproveDate", communityservicepledge.ApproveDate ??(object) DBNull.Value),
                new SqlParameter("@ApproveNotes", communityservicepledge.ApproveNotes ??(object) DBNull.Value),
            };

            await _helper.ExecuteNonQueryAsync("[dbo].[SP_CommunityServicePledge_Add]", parameters);
        }
        #endregion

        #region Get CommunityServicePledge By Id
        public async Task<CommunityServicePledgeDTO> GetCommunityServicePledgeByIdAsync(int reprotID)
        {
            IDbDataParameter[] parameters =
            {
                new SqlParameter("@ReportID", reprotID)
            };
            DataTable dt = await _helper.GetDataTableAsync("[dbo].[SP_CommunityServicePledge_GetByID]", parameters);
            CommunityServicePledgeDTO communityservicepledge = null;

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                communityservicepledge = new CommunityServicePledgeDTO
                {

                    ReportID = Int32.Parse(dr["ReportID"].ToString()),
                    FileNo = Int32.Parse(dr["FileNo"].ToString()),
                    AccusationType = dr["AccusationType"].ToString(),
                    StartDate = DateTime.Parse(dr["StartDate"].ToString()),
                    EndDate = dr["EndDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(dr["EndDate"]) : null,
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

            return communityservicepledge;
        }
        #endregion

        #region Update CommunityServicePledge
        public async Task UpdateCommunityServicePledgeAsync(CommunityServicePledgeDTO communityservicepledge)
        {
            IDbDataParameter[] parameters =
            {
                new SqlParameter("@ReportID", communityservicepledge.ReportID),
                new SqlParameter("@FileNo", communityservicepledge.FileNo),
                new SqlParameter("@AccusationType", communityservicepledge.AccusationType),
                new SqlParameter("@StartDate", communityservicepledge.StartDate),
                new SqlParameter("@EndDate", communityservicepledge.EndDate ?? (object)DBNull.Value),
                new SqlParameter("@Signature", communityservicepledge.Signature ?? (object)DBNull.Value),
                new SqlParameter("@CreatedBy", communityservicepledge.CreatedBy),
                new SqlParameter("@CreatedDate", communityservicepledge.CreatedDate),
                new SqlParameter("@EditedBy", communityservicepledge.EditedBy ??(object) DBNull.Value),
                new SqlParameter("@EditDate", communityservicepledge.EditDate ?? (object)DBNull.Value),
                new SqlParameter("@EditNote", communityservicepledge.EditNote ??(object) DBNull.Value),
                new SqlParameter("@Status", communityservicepledge.Status ??(object) DBNull.Value),
                new SqlParameter("@ApprovedBy", communityservicepledge.ApprovedBy),
                new SqlParameter("@ApproveDate", communityservicepledge.ApproveDate ??(object) DBNull.Value),
                new SqlParameter("@ApproveNotes", communityservicepledge.ApproveNotes ??(object) DBNull.Value),
            };

            await _helper.ExecuteNonQueryAsync("[dbo].[SP_CommunityServicePledge_Update]", parameters);
        }
        #endregion


    }
}
