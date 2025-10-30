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
    public class JudgmentTypeRepo
    {
        private readonly SqlHelper _helper = new SqlHelper();

        #region Get List Of JudgmentType
        public async Task<List<JudgmentTypeDTO>> GetListJudgmentTypeAsync()
        {
            DataTable dt = await _helper.GetDataTableAsync("[Master].[SP_JudgmentType_GetList]");
            var allJudgmentTypeList = new List<JudgmentTypeDTO>();

            foreach (DataRow dr in dt.Rows)
            {
                var JudgmentType = new JudgmentTypeDTO
                {
                    JudgmentTypeId = int.Parse(dr["JudgmentTypeId"].ToString()),
                    JudgmentTypeName = dr["JudgmentTypeName"].ToString()
                };

                allJudgmentTypeList.Add(JudgmentType);
            }

            return allJudgmentTypeList;
        }
        #endregion

        #region Add JudgmentType
        public async Task AddJudgmentTypeAsync(JudgmentTypeDTO JudgmentType)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@JudgmentTypeName", JudgmentType.JudgmentTypeName)
    };
            await _helper.ExecuteNonQueryAsync("[Master].[SP_JudgmentType_Add]", parameters);
        }
        #endregion

        #region Get JudgmentType By Id
        public async Task<JudgmentTypeDTO> GetJudgmentTypeByIdAsync(int id)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@JudgmentTypeId", id)
    };
            DataTable dt = await _helper.GetDataTableAsync("[Master].[SP_JudgmentType_GetById]", parameters);
            JudgmentTypeDTO JudgmentType = null;

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                JudgmentType = new JudgmentTypeDTO
                {
                    JudgmentTypeId = int.Parse(dr["JudgmentTypeId"].ToString()),
                    JudgmentTypeName = dr["JudgmentTypeName"].ToString()
                };
            }

            return JudgmentType;
        }
        #endregion

        #region Update JudgmentType
        public async Task UpdateJudgmentTypeAsync(JudgmentTypeDTO JudgmentType)
        {
            IDbDataParameter[] parameters =
            {
            new SqlParameter("@JudgmentTypeId", JudgmentType.JudgmentTypeId),
            new SqlParameter("@JudgmentTypeName", JudgmentType.JudgmentTypeName)

};

            await _helper.ExecuteNonQueryAsync("[Master].[SP_JudgmentType_Update]", parameters);
        }
        #endregion

        #region Delete JudgmentType
        public async Task DeleteJudgmentTypeAsync(int id)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@JudgmentTypeId", id)
    };
            await _helper.ExecuteNonQueryAsync("[Master].[SP_JudgmentType_Delete]", parameters);
        }
        #endregion
    }
}
