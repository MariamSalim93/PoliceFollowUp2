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
    public class NationalityRepo
    {
        private readonly SqlHelper _helper = new SqlHelper();

        #region Get List Of Nationality
        public async Task<List<NationalityDTO>> GetListNationalityAsync()
        {
            DataTable dt = await _helper.GetDataTableAsync("[Master].[SP_Nationality_GetList]");
            var allNationalityList = new List<NationalityDTO>();

            foreach (DataRow dr in dt.Rows)
            {
                var Nationality = new NationalityDTO
                {
                    NationalityId = int.Parse(dr["NationalityId"].ToString()),
                    NationalityName = dr["NationalityName"].ToString()
                };

                allNationalityList.Add(Nationality);
            }

            return allNationalityList;
        }
        #endregion

        #region Add Nationality
        public async Task AddNationalityAsync(NationalityDTO Nationality)
        {
            IDbDataParameter[] parameters =
            {
            new SqlParameter("@NationalityName", Nationality.NationalityName)
        };
            await _helper.ExecuteNonQueryAsync("[Master].[SP_Nationality_Add]", parameters);
        }
        #endregion

        #region Get Nationality By Id
        public async Task<NationalityDTO> GetNationalityByIdAsync(int id)
        {
            IDbDataParameter[] parameters =
            {
            new SqlParameter("@NationalityId", id)
        };
            DataTable dt = await _helper.GetDataTableAsync("[Master].[SP_Nationality_GetById]", parameters);
            NationalityDTO Nationality = null;

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                Nationality = new NationalityDTO
                {
                    NationalityId = int.Parse(dr["NationalityId"].ToString()),
                    NationalityName = dr["NationalityName"].ToString()
                };
            }

            return Nationality;
        }
        #endregion

        #region Update Nationality
        public async Task UpdateNationalityAsync(NationalityDTO Nationality)
        {
            IDbDataParameter[] parameters =
            {
                new SqlParameter("@NationalityId", Nationality.NationalityId),
                new SqlParameter("@NationalityName", Nationality.NationalityName)

    };

            await _helper.ExecuteNonQueryAsync("[Master].[SP_Nationality_Update]", parameters);
        }
        #endregion

        #region Delete Nationality
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
