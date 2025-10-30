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
    public class MonitoringAreaRepo
    {
        private readonly SqlHelper _helper = new SqlHelper();

        #region Get List Of MonitoringArea
        public async Task<List<MonitoringAreaDTO>> GetListMonitoringAreaAsync()
        {
            DataTable dt = await _helper.GetDataTableAsync("[Master].[SP_MonitoringArea_GetList]");
            var allMonitoringAreaList = new List<MonitoringAreaDTO>();

            foreach (DataRow dr in dt.Rows)
            {
                var MonitoringArea = new MonitoringAreaDTO
                {
                    MonitoringAreaId = int.Parse(dr["MonitoringAreaId"].ToString()),
                    MonitoringAreaName = dr["MonitoringAreaName"].ToString()
                };

                allMonitoringAreaList.Add(MonitoringArea);
            }

            return allMonitoringAreaList;
        }
        #endregion

        #region Add MonitoringArea
        public async Task AddMonitoringAreaAsync(MonitoringAreaDTO MonitoringArea)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@MonitoringAreaName", MonitoringArea.MonitoringAreaName)
    };
            await _helper.ExecuteNonQueryAsync("[Master].[SP_MonitoringArea_Add]", parameters);
        }
        #endregion

        #region Get MonitoringArea By Id
        public async Task<MonitoringAreaDTO> GetMonitoringAreaByIdAsync(int id)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@MonitoringAreaId", id)
    };
            DataTable dt = await _helper.GetDataTableAsync("[Master].[SP_MonitoringArea_GetById]", parameters);
            MonitoringAreaDTO MonitoringArea = null;

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                MonitoringArea = new MonitoringAreaDTO
                {
                    MonitoringAreaId = int.Parse(dr["MonitoringAreaId"].ToString()),
                    MonitoringAreaName = dr["MonitoringAreaName"].ToString()
                };
            }

            return MonitoringArea;
        }
        #endregion

        #region Update MonitoringArea
        public async Task UpdateMonitoringAreaAsync(MonitoringAreaDTO MonitoringArea)
        {
            IDbDataParameter[] parameters =
            {
            new SqlParameter("@MonitoringAreaId", MonitoringArea.MonitoringAreaId),
            new SqlParameter("@MonitoringAreaName", MonitoringArea.MonitoringAreaName)

};

            await _helper.ExecuteNonQueryAsync("[Master].[SP_MonitoringArea_Update]", parameters);
        }
        #endregion

        #region Delete MonitoringArea
        public async Task DeleteMonitoringAreaAsync(int id)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@MonitoringAreaId", id)
    };
            await _helper.ExecuteNonQueryAsync("[Master].[SP_MonitoringArea_Delete]", parameters);
        }
        #endregion
    }
}
