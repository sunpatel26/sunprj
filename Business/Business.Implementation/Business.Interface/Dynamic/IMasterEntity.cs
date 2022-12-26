using Business.Entities.Dynamic;
using Business.SQL;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface.Dynamic
{
    public interface IMasterEntity
    {
        Task<PagedDataTable<MasterEntityMetadata>>  GetDistinctNameList(int compnayID,int page=1,int pagesize=20, string search="", string orderby="ID", string sortby="ASC");

        int Save(int ID, string name, string Value, int SortOrder, bool IsActive, int UserId, short EntryTypeID, int CompanyID, string Code = null, bool IsDefaultSelected = false);
        List<MasterEntityMetadata> GetList(int CompanyID);
        MasterEntityMetadata GetDetail(int id, int CompanyID);
        bool Delete(int id);
        bool Delete(string name);
        List<MasterEntityMetadata> GetListByName(string Name, int CompanyID);
        List<MasterEntityMetadata> GetDistinctNameList(int CompanyID);
        MasterEntityMetadata GetDetail(string Code, int CompanyID);
        List<MasterEntityEntryTypeMetadata> GetMasterEntryTypeList();
        List<DropdownMetadata> GetDropdownKeys(int CompanyID);
        List<DropdownMetadata> GetDropdownValueList(string Key, int CompanyID);
        
    }
}
