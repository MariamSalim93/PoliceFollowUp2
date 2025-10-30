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
    public class JudgmentSourceRepo
    {
        private readonly SqlHelper _helper = new SqlHelper();

        #region Get List Of JudgmentSource
        public async Task<List<JudgmentSourceDTO>> GetListJudgmentSourceAsync()
        {
            DataTable dt = await _helper.GetDataTableAsync("[Master].[SP_JudgmentSource_GetList]");
            var allJudgmentSourceList = new List<JudgmentSourceDTO>();

            foreach (DataRow dr in dt.Rows)
            {
                var JudgmentSource = new JudgmentSourceDTO
                {
                    JudgmentSourceId = int.Parse(dr["JudgmentSourceId"].ToString()),
                    JudgmentSourceName = dr["JudgmentSourceName"].ToString()
                };

                allJudgmentSourceList.Add(JudgmentSource);
            }

            return allJudgmentSourceList;
        }
        #endregion

        #region Add JudgmentSource
        public async Task AddJudgmentSourceAsync(JudgmentSourceDTO JudgmentSource)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@JudgmentSourceName", JudgmentSource.JudgmentSourceName)
    };
            await _helper.ExecuteNonQueryAsync("[Master].[SP_JudgmentSource_Add]", parameters);
        }
        #endregion

        #region Get JudgmentSource By Id
        public async Task<JudgmentSourceDTO> GetJudgmentSourceByIdAsync(int id)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@JudgmentSourceId", id)
    };
            DataTable dt = await _helper.GetDataTableAsync("[Master].[SP_JudgmentSource_GetById]", parameters);
            JudgmentSourceDTO JudgmentSource = null;

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                JudgmentSource = new JudgmentSourceDTO
                {
                    JudgmentSourceId = int.Parse(dr["JudgmentSourceId"].ToString()),
                    JudgmentSourceName = dr["JudgmentSourceName"].ToString()
                };
            }

            return JudgmentSource;
        }
        #endregion

        #region Update JudgmentSource
        public async Task UpdateJudgmentSourceAsync(JudgmentSourceDTO JudgmentSource)
        {
            IDbDataParameter[] parameters =
            {
            new SqlParameter("@JudgmentSourceId", JudgmentSource.JudgmentSourceId),
            new SqlParameter("@JudgmentSourceName", JudgmentSource.JudgmentSourceName)

};

            await _helper.ExecuteNonQueryAsync("[Master].[SP_JudgmentSource_Update]", parameters);
        }
        #endregion

        #region Delete JudgmentSource
        public async Task DeleteJudgmentSourceAsync(int id)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@JudgmentSourceId", id)
    };
            await _helper.ExecuteNonQueryAsync("[Master].[SP_JudgmentSource_Delete]", parameters);
        }
        #endregion
    }
}
