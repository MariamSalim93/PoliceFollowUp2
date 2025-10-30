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
    public class PlacesRepo
    {
        private readonly SqlHelper _helper = new SqlHelper();

        #region Get List Of Places
        public async Task<List<PlacesDTO>> GetListPlacesAsync()
        {
            DataTable dt = await _helper.GetDataTableAsync("[Master].[SP_Places_GetList]");
            var allPlacesList = new List<PlacesDTO>();

            foreach (DataRow dr in dt.Rows)
            {
                var Places = new PlacesDTO
                {
                    PlacesId = int.Parse(dr["PlacesId"].ToString()),
                    PlacesName = dr["PlacesName"].ToString()
                };

                allPlacesList.Add(Places);
            }

            return allPlacesList;
        }
        #endregion

        #region Add Places
        public async Task AddPlacesAsync(PlacesDTO Places)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@PlacesName", Places.PlacesName)
    };
            await _helper.ExecuteNonQueryAsync("[Master].[SP_Places_Add]", parameters);
        }
        #endregion

        #region Get Places By Id
        public async Task<PlacesDTO> GetPlacesByIdAsync(int id)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@PlacesId", id)
    };
            DataTable dt = await _helper.GetDataTableAsync("[Master].[SP_Places_GetById]", parameters);
            PlacesDTO Places = null;

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                Places = new PlacesDTO
                {
                    PlacesId = int.Parse(dr["PlacesId"].ToString()),
                    PlacesName = dr["PlacesName"].ToString()
                };
            }

            return Places;
        }
        #endregion

        #region Update Places
        public async Task UpdatePlacesAsync(PlacesDTO Places)
        {
            IDbDataParameter[] parameters =
            {
            new SqlParameter("@PlacesId", Places.PlacesId),
            new SqlParameter("@PlacesName", Places.PlacesName)

};

            await _helper.ExecuteNonQueryAsync("[Master].[SP_Places_Update]", parameters);
        }
        #endregion

        #region Delete Places
        public async Task DeletePlacesAsync(int id)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@PlacesId", id)
    };
            await _helper.ExecuteNonQueryAsync("[Master].[SP_Places_Delete]", parameters);
        }
        #endregion
    }
}
