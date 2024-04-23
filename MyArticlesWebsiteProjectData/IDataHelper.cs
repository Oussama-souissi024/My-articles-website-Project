using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyArticlesWebsiteProjectData
{
    public interface IDataHelper<Table>
    {
        //Read
        List<Table> GetAllData();
        List<Table> GetDataByUser(string UserID);
        List<Table> Search(string SearchIthem);
        Table Find(int ID);

        //Write
        int Add(Table table);
        int Edit(int ID, Table table);
        int Delete(int ID);

    }
}
