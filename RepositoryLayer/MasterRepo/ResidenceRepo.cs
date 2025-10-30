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
    public class ResidenceRepo
    {
        private readonly SqlHelper _helper = new SqlHelper();

        #region Get List Of Residence
        public async Task<List<ResidenceDTO>> GetListResidenceAsync()
        {
            DataTable dt = await _helper.GetDataTableAsync("[Master].[SP_Residence_GetList]");
            var allResidenceList = new List<ResidenceDTO>();

            foreach (DataRow dr in dt.Rows)
            {
                var Residence = new ResidenceDTO
                {
                    ResidenceId = int.Parse(dr["ResidenceId"].ToString()),
                    ResidenceName = dr["ResidenceName"].ToString()
                };

                allResidenceList.Add(Residence);
            }

            return allResidenceList;
        }
        #endregion

        #region Add Residence
        public async Task AddResidenceAsync(ResidenceDTO Residence)
        {
            IDbDataParameter[] parameters =
            {
            new SqlParameter("@ResidenceName", Residence.ResidenceName)
        };
            await _helper.ExecuteNonQueryAsync("[Master].[SP_Residence_Add]", parameters);
        }
        #endregion

        #region Get Residence By Id
        public async Task<ResidenceDTO> GetResidenceByIdAsync(int id)
        {
            IDbDataParameter[] parameters =
            {
            new SqlParameter("@ResidenceId", id)
        };
            DataTable dt = await _helper.GetDataTableAsync("[Master].[SP_Residence_GetById]", parameters);
            ResidenceDTO Residence = null;

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                Residence = new ResidenceDTO
                {
                    ResidenceId = int.Parse(dr["ResidenceId"].ToString()),
                    ResidenceName = dr["ResidenceName"].ToString()
                };
            }

            return Residence;
        }
        #endregion

        #region Update Residence
        public async Task UpdateResidenceAsync(ResidenceDTO Residence)
        {
            IDbDataParameter[] parameters =
            {
                new SqlParameter("@ResidenceId", Residence.ResidenceId),
                new SqlParameter("@ResidenceName", Residence.ResidenceName)

    };

            await _helper.ExecuteNonQueryAsync("[Master].[SP_Residence_Update]", parameters);
        }
        #endregion

        #region Delete Residence
        public async Task DeleteResidenceAsync(int id)
        {
            IDbDataParameter[] parameters =
            {
            new SqlParameter("@ResidenceId", id)
        };
            await _helper.ExecuteNonQueryAsync("[Master].[SP_Residence_Delete]", parameters);
        }
        #endregion
    }
}
