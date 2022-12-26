using Business.Entities.ProductPhotoPath;
using Business.Interface.ProductImages;
using Business.SQL;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Business.Service.ProductImages
{
    public class ProductImages : IProductImages
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public ProductImages(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }

        public async Task<PagedDataTable<ProductPhotoPath>> GetImagePath()
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

                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_ProductImages", param))
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
                    PagedDataTable<ProductPhotoPath> lst = table.ToPagedDataTableList<ProductPhotoPath>
                        (1, 0, totalItemCount, string.Empty, string.Empty, "1");
                    return lst;
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> AddOrEditProductImages(ProductPhotoPath productPhotoPath)
        {
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@ProductImageID",productPhotoPath.ProductImageID)
                        ,new SqlParameter("@ProductImageText",productPhotoPath.ProductImageText)
                        ,new SqlParameter("@ImagePath",productPhotoPath.ImagePath)
                        ,new SqlParameter("@UOMID",productPhotoPath.UOMID)
                        ,new SqlParameter("@Description",productPhotoPath.Description)
                        ,new SqlParameter("@IsActive",productPhotoPath.IsActive)
                        ,new SqlParameter("@CreatedOrModifiedBy",productPhotoPath.CreatedOrModifiedBy)
                        ,new SqlParameter("@ItemCategoryID",productPhotoPath.ItemCategoryID)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_ProductImages", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> UpdateProductPhoto(ProductPhotoPath productPhotoPath)
        {
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@EmployeeID", productPhotoPath.ProductImageID),
                new SqlParameter("@ImageName", productPhotoPath.ProductImageText),
                new SqlParameter("@ImagePath", productPhotoPath.ImagePath),
                new SqlParameter("@IsActive", productPhotoPath.IsActive),
                new SqlParameter("@CreatedOrModifiedBy", productPhotoPath.CreatedOrModifiedBy ),

                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_EmployeeProfileImage", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductPhotoPath> GetProductImageDetailAsync(int ProductImageID)
        {
            ProductPhotoPath result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@ProductImageID", ProductImageID) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_ProductImages", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<ProductPhotoPath>();
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

        //public async Task<ProductPhotoPath> GetImagePath()
        //{
        //    ProductPhotoPath result = null;
        //    try
        //    {
        //        SqlParameter[] param = { new SqlParameter("@ProductImageID", ) };
        //        DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_ProductImages", param);
        //        if (ds != null)
        //        {
        //            if (ds.Tables.Count > 0)
        //            {
        //                if (ds.Tables[0].Rows.Count > 0)
        //                {
        //                    DataRow dr = ds.Tables[0].Rows[0];
        //                    result = dr.ToPagedDataTableList<ProductPhotoPath>();
        //                }
        //            }
        //        }
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
