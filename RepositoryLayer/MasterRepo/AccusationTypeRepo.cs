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
    public class AccusationTypeRepo
    {
        private readonly SqlHelper _helper = new SqlHelper();

        #region Get List Of AccusationType
        public async Task<List<AccusationTypeDTO>> GetListAccusationTypeAsync()
        {
            DataTable dt = await _helper.GetDataTableAsync("[Master].[SP_AccusationType_GetList]");
            var allAccusationTypeList = new List<AccusationTypeDTO>();

            foreach (DataRow dr in dt.Rows)
            {
                var AccusationType = new AccusationTypeDTO
                {
                    AccusationTypeId = int.Parse(dr["AccusationTypeId"].ToString()),
                    AccusationTypeName = dr["AccusationTypeName"].ToString()
                };

                allAccusationTypeList.Add(AccusationType);
            }

            return allAccusationTypeList;
        }
        #endregion

        #region Add AccusationType
        public async Task AddAccusationTypeAsync(AccusationTypeDTO AccusationType)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@AccusationTypeName", AccusationType.AccusationTypeName)
    };
            await _helper.ExecuteNonQueryAsync("[Master].[SP_AccusationType_Add]", parameters);
        }
        #endregion

        #region Get AccusationType By Id
        public async Task<AccusationTypeDTO> GetAccusationTypeByIdAsync(int id)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@AccusationTypeId", id)
    };
            DataTable dt = await _helper.GetDataTableAsync("[Master].[SP_AccusationType_GetById]", parameters);
            AccusationTypeDTO AccusationType = null;

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                AccusationType = new AccusationTypeDTO
                {
                    AccusationTypeId = int.Parse(dr["AccusationTypeId"].ToString()),
                    AccusationTypeName = dr["AccusationTypeName"].ToString()
                };
            }

            return AccusationType;
        }
        #endregion

        #region Update AccusationType
        public async Task UpdateAccusationTypeAsync(AccusationTypeDTO AccusationType)
        {
            IDbDataParameter[] parameters =
            {
            new SqlParameter("@AccusationTypeId", AccusationType.AccusationTypeId),
            new SqlParameter("@AccusationTypeName", AccusationType.AccusationTypeName)

};

            await _helper.ExecuteNonQueryAsync("[Master].[SP_AccusationType_Update]", parameters);
        }
        #endregion

        #region Delete AccusationType
        public async Task DeleteNationalityAsync(int id)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@NationalityId", id)
    };
            await _helper.ExecuteNonQueryAsync("[Master].[SP_Nationality_Delete]", parameters);
        }
        #endregion
    }
}
