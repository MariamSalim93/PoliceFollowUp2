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
    public class SupervisorRepo
    {
        private readonly SqlHelper _helper = new SqlHelper();

        #region Get List Of Supervisor
        public async Task<List<SupervisorDTO>> GetListSupervisorAsync()
        {
            DataTable dt = await _helper.GetDataTableAsync("[Master].[SP_Supervisor_GetList]");
            var allSupervisorList = new List<SupervisorDTO>();

            foreach (DataRow dr in dt.Rows)
            {
                var Supervisor = new SupervisorDTO
                {
                    SupervisorId = int.Parse(dr["SupervisorId"].ToString()),
                    SupervisorName = dr["SupervisorName"].ToString()
                };

                allSupervisorList.Add(Supervisor);
            }

            return allSupervisorList;
        }
        #endregion

        #region Add Supervisor
        public async Task AddSupervisorAsync(SupervisorDTO Supervisor)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@SupervisorName", Supervisor.SupervisorName)
    };
            await _helper.ExecuteNonQueryAsync("[Master].[SP_Supervisor_Add]", parameters);
        }
        #endregion

        #region Get Supervisor By Id
        public async Task<SupervisorDTO> GetSupervisorByIdAsync(int id)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@SupervisorId", id)
    };
            DataTable dt = await _helper.GetDataTableAsync("[Master].[SP_Supervisor_GetById]", parameters);
            SupervisorDTO Supervisor = null;

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                Supervisor = new SupervisorDTO
                {
                    SupervisorId = int.Parse(dr["SupervisorId"].ToString()),
                    SupervisorName = dr["SupervisorName"].ToString()
                };
            }

            return Supervisor;
        }
        #endregion

        #region Update Supervisor
        public async Task UpdateSupervisorAsync(SupervisorDTO Supervisor)
        {
            IDbDataParameter[] parameters =
            {
            new SqlParameter("@SupervisorId", Supervisor.SupervisorId),
            new SqlParameter("@SupervisorName", Supervisor.SupervisorName)

};

            await _helper.ExecuteNonQueryAsync("[Master].[SP_Supervisor_Update]", parameters);
        }
        #endregion

        #region Delete Supervisor
        public async Task DeleteSupervisorAsync(int id)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@SupervisorId", id)
    };
            await _helper.ExecuteNonQueryAsync("[Master].[SP_Supervisor_Delete]", parameters);
        }
        #endregion
    }
}
