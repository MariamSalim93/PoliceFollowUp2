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
                new SqlParameter("@VisitReport", visit.VisitReport ?? (object)DBNull.Value),
                new SqlParameter("@CreatedBy", visit.CreatedBy),
                new SqlParameter("@CreatedDate", visit.CreatedDate),
                new SqlParameter("@EditedBy", visit.EditedBy),
                new SqlParameter("@EditDate",visit.EditDate ?? (object)DBNull.Value),
                new SqlParameter("@EditNote", visit.EditNote ??(object) DBNull.Value),
                new SqlParameter("@Status", visit.Status ?? (object)DBNull.Value),
                new SqlParameter("@ApprovedBy", visit.ApprovedBy ??(object) DBNull.Value),
                new SqlParameter("@ApproveDate", visit.ApproveDate ?? (object)DBNull.Value),
                new SqlParameter("@ApproveNotes", visit.ApproveNotes ??(object) DBNull.Value),
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
                    VisitReport = dr["VisitReport"] != DBNull.Value ? Convert.ToString(dr["VisitReport"]) : null,
                    CreatedBy = dr["CreatedBy"].ToString(),
                    CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString()),
                    EditDate = dr["EditDate"] != DBNull.Value ? Convert.ToDateTime(dr["EditDate"]) : (DateTime?)null,
                    EditedBy = dr["EditedBy"] != DBNull.Value ? Convert.ToString(dr["EditedBy"]) : null,
                    EditNote = dr["EditNote"] != DBNull.Value ? Convert.ToString(dr["EditNote"]) : null,
                    Status = dr["Status"] != DBNull.Value ? Convert.ToString(dr["Status"]) : null,
                    ApprovedBy = dr["ApprovedBy"] != DBNull.Value ? Convert.ToString(dr["ApprovedBy"]) : null,
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
                new SqlParameter("@VisitReport", visit.VisitReport ?? (object)DBNull.Value),
                new SqlParameter("@CreatedBy", visit.CreatedBy),
                new SqlParameter("@CreatedDate", visit.CreatedDate),
                new SqlParameter("@EditedBy", visit.EditedBy),
                new SqlParameter("@EditDate",visit.EditDate ?? (object)DBNull.Value),
                new SqlParameter("@EditNote", visit.EditNote ??(object) DBNull.Value),
                new SqlParameter("@Status", visit.Status ?? (object)DBNull.Value),
                new SqlParameter("@ApprovedBy", visit.ApprovedBy ??(object) DBNull.Value),
                new SqlParameter("@ApproveDate", visit.ApproveDate ?? (object)DBNull.Value),
                new SqlParameter("@ApproveNotes", visit.ApproveNotes ??(object) DBNull.Value),
            };

            await _helper.ExecuteNonQueryAsync("[dbo].[SP_Visit_Update]", parameters);
        }
        #endregion

    }
}
