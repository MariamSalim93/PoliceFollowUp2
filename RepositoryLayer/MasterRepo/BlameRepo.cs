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
    public class BlameRepo
    {
        private readonly SqlHelper _helper = new SqlHelper();

        #region Get List Of Blame
        public async Task<List<BlameDTO>> GetListBlameAsync()
        {
            DataTable dt = await _helper.GetDataTableAsync("[Master].[SP_Blame_GetList]");
            var allBlameList = new List<BlameDTO>();

            foreach (DataRow dr in dt.Rows)
            {
                var Blame = new BlameDTO
                {
                    BlameId = int.Parse(dr["BlameId"].ToString()),
                    BlameName = dr["BlameName"].ToString()
                };

                allBlameList.Add(Blame);
            }

            return allBlameList;
        }
        #endregion

        #region Add Blame
        public async Task AddBlameAsync(BlameDTO Blame)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@BlameName", Blame.BlameName)
    };
            await _helper.ExecuteNonQueryAsync("[Master].[SP_Blame_Add]", parameters);
        }
        #endregion

        #region Get Blame By Id
        public async Task<BlameDTO> GetBlameByIdAsync(int id)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@BlameId", id)
    };
            DataTable dt = await _helper.GetDataTableAsync("[Master].[SP_Blame_GetById]", parameters);
            BlameDTO Blame = null;

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                Blame = new BlameDTO
                {
                    BlameId = int.Parse(dr["BlameId"].ToString()),
                    BlameName = dr["BlameName"].ToString()
                };
            }

            return Blame;
        }
        #endregion

        #region Update Blame
        public async Task UpdateBlameAsync(BlameDTO Blame)
        {
            IDbDataParameter[] parameters =
            {
            new SqlParameter("@BlameId", Blame.BlameId),
            new SqlParameter("@BlameName", Blame.BlameName)

};

            await _helper.ExecuteNonQueryAsync("[Master].[SP_Blame_Update]", parameters);
        }
        #endregion

        #region Delete Blame
        public async Task DeleteBlameAsync(int id)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@BlameId", id)
    };
            await _helper.ExecuteNonQueryAsync("[Master].[SP_Blame_Delete]", parameters);
        }
        #endregion
    }
}
