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
    public class OfficerRepo
    {
        private readonly SqlHelper _helper = new SqlHelper();

        #region Get List Of Officer
        public async Task<List<OfficerDTO>> GetListOfficerAsync()
        {
            DataTable dt = await _helper.GetDataTableAsync("[Master].[SP_Officer_GetList]");
            var allOfficerList = new List<OfficerDTO>();

            foreach (DataRow dr in dt.Rows)
            {
                var Officer = new OfficerDTO
                {
                    OfficerId = int.Parse(dr["OfficerId"].ToString()),
                    OfficerName = dr["OfficerName"].ToString()
                };

                allOfficerList.Add(Officer);
            }

            return allOfficerList;
        }
        #endregion

        #region Add Officer
        public async Task AddOfficerAsync(OfficerDTO Officer)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@OfficerName", Officer.OfficerName)
    };
            await _helper.ExecuteNonQueryAsync("[Master].[SP_Officer_Add]", parameters);
        }
        #endregion

        #region Get Officer By Id
        public async Task<OfficerDTO> GetOfficerByIdAsync(int id)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@OfficerId", id)
    };
            DataTable dt = await _helper.GetDataTableAsync("[Master].[SP_Officer_GetById]", parameters);
            OfficerDTO Officer = null;

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                Officer = new OfficerDTO
                {
                    OfficerId = int.Parse(dr["OfficerId"].ToString()),
                    OfficerName = dr["OfficerName"].ToString()
                };
            }

            return Officer;
        }
        #endregion

        #region Update Officer
        public async Task UpdateOfficerAsync(OfficerDTO Officer)
        {
            IDbDataParameter[] parameters =
            {
            new SqlParameter("@OfficerId", Officer.OfficerId),
            new SqlParameter("@OfficerName", Officer.OfficerName)

};

            await _helper.ExecuteNonQueryAsync("[Master].[SP_Officer_Update]", parameters);
        }
        #endregion

        #region Delete Officer
        public async Task DeleteOfficerAsync(int id)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@OfficerId", id)
    };
            await _helper.ExecuteNonQueryAsync("[Master].[SP_Officer_Delete]", parameters);
        }
        #endregion
    }
}
