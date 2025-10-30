using DAL;
using SharedLayer.Master;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.MasterRepo
{
    public class VisitsRepo
    {
        private readonly SqlHelper _helper = new SqlHelper();

        #region Get List Of Visits
        public async Task<List<VisitsDTO>> GetListVisitsAsync()
        {
            DataTable dt = await _helper.GetDataTableAsync("[Master].[SP_Visits_GetList]");
            var allVisitsList = new List<VisitsDTO>();

            foreach (DataRow dr in dt.Rows)
            {
                var Visits = new VisitsDTO
                {
                    VisitsId = int.Parse(dr["VisitsId"].ToString()),
                    VisitsName = dr["VisitsName"].ToString()
                };

                allVisitsList.Add(Visits);
            }

            return allVisitsList;
        }
        #endregion

        #region Add Visits
        public async Task AddVisitsAsync(VisitsDTO Visits)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@VisitsName", Visits.VisitsName)
    };
            await _helper.ExecuteNonQueryAsync("[Master].[SP_Visits_Add]", parameters);
        }
        #endregion

        #region Get Visits By Id
        public async Task<VisitsDTO> GetVisitsByIdAsync(int id)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@VisitsId", id)
    };
            DataTable dt = await _helper.GetDataTableAsync("[Master].[SP_Visits_GetById]", parameters);
            VisitsDTO Visits = null;

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                Visits = new VisitsDTO
                {
                    VisitsId = int.Parse(dr["VisitsId"].ToString()),
                    VisitsName = dr["VisitsName"].ToString()
                };
            }

            return Visits;
        }
        #endregion

        #region Update Visits
        public async Task UpdateVisitsAsync(VisitsDTO Visits)
        {
            IDbDataParameter[] parameters =
            {
            new SqlParameter("@VisitsId", Visits.VisitsId),
            new SqlParameter("@VisitsName", Visits.VisitsName)

};

            await _helper.ExecuteNonQueryAsync("[Master].[SP_Visits_Update]", parameters);
        }
        #endregion

        #region Delete Visits
        public async Task DeleteVisitsAsync(int id)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@VisitsId", id)
    };
            await _helper.ExecuteNonQueryAsync("[Master].[SP_Visits_Delete]", parameters);
        }
        #endregion
    }
}
