using Business.Entities.Marketing.VisitingDetail;
using Business.Interface.Marketing.IVisitingDetailService;
using Business.SQL;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service.Marketing.VisitingDetailService
{
    public class MarketingVisitingDetailService : IMarketingVisitingDetailService
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public MarketingVisitingDetailService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }

        public async Task<PagedDataTable<VisitingDetail>> GetAllMarketingVisitingDetailAsync(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "MarketingVisitingDetailID", string sortBy = "ASC")
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
                        };

                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_MarketingVisitingDetail", param))
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
                    PagedDataTable<VisitingDetail> lst = table.ToPagedDataTableList<VisitingDetail>
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

        public async Task<int> MarketingVisitingDetailInsertOrUpdateAsync(VisitingDetail visitingDetail)
        {
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@MarketingVisitedDetailID", visitingDetail.MarketingVisitedDetailID)
                ,new SqlParameter("@VisitedByPerson", visitingDetail.VisitedByPerson)
                ,new SqlParameter("@VisitedTo", visitingDetail.VisitedTo)
                ,new SqlParameter("@Email", visitingDetail.Email)
                ,new SqlParameter("@MobileNo", visitingDetail.MobileNo)
                ,new SqlParameter("@VanueTypeID", visitingDetail.VanueTypeID)
                ,new SqlParameter("@PartyTypeID", visitingDetail.PartyTypeID)
                ,new SqlParameter("@Datetime", visitingDetail.DateTime)
                ,new SqlParameter("@CompanyOrOrganazationName", visitingDetail.CompanyOrOrganazationName)
                ,new SqlParameter("@IsCollectVisitingCard", visitingDetail.IsCollectVisitingCard)
                ,new SqlParameter("@IsCollectMarketingDocs", visitingDetail.IsCollectMarketingDocs)
                ,new SqlParameter("@PlaceOfMeeting", visitingDetail.PlaceOfMeeting)
                ,new SqlParameter("@IsSentDocument", visitingDetail.IsSentDocument)
                ,new SqlParameter("@IsSentMarketingDocs", visitingDetail.IsSentMarketingDocs)
                ,new SqlParameter("@ReferenceBetterBusiness", visitingDetail.ReferenceBetterBusiness)
                ,new SqlParameter("@ReferenceMobileOrEmail", visitingDetail.ReferenceMobileOrEmail)
                ,new SqlParameter("@MeetingTotalTime", visitingDetail.MeetingTotalTime)                
                ,new SqlParameter("@MOM", visitingDetail.MOM)
                ,new SqlParameter("@Feedback", visitingDetail.Feedback)
                ,new SqlParameter("@CreatedOrModifiedBy", visitingDetail.CreatedOrModifiedBy)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_MarketingVisitingDetail", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<VisitingDetail> GetMarketingVisitingDetailAsync(int MarketingVisitedDetailID)
        {
            VisitingDetail result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@MarketingVisitedDetailID", MarketingVisitedDetailID) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_MarketingVisitingDetail", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<VisitingDetail>();
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
