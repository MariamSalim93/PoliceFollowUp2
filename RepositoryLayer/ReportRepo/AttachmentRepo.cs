using DAL;
using SharedLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.ReportRepo
{
    public class AttachmentRepo
    {
        #region Connections
        private readonly SqlHelper _helper = new SqlHelper();
        #endregion

        #region Get List Of Attachment
        public async Task<List<AttachmentDTO>> GetListOfAttachmentAsync()
        {
            DataTable dt = await _helper.GetDataTableAsync("[dbo].[SP_Attachment_GetList]");
            var allAttachmentList = new List<AttachmentDTO>();

            foreach (DataRow dr in dt.Rows)
            {
                var attachment = new AttachmentDTO
                {
                    AttachmentID = Int32.Parse(dr["AttachmentID"].ToString()),
                    FileNo = Int32.Parse(dr["FileNo"].ToString()),
                    AttachmentName = dr["AttachmentName"].ToString(),            
                    Attachments = dr["Attachments"].ToString(),
                    CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString()),
                    CreatedBy = dr["CreatedBy"].ToString(),
                };

                allAttachmentList.Add(attachment);
            }

            return allAttachmentList;
        }
        #endregion

        #region Add Attachment
        public async Task AddAttachmentAsync(AttachmentDTO attachment)
        {
            IDbDataParameter[] parameters =
            {
                new SqlParameter("@FileNo", attachment.FileNo),
                new SqlParameter("@AttachmentName", attachment.AttachmentName),
                new SqlParameter("@Attachments", attachment.Attachments),       
                new SqlParameter("@CreatedBy", attachment.CreatedBy),
                new SqlParameter("@CreatedDate", attachment.CreatedDate),
                new SqlParameter("@EditedBy", attachment.EditedBy),
                new SqlParameter("@EditDate", (object)attachment.EditDate ?? DBNull.Value),
                new SqlParameter("@EditNote", attachment.EditNote),
                new SqlParameter("@Status", attachment.Status),
                new SqlParameter("@ApprovedBy", attachment.ApprovedBy),
                new SqlParameter("@ApproveDate", (object)attachment.ApproveDate ?? DBNull.Value),
                new SqlParameter("@ApproveNotes", attachment.ApproveNotes),
            };

            await _helper.ExecuteNonQueryAsync("[dbo].[SP_Attachment_Add]", parameters);
        }
        #endregion

        #region Get Attachment By Id
        public async Task<AttachmentDTO> GetAttachmentByIdAsync(int attachmentID)
        {
            IDbDataParameter[] parameters =
            {
                new SqlParameter("@AttachmentID", attachmentID)
            };
            DataTable dt = await _helper.GetDataTableAsync("[dbo].[SP_Attachment_GetByID]", parameters);
            AttachmentDTO attachment = null;

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                attachment = new AttachmentDTO
                {
                    AttachmentID = Int32.Parse(dr["AttachmentID"].ToString()),
                    FileNo = Int32.Parse(dr["FileNo"].ToString()),
                    AttachmentName = dr["AttachmentName"].ToString(),
                    Attachments = dr["Attachments"].ToString(),
                    CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString()),
                    CreatedBy = dr["CreatedBy"].ToString(),
                    EditDate = dr["EditDate"] != DBNull.Value ? Convert.ToDateTime(dr["EditDate"]) : (DateTime?)null,
                    EditedBy = dr["EditedBy"].ToString(),
                    EditNote = dr["EditNote"].ToString(),
                    Status = dr["Status"].ToString(),
                    ApprovedBy = dr["ApprovedBy"].ToString(),
                    ApproveDate = dr["ApproveDate"] != DBNull.Value ? Convert.ToDateTime(dr["ApproveDate"]) : (DateTime?)null,

                };
            }

            return attachment;
        }
        #endregion

        #region Update Attachment
        public async Task UpdateAttachmentAsync(AttachmentDTO attachment)
        {
            IDbDataParameter[] parameters =
            {
                new SqlParameter("@AttachmentID", attachment.AttachmentID),
                new SqlParameter("@FileNo", attachment.FileNo),
                new SqlParameter("@AttachmentName", attachment.AttachmentName),
                new SqlParameter("@Attachments", attachment.Attachments),
                new SqlParameter("@CreatedBy", attachment.CreatedBy),
                new SqlParameter("@CreatedDate", attachment.CreatedDate),
                new SqlParameter("@EditedBy", attachment.EditedBy),
                new SqlParameter("@EditDate", (object)attachment.EditDate ?? DBNull.Value),
                new SqlParameter("@EditNote", attachment.EditNote),
                new SqlParameter("@Status", attachment.Status),
                new SqlParameter("@ApprovedBy", attachment.ApprovedBy),
                new SqlParameter("@ApproveDate", (object)attachment.ApproveDate ?? DBNull.Value),
                new SqlParameter("@ApproveNotes", attachment.ApproveNotes),
            };

            await _helper.ExecuteNonQueryAsync("[dbo].[SP_Attachment_Update]", parameters);
        }
        #endregion

    }
}
