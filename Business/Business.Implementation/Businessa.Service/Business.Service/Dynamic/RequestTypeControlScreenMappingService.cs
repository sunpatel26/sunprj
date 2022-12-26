using Business.Entities.Dynamic;
using Business.Interface.Dynamic;
using Business.SQL;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Business.Service.Dynamic
{
    public class RequestTypeControlScreenMappingService : IRequestTypeControlScreenMapping
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public RequestTypeControlScreenMappingService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }
        public int Save(int RequestTypeControlScreenID, int RequestTypeID, int RequestTypeControlID, int? RoleID, string ScreenName, string RenderType, int CreatedBy)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", RequestTypeControlScreenID);
                param.Add("@RequestTypeID", RequestTypeID);
                param.Add("@RequestTypeControlID", RequestTypeControlID);
                param.Add("@RoleID", RoleID);
                param.Add("@ScreenName", ScreenName);
                param.Add("@RenderType", RenderType);
                param.Add("@CreatedBy", CreatedBy);

                return QueryHelper.Save(connection, "SaveRequestTypeControlScreenMapping", param);
            }
            catch
            {
                throw;
            }
        }
        public List<RequestTypeControlScreenMappingMetadata> GetList(int CompanyID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyID", CompanyID);
                return QueryHelper.GetList<RequestTypeControlScreenMappingMetadata>(connection, "GetRequestTypeControlScreenMappingList", param);
            }
            catch
            {
                throw;
            }
        }

        public List<RequestTypeControlScreenMappingMetadata> GetList(string screenName)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ScreenName", screenName);
                return QueryHelper.GetList<RequestTypeControlScreenMappingMetadata>(connection, "GetRequestTypeControlScreenMappingListByScreen", param);
            }
            catch
            {
                throw;
            }
        }

        public RequestTypeControlScreenMappingMetadata GetDetail(int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", id);
                return QueryHelper.GetTableDetail<RequestTypeControlScreenMappingMetadata>(connection, "GetRequestTypeControlScreenMapping", param);
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
                return QueryHelper.Delete(connection, "DeleteRequestTypeControlScreenMapping", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DropdownMetadata> GetRequestTypes(int CompanyID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyID", CompanyID);
                return QueryHelper.GetList<DropdownMetadata>(connection, "GetRequestTypeList", param);
            }
            catch
            {
                throw;
            }
        }
        public List<DropdownMetadata> GetControlRenderTypes()
        {
            try
            {
                var list = new List<DropdownMetadata>();
                list.Add(new DropdownMetadata() { Text = "Edit", Value = ConstVariable.RENDER_TYPE_EDIT });
                list.Add(new DropdownMetadata() { Text = "Read only", Value = ConstVariable.RENDER_TYPE_READONLY });
                list.Add(new DropdownMetadata() { Text = "Hide", Value = ConstVariable.RENDER_TYPE_HIDE });
                return list;
            }
            catch
            {
                throw;
            }
        }

        public List<DropdownMetadata> GetScreenNameTypes()
        {
            try
            {
                var list = new List<DropdownMetadata>();
                list.Add(new DropdownMetadata() { Text = ConstVariable.SCREEN_NAME_NEW_CASE, Value = ConstVariable.SCREEN_NAME_NEW_CASE });
                list.Add(new DropdownMetadata() { Text = ConstVariable.SCREEN_NAME_UPDATE_CASE, Value = ConstVariable.SCREEN_NAME_UPDATE_CASE });
                return list;
            }
            catch
            {
                throw;
            }
        }

        public List<DropdownMetadata> GetControlsByRequestType(int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@RequestTypeID", id);
                return QueryHelper.GetList<DropdownMetadata>(connection, "GetRequestTypeControlsByRequestType", param);
            }
            catch
            {
                throw;
            }
        }
    }
}
