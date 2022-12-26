using Business.Entities;
using Business.Entities.Dynamic;
using Business.Interface.Dynamic;
using Business.SQL;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Data;

namespace Business.Service.Dynamic
{
    public class RequestTypeControlService : IRequestTypeControl
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public RequestTypeControlService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }

        public int Save(int RequestTypeControlID, int RequestTypeTitleID, string Label, string Type, bool IsRequired, string DefaultValue, string DataKey, bool IsActive, string AccessByRoles, short? ControlValidationRuleID, int CreatedBy, string Tooltip, short? MinLength, short? MaxLength, short? SortOrder)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", RequestTypeControlID);
                param.Add("@RequestTypeTitleID", RequestTypeTitleID);
                param.Add("@Label", Label);
                param.Add("@Type", Type);
                param.Add("@IsActive", IsActive);
                param.Add("@IsRequired", IsRequired);
                param.Add("@DefaultValue", DefaultValue);
                param.Add("@DataKey", DataKey);
                param.Add("@IsActive", IsActive);
                param.Add("@AccessByRoles", AccessByRoles);
                param.Add("@ControlValidationRuleID", ControlValidationRuleID);
                param.Add("@CreatedBy", CreatedBy);
                param.Add("@Tooltip", Tooltip);
                param.Add("@MinLength", MinLength);
                param.Add("@MaxLength", MaxLength);
                param.Add("@SortOrder", SortOrder);

                return QueryHelper.Save(connection, "SaveRequestTypeControl", param);
            }
            catch
            {
                throw;
            }
        }
        public List<RequestTypeControlMetadata> GetList(int CompanyID)
        {
            return GetList(null, CompanyID);
        }

        public PagedDataTable<RequestTypeControlMetadata> GetList(int? requestTypeTitleID, int CompanyID, int pageNo = 1, int pageSize = 0, string orderBy = "RequestTypeTitle", string sortBy = "ASC", string searchString = "")
        {

            DataTable table = new DataTable();
            int totalItemCount = 0;
            try
            {
                SqlParameter[] param = {
                         new SqlParameter("@CompanyID",CompanyID)
                        ,new SqlParameter("@RequestTypeTitleID", requestTypeTitleID)
                        ,new SqlParameter("@PageNo",pageNo)
                        ,new SqlParameter("@PageSize",pageSize)
                        ,new SqlParameter("@SearchString",searchString)
                        ,new SqlParameter("@OrderBy",orderBy)
                        ,new SqlParameter("@SortBy",sortBy=="ASC"?0:1)
                        };

                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "GetRequestTypeControlList", param))
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
                    PagedDataTable<RequestTypeControlMetadata> lst = table.ToPagedDataTableList<RequestTypeControlMetadata>
                        (pageNo, pageSize, totalItemCount, searchString, orderBy, sortBy);
                    return lst;
                }
            }
            catch
            {
                throw;
            }
        }
        public RequestTypeControlMetadata GetDetail(int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", id);
                return QueryHelper.GetTableDetail<RequestTypeControlMetadata>(connection, "GetRequestTypeControl", param);
            }
            catch
            {
                throw;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", id);
                return QueryHelper.Delete(connection, "DeleteRequestTypeControl", param);
            }
            catch
            {
                throw;
            }
        }

        public List<RequestTypeControlMetadata> GetListByRequestType(int? requestTypeID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@RequestTypeID", requestTypeID);
                return QueryHelper.GetList<RequestTypeControlMetadata>(connection, "GetRequestTypeControlListByRequestType", param);
            }
            catch
            {
                throw;
            }
        }

        public List<ControlValidationRuleMetadata> GetValidationRules()
        {
            try
            {
                return QueryHelper.GetList<ControlValidationRuleMetadata>(connection, "GetControlValidationRules");
            }
            catch
            {
                throw;
            }
        }

        public List<DropdownMetadata> GetControlTypes()
        {
            try
            {
                var list = new List<DropdownMetadata>();
                list.Add(new DropdownMetadata() { Text = ConstVariable.TYPE_BOOLEAN, Value = ConstVariable.TYPE_BOOLEAN });
                list.Add(new DropdownMetadata() { Text = ConstVariable.TYPE_DATETIME, Value = ConstVariable.TYPE_DATETIME });
                list.Add(new DropdownMetadata() { Text = ConstVariable.TYPE_TIME, Value = ConstVariable.TYPE_TIME });
                list.Add(new DropdownMetadata() { Text = ConstVariable.TYPE_DIVIDER, Value = ConstVariable.TYPE_DIVIDER });
                list.Add(new DropdownMetadata() { Text = ConstVariable.TYPE_DROPDOWN, Value = ConstVariable.TYPE_DROPDOWN });
                list.Add(new DropdownMetadata() { Text = ConstVariable.TYPE_DROPDOWN_MULTISELECT, Value = ConstVariable.TYPE_DROPDOWN_MULTISELECT });
                list.Add(new DropdownMetadata() { Text = ConstVariable.TYPE_FILEUPLOAD, Value = ConstVariable.TYPE_FILEUPLOAD });
                list.Add(new DropdownMetadata() { Text = ConstVariable.TYPE_LABLE, Value = ConstVariable.TYPE_LABLE });
                list.Add(new DropdownMetadata() { Text = ConstVariable.TYPE_RADIOLIST, Value = ConstVariable.TYPE_RADIOLIST });
                list.Add(new DropdownMetadata() { Text = ConstVariable.TYPE_TEXT, Value = ConstVariable.TYPE_TEXT });
                list.Add(new DropdownMetadata() { Text = ConstVariable.TYPE_TEXT_MULTILINE, Value = ConstVariable.TYPE_TEXT_MULTILINE });
                return list;
            }
            catch
            {
                throw;
            }
        }
    }
}
