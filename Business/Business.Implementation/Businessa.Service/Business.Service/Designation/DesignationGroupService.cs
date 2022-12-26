using Business.Entities.Designation;
using Business.Interface.Designation;
using Business.SQL;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Business.Service.Designation
{
    public class DesignationGroupService : IDesignationGroupService
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public DesignationGroupService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }

        /*Create New Department Group*/
        public async Task<int> CreateDesignationtGroupAsync(DesignationGroupMaster designationGroupMaster)
        {
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@DesignationGroupID", designationGroupMaster.DesignationGroupID)
                ,new SqlParameter("@DesignationGroupText", designationGroupMaster.DesignationGroupText)
                ,new SqlParameter("@Remark", designationGroupMaster.Remark)
                ,new SqlParameter("@IsActive", designationGroupMaster.IsActive)
                ,new SqlParameter("@CreatedOrModifiedBy", designationGroupMaster.CreatedOrModifiedBy)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_DesignationGroupMaster", param);

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
