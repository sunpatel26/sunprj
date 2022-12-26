using Business.Entities.Dynamic;
using Business.Interface.Dynamic;
using Business.SQL;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service.Dynamic
{
    public class RequestTypeService : IRequestType
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public RequestTypeService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }
        public async Task<PagedDataTable<RequestTypeMetadata>> GetAllList(int compnayID, int page = 1, int pagesize = 20, string search = "", string orderby = "Name", string sortby = "ASC")
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<RequestTypeMetadata> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@CompanyID",compnayID)
                        ,new SqlParameter("@PageNo",page)
                        ,new SqlParameter("@PageSize",pagesize)
                        ,new SqlParameter("@SearchString",search)
                        ,new SqlParameter("@OrderBy",orderby)
                        ,new SqlParameter("@SortBy",sortby=="ASC"?0:1)

                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_RequestType_ByCompnay", param))
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
                    lst = table.ToPagedDataTableList<RequestTypeMetadata>(page, pagesize, totalItemCount);
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

        public int Save(int ID, int EntityID, string Name, string AccessByRoles, string AssignedTo, bool IsActive, int UserId, int CompanyID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", ID);
                param.Add("@EntityID", EntityID);
                param.Add("@Name", Name);
                param.Add("@AccessByRoles", AccessByRoles);
                param.Add("@AssignedTo", AssignedTo);
                param.Add("@IsActive", IsActive);
                param.Add("@CreatedBy", UserId);
                param.Add("@CompanyID", CompanyID);
                return QueryHelper.Save(connection,"SaveRequestType", param);
            }
            catch 
            {
                throw ;
            }
        }
        public List<RequestTypeMetadata> GetList(int CompanyID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyID", CompanyID);
                return QueryHelper.GetList<RequestTypeMetadata>(connection, "GetRequestType", param);
            }
            catch 
            {
                throw ;
            }
        }

        public RequestTypeMetadata GetDetail(int id, int CompanyID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", id);
                param.Add("@CompanyID", CompanyID);
                return QueryHelper.GetTableDetail<RequestTypeMetadata>(connection, "Usp_Get_RequestType", param);
            }
            catch 
            {
                throw ;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", id);
                return QueryHelper.Delete(connection, "DeleteRequestType", param);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("DELETE statement conflicted with the REFERENCE constraint"))
                {
                    return false;
                }
                throw ;
            }
        }

        public List<DropdownMetadata> GetDropdownList(int EntityID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@EntityID", EntityID);
                return QueryHelper.GetList<DropdownMetadata>(connection, "GetRequestTypeByEntity", param);
            }
            catch 
            {
                throw ;
            }
        }
    }
}
