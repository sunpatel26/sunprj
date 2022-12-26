using Business.Entities.Department;
using Business.Interface.Department;
using Business.SQL;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Business.Service.Department
{
    public class DepartmentGroupService : IDepartmentGroupService
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public DepartmentGroupService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }

        /*Create New Department Group*/
        public async Task<int> CreateDepartmentGroupAsync(DepartmentGroupMaster departmentGroupMaster)
        {
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@DepartmentGroupID", departmentGroupMaster.DepartmentGroupID)
                ,new SqlParameter("@DepartmentGroupText", departmentGroupMaster.DepartmentGroupText)
                ,new SqlParameter("@Remark", departmentGroupMaster.Remark)
                ,new SqlParameter("@IsActive", departmentGroupMaster.IsActive)
                ,new SqlParameter("@CreatedOrModifiedBy", departmentGroupMaster.CreatedOrModifiedBy)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_DepartmentGroupMaster", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /*Create New Department Group*/
    }
}
