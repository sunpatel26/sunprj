using Business.Entities.Dynamic;
using Business.Interface.Dynamic;
using Business.SQL;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service.Dynamic
{
    public class EntityService : IEntity
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public EntityService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
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
                param.Add("@Code", Code);
                return QueryHelper.Save(connection,"SaveMasterList", param);
            }
            catch 
            {
                throw;
            }
        }

        public int Save(int EntityID, string DatabaseName, string TableName, string Label, string ValueField, string TextField, string CaseTypeRole_DatabaseName, string CaseTypeRole_TableName, string CaseTypeRole_ValueField, string CaseTypeRole_TextField, string AssignedTo_DatabaseName, string AssignedTo_TableName, string AssignedTo_ValueField, string AssignedTo_TextField)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@EntityID", EntityID);
                param.Add("@DatabaseName", DatabaseName);
                param.Add("@TableName", TableName);
                param.Add("@Label", Label);
                param.Add("@ValueField", ValueField);
                param.Add("@TextField", TextField);

                param.Add("@CaseTypeRole_DatabaseName", CaseTypeRole_DatabaseName);
                param.Add("@CaseTypeRole_TableName", CaseTypeRole_TableName);
                param.Add("@CaseTypeRole_ValueField", CaseTypeRole_ValueField);
                param.Add("@CaseTypeRole_TextField", CaseTypeRole_TextField);

                param.Add("@AssignedTo_DatabaseName", AssignedTo_DatabaseName);
                param.Add("@AssignedTo_TableName", AssignedTo_TableName);
                param.Add("@AssignedTo_ValueField", AssignedTo_ValueField);
                param.Add("@AssignedTo_TextField", AssignedTo_TextField);

                return QueryHelper.Save(connection, "SaveEntityDatabaseSetup", param);
            }
            catch 
            {
                throw ;
            }
        }
        public List<MasterEntityMetadata> GetList(int CompanyID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyID", CompanyID);
                param.Add("@For", "Entity");
                return QueryHelper.GetList<MasterEntityMetadata>(connection, "GetMasterList", param);
            }
            catch 
            {
                throw ;
            }
        }

        public MasterEntityMetadata GetDetail(int id, int CompanyID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@ID", id);
                param.Add("@For", "Entity");
                param.Add("@CompanyID", CompanyID);
                var result = QueryHelper.GetTableDetail<MasterEntityMetadata>(connection, "GetMasterList", param);

                param = new DynamicParameters();
                param.Add("@EntityID", result.MasterListID);
                result.Database = QueryHelper.GetTableDetail<EntityDatabaseSetupMetadata>(connection, "GetEntityDatabaseSetup", param);
                return result;
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
                return QueryHelper.Delete(connection, "DeleteMasterList", param);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                {
                    return false;
                }
                throw;
            }
        }
        public List<MasterEntityMetadata> GetListByName(string Name, int CompanyID)
        {
            return null;
        }
        public List<MasterEntityMetadata> GetDistinctNameList(int CompanyID)
        {
            return null;
        }

        public List<MasterEntityMetadata> GetListByCompany(int CompanyID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@CompanyID", CompanyID);
                return QueryHelper.GetList<MasterEntityMetadata>(connection, "GetEntityListByCompany", param);
            }
            catch 
            {
                throw;
            }
        }

        public MasterEntityMetadata GetDetail(string Code, int CompanyID)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Code", Code);
                param.Add("@CompanyID", CompanyID);
                return QueryHelper.GetTableDetail<MasterEntityMetadata>(connection, "GetEntityByCode", param);
            }
            catch 
            {
                throw;
            }
        }

        public bool Delete(string name)
        {
            throw new NotImplementedException();
        }

        public List<MasterEntityEntryTypeMetadata> GetMasterEntryTypeList()
        {
            throw new NotImplementedException();
        }
    }
}
