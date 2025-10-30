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
    public class AuthorityRepo
    {
        private readonly SqlHelper _helper = new SqlHelper();

        #region Get List Of Authority
        public async Task<List<AuthorityDTO>> GetListAuthorityAsync()
        {
            DataTable dt = await _helper.GetDataTableAsync("[Master].[SP_Authority_GetList]");
            var allAuthorityList = new List<AuthorityDTO>();

            foreach (DataRow dr in dt.Rows)
            {
                var Authority = new AuthorityDTO
                {
                    AuthorityId = int.Parse(dr["AuthorityId"].ToString()),
                    AuthorityName = dr["AuthorityName"].ToString()
                };

                allAuthorityList.Add(Authority);
            }

            return allAuthorityList;
        }
        #endregion

        #region Add Authority
        public async Task AddAuthorityAsync(AuthorityDTO Authority)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@AuthorityName", Authority.AuthorityName)
    };
            await _helper.ExecuteNonQueryAsync("[Master].[SP_Authority_Add]", parameters);
        }
        #endregion

        #region Get Authority By Id
        public async Task<AuthorityDTO> GetAuthorityByIdAsync(int id)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@AuthorityId", id)
    };
            DataTable dt = await _helper.GetDataTableAsync("[Master].[SP_Authority_GetById]", parameters);
            AuthorityDTO Authority = null;

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                Authority = new AuthorityDTO
                {
                    AuthorityId = int.Parse(dr["AuthorityId"].ToString()),
                    AuthorityName = dr["AuthorityName"].ToString()
                };
            }

            return Authority;
        }
        #endregion

        #region Update Authority
        public async Task UpdateAuthorityAsync(AuthorityDTO Authority)
        {
            IDbDataParameter[] parameters =
            {
            new SqlParameter("@AuthorityId", Authority.AuthorityId),
            new SqlParameter("@AuthorityName", Authority.AuthorityName)

};

            await _helper.ExecuteNonQueryAsync("[Master].[SP_Authority_Update]", parameters);
        }
        #endregion

        #region Delete Authority
        public async Task DeleteAuthorityAsync(int id)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@AuthorityId", id)
    };
            await _helper.ExecuteNonQueryAsync("[Master].[SP_Authority_Delete]", parameters);
        }
        #endregion
    }
}
