using Business.Entities;
using Business.Interface;
using Business.SQL;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace Business.Service
{
    public class VisitorService : IVisitorService
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public VisitorService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }

        public int AddVisitorRequestAsync(VisitorMetaData user, int LoggedInUserID, IFormFile files, string filePath)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@FirstName",user.FirstName)
                    ,new SqlParameter("@LastName",user.LastName)
                    ,new SqlParameter("@MobileNo",user.MobileNo)
                    ,new SqlParameter("@Email",user.Email)
                    ,new SqlParameter("@Address1",user.Address1)
                    ,new SqlParameter("@Address2",user.Address2)
                    ,new SqlParameter("@Address3",user.Address3)
                    ,new SqlParameter("@Area",user.Area)
                    ,new SqlParameter("@ZIPCode",user.ZipCode)
                    ,new SqlParameter("@CityID",user.CityID)
                    ,new SqlParameter("@StateID",user.StateID)
                    ,new SqlParameter("@DistrictID",user.DistrictID)
                    ,new SqlParameter("@ZIPCodeID",user.ZipCodeID)
                    ,new SqlParameter("@IdentityProofTypeID",user.IdentityProofTypeID)
                    ,new SqlParameter("@IdentityProofNumber",user.IdentityProofNumber)
                    ,new SqlParameter("@VehicleTypeID",user.VehicleTypeID)
                    ,new SqlParameter("@VehicleNo",user.VehicleNo)
                    ,new SqlParameter("@MeetingRequestDateTime",user.MeetingRequestDateTime)
                    ,new SqlParameter("@MeetingRequestTitle",user.MeetingRequestTitle)
                    ,new SqlParameter("@PurposeofMeeting",user.PurposeofMeeting)
                    ,new SqlParameter("@MeetToWhomPersonName",user.MeetToWhomPersonName)
                    ,new SqlParameter("@MeetToWhomPersonMobile",user.MeetToWhomPersonMobile)
                    ,new SqlParameter("@MeetToWhomPersonEmail",user.MeetToWhomPersonEmail)
                    ,new SqlParameter("@VisitorTypeID",user.VisitorTypeID)
                    ,new SqlParameter("@CreatedBy",LoggedInUserID)
                    ,new SqlParameter("@CreatedDate",DateTime.Now)
                    ,new SqlParameter("@SecurityOfficerName",user.SecurityOfficerName)
                    ,new SqlParameter("@SecurityOfficerMobile",user.SecurityOfficerMobile)
                    ,new SqlParameter("@FileType", files.ContentType)
                    ,new SqlParameter("@Extension", System.IO.Path.GetExtension(files.FileName))
                    ,new SqlParameter("@Name", files.FileName)
                    ,new SqlParameter("@FilePath", filePath)

                };

                var obj =  SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "Usp_I_VisitorMeetingRequest", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch
            {
                throw;
            }
        }

        public PagedDataTable<VisitorMetaData> GetAll(int pageNo = 1, int pageSize = 5, string orderBy = "MeetingRequestDateTime", string sortBy = "DESC", string searchString = "", int id = 0, int userid = 0 )
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",pageNo)
                        ,new SqlParameter("@PageSize",pageSize)
                        ,new SqlParameter("@SearchString",searchString)
                        ,new SqlParameter("@OrderBy",orderBy)
                        ,new SqlParameter("@SortBy",sortBy=="ASC"?0:1)
                        ,new SqlParameter("@ID",id)
                        ,new SqlParameter("@UserID",userid)
                        };

                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_VisitorMeetingRequest", param))
                {
                    if (ds.Tables.Count > 0)
                    {
                        table = ds.Tables[0];
                        if (table.Rows.Count > 0)
                        {
                            if (table.ContainColumn("TotalCount"))
                                totalItemCount = Convert.ToInt32(table.Rows[0]["TotalCount"]);
                            else
                                totalItemCount = table.Rows.Count;
                        }
                    }
                    PagedDataTable<VisitorMetaData> lst = table.ToPagedDataTableList<VisitorMetaData>
                        (pageNo, pageSize, totalItemCount, searchString, orderBy, sortBy);
                    return lst;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (table != null)
                    table.Dispose();
            }
        }

        public int ApproveVisitorRequest(VisitorRequestApproveMetaData model, int LoggedInUserID)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@VisitorMeetingRequestID",model.VisitorMeetingRequestID)
                    ,new SqlParameter("@IsApproved",model.IsApproved)
                    ,new SqlParameter("@MeetingDateAndTime",model.MeetingDateAndTime)
                    ,new SqlParameter("@Note",model.Note)
                    //,new SqlParameter("@Address1",model.Address1)
                    //,new SqlParameter("@Address2",model.Address2)
                    //,new SqlParameter("@Address3",model.Address3)
                    //,new SqlParameter("@Area",model.Area)
                    //,new SqlParameter("@ZIPCode",model.ZipCode)
                    //,new SqlParameter("@CityID",model.CityID)
                    //,new SqlParameter("@StateID",model.StateID)
                    //,new SqlParameter("@DistrictID",model.DistrictID)
                    //,new SqlParameter("@ZIPCodeID",model.ZipCodeID)
                    ,new SqlParameter("@CreatedBy",LoggedInUserID)                    
                };

                var obj = SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "Usp_I_VisitorMeetingRequestApproval", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch
            {
                throw;
            }
        }

        public int setVisitorStatus(VisitorMeetingStatus model, int LoggedInUserID)
        {
            

            try
                {
                SqlParameter[] param = {
                    new SqlParameter("@VisitorMeetingStatusID",model.VisitorMeetingStatusID)
                    ,new SqlParameter("@VisitorMeetingRequestID",model.VisitorMeetingRequestID)
                    ,new SqlParameter("@InTime",model.InTime.ToString("MM/dd/yyyy HH:mm:ss"))
                    ,new SqlParameter("@OutTime",model.OutTime)
                    ,new SqlParameter("@CreatedORModifiedBy",LoggedInUserID)
                    ,new SqlParameter("@OPNeeded", true)
                };

                var obj = SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, "Usp_IU_VisitorMeetingStatus", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch
            {
                throw;
            }
        }


        public async Task<VisitorRequestApproveMetaData> GetVisitorAsync(string QRCode)
        {
            VisitorRequestApproveMetaData result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@SearchString", QRCode) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_VisitorMeetingRequestByQRCode", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<VisitorRequestApproveMetaData>();
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<VisitorMeetingStatus> GetVisitorMeetingDetail(int VisitorMeetingRequestID)
        {
            VisitorMeetingStatus result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@VisitorMeetingRequestID", VisitorMeetingRequestID) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_VisitorMeetingStatusById", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<VisitorMeetingStatus>();
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertFeedback(VisitorFeedbackMetaData model)
        {
            int result = 0;
            try
            {
                SqlParameter[] param = { new SqlParameter("@VisitorMeetingRequestID", model.VisitorMeetingRequestID),
                new SqlParameter("@VisitorFeedbackID", model.VisitorFeedbackID),
                new SqlParameter("@FeedbackQuestionID", model.FeedbackQuestionID),
                new SqlParameter("@Answer", model.Answer)};
                DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_IU_FeedbackbyVisitor", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = Convert.ToInt32(dr.ItemArray[0]);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public VisitorMeetingRequestFile GetVisitorMeetingRequestFile(int VisitorMeetingRequestID)
        {
            VisitorMeetingRequestFile fileData = new VisitorMeetingRequestFile();
            try
            {
                SqlParameter[] param = { new SqlParameter("@VisitorMeetingRequestID", VisitorMeetingRequestID) };
                DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_Get_VisitorMeetingRequestFile", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            fileData = dr.ToPagedDataTableList<VisitorMeetingRequestFile>();
                        }
                    }
                }
                return fileData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DashbaordCount> GetDashboardCounts(int userid)
        {
            DashbaordCount result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@UserID", userid) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_GetVisitorDashboardCount", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<DashbaordCount>();
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

      
    }
}
