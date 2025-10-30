using DAL;
using SharedLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.ReportRepo
{
    public class ConvictFileRepo
    {
        #region Connections
        private readonly SqlHelper _helper = new SqlHelper();
        #endregion

        #region Get List Of ConvictFile
        public async Task<List<ConvictFileDTO>> GetListOfConvictFileAsync()
        {
            DataTable dt = await _helper.GetDataTableAsync("[dbo].[SP_ConvictFile_GetList]");
            var allConvictFileList = new List<ConvictFileDTO>();

            foreach (DataRow dr in dt.Rows)
            {
                var convictFile = new ConvictFileDTO
                {
                    ReportID = Int32.Parse(dr["ReportID"].ToString()),
                    Name = dr["Name"].ToString(),
                    Gender = dr["Gender"].ToString(),
                    Nationality = dr["Nationality"].ToString(),
                    UnifiedNo = dr["UnifiedNo"].ToString(),
                    CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString()),
                    CreatedBy = dr["CreatedBy"].ToString(),

                };

                allConvictFileList.Add(convictFile);
            }

            return allConvictFileList;
        }
        #endregion

        #region Add ConvictFile
        public async Task AddConvictFileAsync(ConvictFileDTO convictFile)
        {
            IDbDataParameter[] parameters =
            {
                new SqlParameter("@Name", convictFile.Name),
                new SqlParameter("@FakeName", convictFile.FakeName?? (object)DBNull.Value),
                new SqlParameter("@Gender", convictFile.Gender),
                new SqlParameter("@Nationality", convictFile.Nationality),
                new SqlParameter("@BirthDate", convictFile.BirthDate),
                new SqlParameter("@Age", convictFile.Age?? (object)DBNull.Value),
                new SqlParameter("@EducationalLevel", convictFile.EducationalLevel ??(object) DBNull.Value),
                new SqlParameter("@Disability", convictFile.Disability?? (object)DBNull.Value),
                new SqlParameter("@MaritalStatus", convictFile.MaritalStatus),
                new SqlParameter("@Profession", convictFile.Profession),
                new SqlParameter("@Residence", convictFile.Residence),
                new SqlParameter("@UnifiedNo", convictFile.UnifiedNo),
                new SqlParameter("@EmiratesID", convictFile.EmiratesID?? (object)DBNull.Value),
                new SqlParameter("@Phone", convictFile.Phone),
                new SqlParameter("@Notes", convictFile.Notes?? (object)DBNull.Value),
                new SqlParameter("@Attachments", convictFile.Attachments?? (object)DBNull.Value),
                new SqlParameter("@CreatedBy", convictFile.CreatedBy),
                new SqlParameter("@CreatedDate", convictFile.CreatedDate),
                new SqlParameter("@EditedBy", convictFile.EditedBy),
                new SqlParameter("@EditDate", convictFile.EditDate ?? (object)DBNull.Value),
                new SqlParameter("@EditNote", convictFile.EditNote),
                new SqlParameter("@Status", convictFile.Status),
                new SqlParameter("@ApprovedBy", convictFile.ApprovedBy),
                new SqlParameter("@ApproveDate", convictFile.ApproveDate ?? (object)DBNull.Value),
                new SqlParameter("@ApproveNotes", convictFile.ApproveNotes),
            };

            await _helper.ExecuteNonQueryAsync("[dbo].[SP_ConvictFile_Add]", parameters);
        }
        #endregion

        #region Get ConvictFile By Id
        public async Task<ConvictFileDTO> GetConvictFileByIdAsync(int id)
        {
            IDbDataParameter[] parameters =
            {
                new SqlParameter("@ReportID", id)
            };
            DataTable dt = await _helper.GetDataTableAsync("[dbo].[SP_ConvictFile_GetByID]", parameters);
            ConvictFileDTO convictFile = null;

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                convictFile = new ConvictFileDTO
                {

                    ReportID = Convert.ToInt32(dr["ReportID"]),
                    Name = Convert.ToString(dr["Name"]),
                    FakeName = dr["FakeName"] != DBNull.Value ? Convert.ToString(dr["FakeName"]) : null,
                    Gender = Convert.ToString(dr["Gender"]),
                    Nationality = Convert.ToString(dr["Nationality"]),
                    BirthDate = Convert.ToDateTime(dr["BirthDate"]),
                    Age = dr["Age"] != DBNull.Value ? (int?)Convert.ToInt32(dr["Age"]) : null,
                    Disability = dr["Disability"] != DBNull.Value ? Convert.ToString(dr["Disability"]) : null,
                    EducationalLevel = dr["EducationalLevel"] != DBNull.Value ? Convert.ToString(dr["EducationalLevel"]) : null,
                    MaritalStatus = Convert.ToString(dr["MaritalStatus"]),
                    Profession = Convert.ToString(dr["Profession"]),
                    Residence = Convert.ToString(dr["Residence"]),
                    UnifiedNo = Convert.ToString(dr["UnifiedNo"]),
                    EmiratesID = dr["EmiratesID"] != DBNull.Value ? Convert.ToString(dr["EmiratesID"]) : null,
                    Phone = Convert.ToString(dr["Phone"]),
                    Notes = dr["Notes"] != DBNull.Value ? Convert.ToString(dr["Notes"]) : null,
                    Attachments = dr["Attachments"] != DBNull.Value ? Convert.ToString(dr["Attachments"]) : null,
                    CreatedBy = dr["CreatedBy"] != DBNull.Value ? Convert.ToString(dr["CreatedBy"]) : null,
                    CreatedDate = dr["CreatedDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(dr["CreatedDate"]) : null,
                    EditedBy = dr["EditedBy"] != DBNull.Value ? Convert.ToString(dr["EditedBy"]) : null,
                    EditDate = dr["EditDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(dr["EditDate"]) : null,
                    EditNote = dr["EditNote"] != DBNull.Value ? Convert.ToString(dr["EditNote"]) : null,
                    Status = dr["Status"] != DBNull.Value ? Convert.ToString(dr["Status"]) : null,
                    ApprovedBy = dr["ApprovedBy"] != DBNull.Value ? Convert.ToString(dr["ApprovedBy"]) : null,
                    ApproveDate = dr["ApproveDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(dr["ApproveDate"]) : null,
                    ApproveNotes = dr["ApproveNotes"] != DBNull.Value ? Convert.ToString(dr["ApproveNotes"]) : null

                };
            }

            return convictFile;
        }
        #endregion

        #region Update convictFilePledge
        public async Task UpdateConvictFileAsync(ConvictFileDTO convictFile)
        {
            IDbDataParameter[] parameters =
            {
                new SqlParameter("@ReportID", convictFile.ReportID),
                new SqlParameter("@Name", convictFile.Name),
                new SqlParameter("@FakeName", convictFile.FakeName?? (object)DBNull.Value),
                new SqlParameter("@Gender", convictFile.Gender),
                new SqlParameter("@Nationality", convictFile.Nationality),
                new SqlParameter("@BirthDate", convictFile.BirthDate),
                new SqlParameter("@Age", convictFile.Age?? (object)DBNull.Value),
                new SqlParameter("@EducationalLevel", convictFile.EducationalLevel ??(object) DBNull.Value),
                new SqlParameter("@Disability", convictFile.Disability?? (object)DBNull.Value),
                new SqlParameter("@MaritalStatus", convictFile.MaritalStatus),
                new SqlParameter("@Profession", convictFile.Profession),
                new SqlParameter("@Residence", convictFile.Residence),
                new SqlParameter("@UnifiedNo", convictFile.UnifiedNo),
                new SqlParameter("@EmiratesID", convictFile.EmiratesID?? (object)DBNull.Value),
                new SqlParameter("@Phone", convictFile.Phone),
                new SqlParameter("@Notes", convictFile.Notes?? (object)DBNull.Value),
                new SqlParameter("@Attachments", convictFile.Attachments?? (object)DBNull.Value),
                new SqlParameter("@CreatedBy", convictFile.CreatedBy),
                new SqlParameter("@CreatedDate", convictFile.CreatedDate),
                new SqlParameter("@EditedBy", convictFile.EditedBy),
                new SqlParameter("@EditDate", convictFile.EditDate ?? (object)DBNull.Value),
                new SqlParameter("@EditNote", convictFile.EditNote),
                new SqlParameter("@Status", convictFile.Status),
                new SqlParameter("@ApprovedBy", convictFile.ApprovedBy),
                new SqlParameter("@ApproveDate", convictFile.ApproveDate ?? (object)DBNull.Value),
                new SqlParameter("@ApproveNotes", convictFile.ApproveNotes),
            };

            await _helper.ExecuteNonQueryAsync("[dbo].[SP_ConvictFile_Update]", parameters);
        }
        #endregion


    }
}
