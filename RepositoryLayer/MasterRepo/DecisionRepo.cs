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
    public class DecisionRepo
    {
        private readonly SqlHelper _helper = new SqlHelper();

        #region Get List Of Decision
        public async Task<List<DecisionDTO>> GetListDecisionAsync()
        {
            DataTable dt = await _helper.GetDataTableAsync("[Master].[SP_Decision_GetList]");
            var allDecisionList = new List<DecisionDTO>();

            foreach (DataRow dr in dt.Rows)
            {
                var Decision = new DecisionDTO
                {
                    DecisionId = int.Parse(dr["DecisionId"].ToString()),
                    DecisionName = dr["DecisionName"].ToString()
                };

                allDecisionList.Add(Decision);
            }

            return allDecisionList;
        }
        #endregion

        #region Add Decision
        public async Task AddDecisionAsync(DecisionDTO Decision)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@DecisionName", Decision.DecisionName)
    };
            await _helper.ExecuteNonQueryAsync("[Master].[SP_Decision_Add]", parameters);
        }
        #endregion

        #region Get Decision By Id
        public async Task<DecisionDTO> GetDecisionByIdAsync(int id)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@DecisionId", id)
    };
            DataTable dt = await _helper.GetDataTableAsync("[Master].[SP_Decision_GetById]", parameters);
            DecisionDTO Decision = null;

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                Decision = new DecisionDTO
                {
                    DecisionId = int.Parse(dr["DecisionId"].ToString()),
                    DecisionName = dr["DecisionName"].ToString()
                };
            }

            return Decision;
        }
        #endregion

        #region Update Decision
        public async Task UpdateDecisionAsync(DecisionDTO Decision)
        {
            IDbDataParameter[] parameters =
            {
            new SqlParameter("@DecisionId", Decision.DecisionId),
            new SqlParameter("@DecisionName", Decision.DecisionName)

};

            await _helper.ExecuteNonQueryAsync("[Master].[SP_Decision_Update]", parameters);
        }
        #endregion

        #region Delete Decision
        public async Task DeleteDecisionAsync(int id)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@DecisionId", id)
    };
            await _helper.ExecuteNonQueryAsync("[Master].[SP_Decision_Delete]", parameters);
        }
        #endregion
    }
}
