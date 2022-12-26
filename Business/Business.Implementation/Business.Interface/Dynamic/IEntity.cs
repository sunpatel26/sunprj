using Business.Entities.Dynamic;
using Business.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface.Dynamic
{
    public interface IEntity
    {
        
        bool Delete(int id);

       
        int Save(int ID, string name, string Value, int SortOrder, bool IsActive, int UserId, short EntryTypeID, int CompanyID, string Code = null, bool IsDefaultSelected = false);
        List<MasterEntityMetadata> GetList(int CompanyID);
        MasterEntityMetadata GetDetail(int id, int CompanyID);
        bool Delete(string name);
        List<MasterEntityMetadata> GetListByName(string Name, int CompanyID);
        List<MasterEntityMetadata> GetDistinctNameList(int CompanyID);
        MasterEntityMetadata GetDetail(string Code, int CompanyID);
        List<MasterEntityEntryTypeMetadata> GetMasterEntryTypeList();
      
    }
}
