using Business.Entities;
using Business.Entities.Dynamic;
using Business.Interface.Dynamic;
using Business.SQL;
using Dapper;
using MailKit.Search;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service.Dynamic
{
    public class MasterEntity : IMasterEntity
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public MasterEntity(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }
        public async Task<PagedDataTable<MasterEntityMetadata>> GetDistinctNameList(int compnayID, int pageNo = 1, int pageSize = 20, string searchString = "", string orderBy = "Name", string sortBy = "ASC")
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<MasterEntityMetadata> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@CompanyID",compnayID)
                        ,new SqlParameter("@PageNo",pageNo)
                        ,new SqlParameter("@PageSize",pageSize)
                        ,new SqlParameter("@SearchString",searchString)
                        ,new SqlParameter("@OrderBy",orderBy)
                        ,new SqlParameter("@SortBy",sortBy=="ASC"?0:1)

                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "usp_GetAll_Compnay_MasterList", param))
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
                    lst = table.ToPagedDataTableList<MasterEntityMetadata>(pageNo, pageSize, totalItemCount);
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

        public int Save(int ID, string name, string Value, int SortOrder, bool IsActive, int UserId, short EntryTypeID, int CompanyID, string Code = null, bool IsDefaultSelected = false)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", ID);
                param.Add("@Value", Value);
                param.Add("@SortOrder", SortOrder);
                param.Add("@IsActive", IsActive);
                param.Add("@CreatedBy", UserId);
                param.Add("@For", name);
                param.Add("@EntryTypeID", EntryTypeID);
                param.Add("@CompanyID", CompanyID);
                param.Add("@IsDefaultSelected", IsDefaultSelected);

                return QueryHelper.Save(connection, "SaveMasterList", param);

            }
            catch
            {
                throw;
            }
        }
        public List<MasterEntityMetadata> GetList(int CompanyID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyID", CompanyID);
                return QueryHelper.GetList<MasterEntityMetadata>(connection, "GetMasterList", param);
            }
            catch 
            {
                throw;
            }
        }

        public MasterEntityMetadata GetDetail(int id, int CompanyID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", id);
                param.Add("@CompanyID", CompanyID);
                return QueryHelper.GetTableDetail<MasterEntityMetadata>(connection, "Usp_Get_MasterList", param);
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
                return QueryHelper.Delete(connection, "DeleteMasterList", param);
            }
            catch
            {
                throw;
            }
        }

        public bool Delete(string name)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Name", name);
                return QueryHelper.Delete(connection, "DeleteMasterListByName", param);
            }
            catch
            {
                throw;
            }
        }

        public List<MasterEntityMetadata> GetListByName(string Name, int CompanyID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@For", Name);
                param.Add("@CompanyID", CompanyID);
                return QueryHelper.GetList<MasterEntityMetadata>(connection, "GetMasterListByName", param);
            }
            catch
            {
                throw;
            }
        }
        public List<MasterEntityMetadata> GetDistinctNameList(int CompanyID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyID", CompanyID);
                return QueryHelper.GetList<MasterEntityMetadata>(connection, "GetDistinctNameMasterList", param);
            }
            catch
            {
                throw;
            }
        }

        public List<MasterEntityEntryTypeMetadata> GetMasterEntryTypeList()
        {
            try
            {
                return QueryHelper.GetList<MasterEntityEntryTypeMetadata>(connection, "GetMasterListEntryTypeList", null);
            }
            catch
            {
                throw;
            }
        }

        public List<DropdownMetadata> GetDropdownKeys(int CompanyID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyID", CompanyID);
                var list = QueryHelper.GetList<DropdownMetadata>(connection, "GetDropdownKeys", param);
                foreach (var item in list)
                {
                    item.Text = item.Value;
                }
                return list;
            }
            catch
            {
                throw;
            }
        }

        public List<DropdownMetadata> GetDropdownValueList(string Key, int CompanyID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Key", Key);
                param.Add("@CompanyID", CompanyID);
                var list = QueryHelper.GetList<DropdownMetadata>(connection, "GetDropdownList", param);
                return list;
            }
            catch
            {
                throw;
            }
        }

        public MasterEntityMetadata GetDetail(string Code, int CompanyID)
        {
            throw new NotImplementedException();
        }


    }
}
