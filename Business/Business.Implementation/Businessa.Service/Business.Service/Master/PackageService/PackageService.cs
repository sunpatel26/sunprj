using Business.Entities.Master.Package;
using Business.Entities.PackageFormTxn;
using Business.Interface.Marketing.IPackage;
using Business.SQL;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Business.Service.Master.PackageService
{
    public class PackageService : IPackageService
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public PackageService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }

        #region Package Index Page Start

        public async Task<PagedDataTable<PackageMaster>> GetAllPackageAsync()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",1)
                        ,new SqlParameter("@PageSize","0")
                        ,new SqlParameter("@SearchString",string.Empty)
                        ,new SqlParameter("@OrderBy",string.Empty)
                        ,new SqlParameter("@SortBy",1)
                        };

                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_PackageMaster", param))
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
                    PagedDataTable<PackageMaster> lst = table.ToPagedDataTableList<PackageMaster>
                        (1, 0, totalItemCount, string.Empty, string.Empty, "1");
                    return lst;
                }
            }
            catch
            {
                throw;
            }
        }

        /*07-12-2022 Below code working fine but it is comment because output shown in grid form. But we want to dispaly in card form. Dravesh Lokhande.*/


        //public async Task<PagedDataTable<PackageMaster>> GetAllPackageAsync(int pageNo, int pageSize, string searchString = "", string orderBy = "PackageName", string sortBy = "ASC")
        //{
        //    DataTable table = new DataTable();
        //    int totalItemCount = 0;
        //    PagedDataTable<PackageMaster> lst = null;
        //    try
        //    {
        //        SqlParameter[] param = {
        //                new SqlParameter("@PageNo",pageNo)
        //                ,new SqlParameter("@PageSize",pageSize)
        //                ,new SqlParameter("@SearchString",searchString)
        //                ,new SqlParameter("@OrderBy",orderBy)
        //                ,new SqlParameter("@SortBy",sortBy=="ASC"?0:1)

        //                };
        //        using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_PackageMaster", param))
        //        {
        //            if (ds.Tables.Count > 0)
        //            {
        //                table = ds.Tables[0];
        //                if (table.Rows.Count > 0)
        //                {
        //                    if (table.ContainColumn("TotalCount"))
        //                        totalItemCount = Convert.ToInt32(table.Rows[0]["TotalCount"]);
        //                    else
        //                        totalItemCount = table.Rows.Count;
        //                }
        //            }
        //            lst = table.ToPagedDataTableList<PackageMaster>(pageNo, pageSize, totalItemCount);
        //            return lst;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        if (table != null)
        //            table.Dispose();
        //    }
        //}
        #endregion

        #region Package silder Start
        //Get detail of individual row
        public async Task<PackageMaster> GetPackageAsync(int PackageID)
        {
            PackageMaster result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@PackageID", PackageID) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_PackageMaster", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<PackageMaster>();
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
        #endregion

        #region Package Insert or Update Start
        public async Task<int> InsertOrUpdatePackageAsync(PackageMaster packageMaster)
        {
            try
            {
                SqlParameter[] param = {
                 new SqlParameter("@PackageID", packageMaster.PackageID)
                 ,new SqlParameter("@PackageName",packageMaster.PackageName)
                 ,new SqlParameter("@Description", packageMaster.Description)
                 ,new SqlParameter("@PackageTypeID", packageMaster.PackageTypeID)
                 ,new SqlParameter("@PackageColor", packageMaster.PackageColor)
                 ,new SqlParameter("@IsActive", packageMaster.IsActive)
                 ,new SqlParameter("@CreatedOrModifiedBy", packageMaster.CreatedOrModifiedBy)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_PackageMaster", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Package Detail Page Start
        public async Task<PagedDataTable<PackageFormTxn>> GetAllPackageDetailAsync(int packageID, int pageNo, int pageSize, string searchString = "", string orderBy = "PackageName", string sortBy = "ASC")
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            string packagename = null;
            PagedDataTable<PackageFormTxn> lst = null;
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@PackageID",packageID)
                   /* ,new SqlParameter("@PackageName",packageName)*/
                        /*new SqlParameter("@PageNo",pageNo)
                        ,new SqlParameter("@PageSize",pageSize)
                        ,new SqlParameter("@SearchString",searchString)
                        ,new SqlParameter("@OrderBy",orderBy)
                        ,new SqlParameter("@SortBy",sortBy=="ASC"?0:1)*/

                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetALL_FormAssigntoPackage", param))
                {
                    if (ds.Tables.Count > 0)
                    {
                        table = ds.Tables[0];
                        if (table.Rows.Count > 0)
                        {
                            if (table.ContainColumn("TotalCount"))
                            {
                                totalItemCount = Convert.ToInt32(table.Rows[0]["TotalCount"]);
                                packagename = Convert.ToString(table.Rows[0]["PackageName"]);
                            }
                            else
                            {
                                totalItemCount = table.Rows.Count;
                                packagename = "";
                            }
                        }
                    }
                    lst = table.ToPagedDataTableList<PackageFormTxn>(pageNo, pageSize, totalItemCount, packagename);
                    return lst;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (table != null)
                    table.Dispose();
            }
        }
        #endregion

        #region

        public async Task<int> ActiveInActivePackageForm(List<PackageFormTxn> packageFormTxn)
        {
            //DataTable btnPackagesForm = new DataTable();
            DataTable btnPackagesForm = new DataTable("UDTT_PackageFormTxn");
            /*//we create column names as per the type in DB 
            btnPackagesForm.Columns.Add("PackageFormID", typeof(int));
            btnPackagesForm.Columns.Add("PackageID", typeof(int));
            btnPackagesForm.Columns.Add("FormID", typeof(int));
            btnPackagesForm.Columns.Add("AddNew", typeof(bool));
            btnPackagesForm.Columns.Add("Edit", typeof(bool));
            btnPackagesForm.Columns.Add("Cancel", typeof(bool));
            btnPackagesForm.Columns.Add("View", typeof(bool));
            btnPackagesForm.Columns.Add("Print", typeof(bool));
            btnPackagesForm.Columns.Add("Email", typeof(bool));
            btnPackagesForm.Columns.Add("EmailWithAttachment", typeof(bool));
            btnPackagesForm.Columns.Add("ExportToPDF", typeof(bool));
            btnPackagesForm.Columns.Add("ExportToExcel", typeof(bool));
            btnPackagesForm.Columns.Add("Search", typeof(bool));
            btnPackagesForm.Columns.Add("IsActive", typeof(bool));
            btnPackagesForm.Columns.Add("CreatedOrModifyBy", typeof(int)); 
            //btnPackagesForm.Columns.Add("RequirementShiftTimingFrom", typeof(string));
            //btnPackagesForm.Columns.Add("RequirementShiftTimingTo", typeof(string));
            //btnPackagesForm.Columns.Add("RequirementHoursPerWeek", typeof(decimal));
            //and fill in some values 
            if (packageFormTxn.IsActive != null)
            {

                foreach (var shift in packageFormTxn.IsActive)
                {
                    btnPackagesForm.Rows.Add(shift.PackageFormID
                                    , shift.PackageID
                                    , shift.FormID
                                    , shift.AddNew
                                    , shift.Edit
                                    , shift.Cancel
                                    , shift.View
                                    , shift.Print
                                    , shift.Email
                                    , shift.EmailWithAttachment
                                    , shift.ExportToPDF
                                    , shift.ExportToExcel
                                    , shift.Search
                                    , shift.IsActive
                                    , shift.CreatedOrModifyBy);
                }
            }*/


            try
            {

                SqlParameter[] param =  {
                   new SqlParameter("@UDTVPackageFormTxn", btnPackagesForm)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_FormAssigntoPackageDetail", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
