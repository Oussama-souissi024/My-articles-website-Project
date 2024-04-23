using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyArticlesWebsiteProject.Core;
using MyArticlesWebsiteProjectData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyArticlesWebsiteProject.Data.SqlServerEF
{
    public class CategoryEnity : IDataHelper<Category>
    {
        private MyDBContext DB;
        private Category _Table;
        public CategoryEnity()
        {
            DB = new MyDBContext();
        }

        public int Add(Category table)
        {
            if (DB.Database.CanConnect())
            {
                DB.Category.Add(table);
                DB.SaveChanges();
                return 1;
            }
            else
                return 0;
        }

        public int Delete(int ID)
        {
            if (DB.Database.CanConnect())
            {
                _Table = Find(ID);
                DB.Category.Remove(_Table);
                DB.SaveChanges();
                return 1;
            }
            else
                return 0;
        }

        public int Edit(int ID, Category table)
        {
            DB = new MyDBContext();
            if (DB.Database.CanConnect())
            {
                DB.Category.Update(table);
                DB.SaveChanges();
                return 1;
            }
            else
                return 0;
        }

        public Category Find(int ID)
        {
            if (DB.Database.CanConnect())
            {
                return DB.Category.Where(x => x.ID == ID).First();
            }
            else
                return null;
        }

        public List<Category> GetAllData()
        {
            if (DB.Database.CanConnect())
            {
                return DB.Category.ToList();
            }
            else
                return null;
        }

        public List<Category> GetDataByUser(string UserID)
        {
            throw new NotImplementedException();
        }

        public List<Category> Search(string SearchIthem)
        {
            if (DB.Database.CanConnect())
            {
                return DB.Category.Where(x => x.Name.Contains(SearchIthem) || x.ID.ToString().Contains(SearchIthem)).ToList();
            }
            else
                return null;
        }
    }
}
