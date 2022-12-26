using Business.Entities.PartyType;
using Business.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Party Type Interface */

namespace Business.Interface.IPartyTypeService
{
    public interface IPartyTypeService
    {
        Task<PagedDataTable<PartyType>> GetAllPartyTypeAsync(int pageNo, int pageSize, string searchString = "", string orderBy = "CompanyName", string sortBy = "ASC");
        Task<int> PartyTypeCreateOrUpdateAsync(PartyType compnay);
        Task<PartyType> GetPartyTypeAsync(int PartyTypeID);
        

        
    }
}
