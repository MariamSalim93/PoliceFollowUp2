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
    public class DeviceNoRepo
    {
        private readonly SqlHelper _helper = new SqlHelper();

        #region Get List Of DeviceNo
        public async Task<List<DeviceNoDTO>> GetListDeviceNoAsync()
        {
            DataTable dt = await _helper.GetDataTableAsync("[Master].[SP_DeviceNo_GetList]");
            var allDeviceNoList = new List<DeviceNoDTO>();

            foreach (DataRow dr in dt.Rows)
            {
                var DeviceNo = new DeviceNoDTO
                {
                    DeviceNoId = int.Parse(dr["DeviceNoId"].ToString()),
                    DeviceNoName = dr["DeviceNoName"].ToString()
                };

                allDeviceNoList.Add(DeviceNo);
            }

            return allDeviceNoList;
        }
        #endregion

        #region Add DeviceNo
        public async Task AddDeviceNoAsync(DeviceNoDTO DeviceNo)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@DeviceNoName", DeviceNo.DeviceNoName)
    };
            await _helper.ExecuteNonQueryAsync("[Master].[SP_DeviceNo_Add]", parameters);
        }
        #endregion

        #region Get DeviceNo By Id
        public async Task<DeviceNoDTO> GetDeviceNoByIdAsync(int id)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@DeviceNoId", id)
    };
            DataTable dt = await _helper.GetDataTableAsync("[Master].[SP_DeviceNo_GetById]", parameters);
            DeviceNoDTO DeviceNo = null;

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                DeviceNo = new DeviceNoDTO
                {
                    DeviceNoId = int.Parse(dr["DeviceNoId"].ToString()),
                    DeviceNoName = dr["DeviceNoName"].ToString()
                };
            }

            return DeviceNo;
        }
        #endregion

        #region Update DeviceNo
        public async Task UpdateDeviceNoAsync(DeviceNoDTO DeviceNo)
        {
            IDbDataParameter[] parameters =
            {
            new SqlParameter("@DeviceNoId", DeviceNo.DeviceNoId),
            new SqlParameter("@DeviceNoName", DeviceNo.DeviceNoName)

};

            await _helper.ExecuteNonQueryAsync("[Master].[SP_DeviceNo_Update]", parameters);
        }
        #endregion

        #region Delete DeviceNo
        public async Task DeleteDeviceNoAsync(int id)
        {
            IDbDataParameter[] parameters =
            {
        new SqlParameter("@DeviceNoId", id)
    };
            await _helper.ExecuteNonQueryAsync("[Master].[SP_DeviceNo_Delete]", parameters);
        }
        #endregion
    }
}
