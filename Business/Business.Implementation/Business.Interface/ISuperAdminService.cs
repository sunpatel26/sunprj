using Business.Entities;
using Business.SQL;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface ISuperAdminService
    {
        Task<int> InsertError(ErrorMetadata item);

        Task AddPermission(PermissionMasterMetadata item);
        Task<PagedDataTable<PermissionMasterMetadata>> GetAllPermission(int roleid);

        #region "Address Type Master"
        Task<AddressTypeMasterMetadata> GetAddressTypeAsync(int addressTypeID);
        Task<int> InsertOrUpdateAddressTypeAsync(AddressTypeMasterMetadata item);
        Task<int> DeleteAddressTypeAsync(int addressTypeID);
        Task<PagedDataTable<AddressTypeMasterMetadata>> GetAllAddressTypeAsync(int pageNo = 1, int pageSize = 0, string searchString = "", string orderBy = "AddressTypeText", string sortBy = "ASC");
        #endregion

        #region "Business Type Master"
        Task<BusinessTypeMasterMetadata> GetBusinessTypeAsync(int businessTypeID);
        Task<int> InsertOrUpdateBusinessTypeAsync(BusinessTypeMasterMetadata item);
        Task<int> DeleteBusinessTypeAsync(int businessTypeID);
        Task<PagedDataTable<BusinessTypeMasterMetadata>> GetAllBusinessTypeAsync(int pageNo = 1, int pageSize = 0, string searchString = "", string orderBy = "BusinessTypeText", string sortBy = "ASC");
        #endregion

        #region Industry Type Master"
        Task<IndustryTypeMasterMetadata> GetIndustryTypeAsync(int industryTypeID);
        Task<int> InsertOrUpdateIndustryTypeAsync(IndustryTypeMasterMetadata item);
        Task<int> DeleteIndustryTypeAsync(int industryTypeID);
        Task<PagedDataTable<IndustryTypeMasterMetadata>> GetAllIndustryTypeAsync(int pageNo = 1, int pageSize = 0, string searchString = "", string orderBy = "IndustryTypeText", string sortBy = "ASC");
        #endregion

        #region "Department Master"
        Task<PagedDataTable<DepartmentMasterMetadata>> GetAllDepartmentAsync(int pageNo = 1, int pageSize = 0, string searchString = "", string orderBy = "DepartmentName", string sortBy = "ASC");

        #endregion

        #region "Designation Master"
        Task<PagedDataTable<DesignationMasterMetadata>> GetAllDesignationAsync(int pageNo = 1, int pageSize = 0, string searchString = "", string orderBy = "DesignationText", string sortBy = "ASC");

        #endregion

        #region "Email Group  Master"
        Task<PagedDataTable<EmailGroupMasterMetadata>> GetAllEmailGroupAsync(int pageNo = 1, int pageSize = 0, string searchString = "", string orderBy = "EmailGroupName", string sortBy = "ASC");

        #endregion

        #region "Country Master"
        Task<CountryMasterMetadata> GetCountryAsync(int id);
        Task<int> InsertOrUpdateCountryAsync(CountryMasterMetadata item);
        Task<int> DeleteCountryAsync(int id);
        Task<PagedDataTable<CountryMasterMetadata>> GetAllCountryAsync(int pageNo = 1, int pageSize = 0, string searchString = "", string orderBy = "CountryName", string sortBy = "ASC");
        #endregion

        #region "State Master"
        Task<StateMasterMetadata> GetStateAsync(int id);
        Task<int> InsertOrUpdateStateAsync(StateMasterMetadata item);
        Task<int> DeleteStateAsync(int id);
        Task<PagedDataTable<StateMasterMetadata>> GetAllStateAsync(int pageNo = 1, int pageSize = 0, string searchString = "", string orderBy = "StateName", string sortBy = "ASC", int countryID=0);
        #endregion

        #region "City Master"
        Task<CityMasterMetadata> GetCityAsync(int id);
        Task<int> InsertOrUpdateCityAsync(CityMasterMetadata item);
        Task<int> DeleteCityAsync(int id);
        Task<PagedDataTable<CityMasterMetadata>> GetAllCityAsync(int pageNo = 1, int pageSize = 0, string searchString = "", string orderBy = "CityName", string sortBy = "ASC", int stateID = 0);
        #endregion

        #region "District Master"
        Task<DistrictMasterMetadata> GetDistrictAsync(int id);
        Task<int> InsertOrUpdateDistrictAsync(DistrictMasterMetadata item);
        Task<int> DeleteDistrictAsync(int id);
        Task<PagedDataTable<DistrictMasterMetadata>> GetAllDistrictAsync(int pageNo = 1, int pageSize = 0, string searchString = "", string orderBy = "DistrictName", string sortBy = "ASC", int stateID = 0);
        #endregion

        #region "Taluka Master"
        Task<TalukaMasterMetadata> GetTalukaAsync(int id);
        Task<int> InsertOrUpdateTalukaAsync(TalukaMasterMetadata item);
        Task<int> DeleteTalukaAsync(int id);
        Task<PagedDataTable<TalukaMasterMetadata>> GetAllTalukaAsync(int pageNo = 1, int pageSize = 0, string searchString = "", string orderBy = "TalukaName", string sortBy = "ASC", int districtID = 0);
        #endregion

        #region "Zipcode Master"
        Task<ZipcodeMasterMetadata> GetZipcodeAsync(int id);
        Task<int> InsertOrUpdateZipcodeAsync(ZipcodeMasterMetadata item);
        Task<int> DeleteZipcodeAsync(int id);
        Task<PagedDataTable<ZipcodeMasterMetadata>> GetAllZipcodeAsync(int pageNo = 1, int pageSize = 0, string searchString = "", string orderBy = "ZIPCode", string sortBy = "ASC", int districtID = 0);
        Task<PagedDataTable<ZipcodeMasterMetadata>> GetAllZipcodeAutoCompletAsync(string searchString);
        #endregion

        PagedDataTable<IdentityProofTypeMetadata> GetIdentityProofTypeAsync();

        PagedDataTable<VehicleTypeMasterMetaData> GetVehicleTypeAsync();       

        PagedDataTable<FeedbackQuestionMasterMetadata> GetFeedbackQuestions();
        PagedDataTable<ZipcodeMasterMetadata> GetZipCodeAsync(string search);

        #region "Document Type Master"
        Task<DocumentTypeMasterMetadata> GetDocumentTypeAsync(int documentTypeID);
        Task<int> InsertOrUpdateDocumentTypeAsync(DocumentTypeMasterMetadata item);
        Task<int> DeleteDocumentTypeAsync(int documentTypeID);
        Task<PagedDataTable<DocumentTypeMasterMetadata>> GetAllDocumentTypeAsync(int pageNo = 1, int pageSize = 0, string searchString = "", string orderBy = "DocumentTypeName", string sortBy = "ASC");
        #endregion
    }
}
