using Business.Entities.FormMasterEntitie;
using Business.SQL;
using System.Threading.Tasks;

namespace Business.Interface.IFormMaster
{
    public interface IFormMasterService
    {
        Task<PagedDataTable<FormMaster>> GetAllFormMasterAsync(int pageNo, int pageSize, string searchString = "", string orderBy = "FormName", string sortBy = "ASC");
        Task<FormMaster> GetFormMasterAsync(int FormID);
        Task<int> InsertOrUpdateFormMasterAsync(FormMaster formMaster);
    }
}
