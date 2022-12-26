using Business.Entities.Dynamic;
using Business.Interface.Dynamic;
using Business.SQL;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Business.Service.Dynamic
{
    public class RequestTypeModuleService : IRequestTypeModule
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public RequestTypeModuleService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }

        public int Save(int RequestTypeTitleID, int RequestTypeID, string Name, int SortOrder, bool IsActive, int UserId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", RequestTypeTitleID);
                param.Add("@RequestTypeID", RequestTypeID);
                param.Add("@Name", Name);
                param.Add("@SortOrder", SortOrder);
                param.Add("@IsActive", IsActive);
                param.Add("@CreatedBy", UserId);

                return QueryHelper.Save(connection, "SaveRequestTypeTitle", param);
            }
            catch 
            {
                throw ;
            }
        }
        public List<RequestTypeTitleMetadata> GetList(int? requestTypeID, int CompanyID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@requestTypeID", requestTypeID);
                param.Add("@CompanyID", CompanyID);
                return QueryHelper.GetList<RequestTypeTitleMetadata>(connection, "GetRequestTypeTitleList", param);
            }
            catch 
            {
                throw ;
            }
        }

        public RequestTypeTitleMetadata GetDetail(int id)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", id);
                return QueryHelper.GetTableDetail<RequestTypeTitleMetadata>(connection, "GetRequestTypeTitle", param);
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
                return QueryHelper.Delete(connection, "DeleteRequestTypeTitle", param);
            }
            catch
            {
                throw ;
            }
        }
    }
}
