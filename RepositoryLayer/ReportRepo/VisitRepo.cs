using DAL;
using SharedLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.ReportRepo
{
    public class VisitRepo
    {
        #region Connections
        private readonly SqlHelper _helper = new SqlHelper();
        #endregion

        #region Get List Of Visit
        public async Task<List<VisitDTO>> GetListOfVisitAsync()
        {
            DataTable dt = await _helper.GetDataTableAsync("[dbo].[SP_Visit_GetList]");
            var allVisitList = new List<VisitDTO>();

            foreach (DataRow dr in dt.Rows)
            {
                var visit = new VisitDTO
                {
                    ReportID = Int32.Parse(dr["ReportID"].ToString()),
                    FileNo = Int32.Parse(dr["FileNo"].ToString()),
                    VisitDate = DateTime.Parse(dr["VisitDate"].ToString()),
                    VisitPlace = dr["VisitPlace"].ToString(),
                    ImplementingAuthority = dr["ImplementingAuthority"].ToString(),
                    CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString()),
                    CreatedBy = dr["CreatedBy"].ToString(),

                };

                allVisitList.Add(visit);
            }

            return allVisitList;
        }
        #endregion

        #region Add Visit
        public async Task AddVisitAsync(VisitDTO visit)
        {
            IDbDataParameter[] parameters =
            {
                new SqlParameter("@FileNo", visit.FileNo),
                new SqlParameter("@VisitDate", visit.VisitDate),
                new SqlParameter("@VisitPlace", visit.VisitPlace),
                new SqlParameter("@ImplementingAuthority", visit.ImplementingAuthority),
                new SqlParameter("@VisitReport", visit.VisitReport),
                new SqlParameter("@CreatedBy", visit.CreatedBy),
                new SqlParameter("@CreatedDate", visit.CreatedDate),
                new SqlParameter("@EditedBy", visit.EditedBy),
                new SqlParameter("@EditDate", (object)visit.EditDate ?? DBNull.Value),
                new SqlParameter("@EditNote", visit.EditNote),
                new SqlParameter("@Status", visit.Status),
                new SqlParameter("@ApprovedBy", visit.ApprovedBy),
                new SqlParameter("@ApproveDate", (object)visit.ApproveDate ?? DBNull.Value),
                new SqlParameter("@ApproveNotes", visit.ApproveNotes),
            };

            await _helper.ExecuteNonQueryAsync("[dbo].[SP_Visit_Add]", parameters);
        }
        #endregion

        #region Get Visit By Id
        public async Task<VisitDTO> GetVisitByIdAsync(int reprotID)
        {
            IDbDataParameter[] parameters =
            {
                new SqlParameter("@ReportID", reprotID)
            };
            DataTable dt = await _helper.GetDataTableAsync("[dbo].[SP_Visit_GetByID]", parameters);
            VisitDTO visit = null;

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                visit = new VisitDTO
                {

                    ReportID = Int32.Parse(dr["ReportID"].ToString()),
                    FileNo = Int32.Parse(dr["FileNo"].ToString()),
                    VisitDate = DateTime.Parse(dr["VisitDate"].ToString()),
                    VisitPlace = dr["VisitPlace"].ToString(),
                    ImplementingAuthority = dr["ImplementingAuthority"].ToString(),
                    VisitReport = dr["VisitReport"].ToString(),
                    CreatedBy = dr["CreatedBy"].ToString(),
                    CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString()),
                    EditDate = dr["EditDate"] != DBNull.Value ? Convert.ToDateTime(dr["EditDate"]) : (DateTime?)null,
                    EditedBy = dr["EditedBy"].ToString(),
                    EditNote = dr["EditNote"].ToString(),
                    Status = dr["Status"].ToString(),
                    ApprovedBy = dr["ApprovedBy"].ToString(),
                    ApproveDate = dr["ApproveDate"] != DBNull.Value ? Convert.ToDateTime(dr["ApproveDate"]) : (DateTime?)null,

                };
            }

            return visit;
        }
        #endregion

        #region Update Visit
        public async Task UpdateVisitAsync(VisitDTO visit)
        {
            IDbDataParameter[] parameters =
            {
                new SqlParameter("@ReportID", visit.ReportID),
                new SqlParameter("@FileNo", visit.FileNo),
                new SqlParameter("@VisitDate", visit.VisitDate),
                new SqlParameter("@VisitPlace", visit.VisitPlace),
                new SqlParameter("@ImplementingAuthority", visit.ImplementingAuthority),
                new SqlParameter("@VisitReport", visit.VisitReport),
                new SqlParameter("@CreatedBy", visit.CreatedBy),
                new SqlParameter("@CreatedDate", visit.CreatedDate),
                new SqlParameter("@EditedBy", visit.EditedBy),
                new SqlParameter("@EditDate", visit.EditDate),
                new SqlParameter("@EditNote", visit.EditNote),
                new SqlParameter("@Status", visit.Status),
                new SqlParameter("@ApprovedBy", visit.ApprovedBy),
                new SqlParameter("@ApproveDate", visit.ApproveDate),
                new SqlParameter("@ApproveNotes", visit.ApproveNotes),
            };

            await _helper.ExecuteNonQueryAsync("[dbo].[SP_Visit_Update]", parameters);
        }
        #endregion

    }
}
