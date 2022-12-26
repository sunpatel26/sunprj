using Business.Entities.Master.Package;
using Business.Entities.PackageFormTxn;
using Business.SQL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interface.Marketing.IPackage
{
    public interface IPackageService
    {
        Task<PagedDataTable<PackageMaster>> GetAllPackageAsync();

        //07-12-2022 Below code working fine but it is comment because output shown in grid form. But we want to dispaly in card form. Dravesh Lokhande.
        //Task<PagedDataTable<PackageMaster>> GetAllPackageAsync(int pageNo, int pageSize, string searchString = "", string orderBy = "PackageName", string sortBy = "ASC");       

        Task<int> InsertOrUpdatePackageAsync(PackageMaster packageMaster);
        Task<PackageMaster> GetPackageAsync(int PackageID);
        Task<PagedDataTable<PackageFormTxn>> GetAllPackageDetailAsync(int packageID, int pageNo, int pageSize, string searchString = "", string orderBy = "PackageName", string sortBy = "ASC");
        Task<int> ActiveInActivePackageForm(List<PackageFormTxn> packageFormTxn);
    }
}
