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
    public class AuthorEnity : IDataHelper<Author>
    {
        private MyDBContext DB;
        private Author _Table;
        public AuthorEnity()
        {
            DB = new MyDBContext();
        }

        public int Add(Author table)
        {
            if (DB.Database.CanConnect())
            {
                DB.Author.Add(table);
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
                DB.Author.Remove(_Table);
                DB.SaveChanges();
                return 1;
            }
            else
                return 0;
        }

        public int Edit(int ID, Author table)
        {
            DB = new MyDBContext();
            if (DB.Database.CanConnect())
            {
                DB.Author.Update(table);
                DB.SaveChanges();
                return 1;
            }
            else
                return 0;
        }

        public Author Find(int ID)
        {
            if (DB.Database.CanConnect())
            {
                //return DB.Author.Where(x => x.ID == ID).First();
                return DB.Author.FirstOrDefault(x => x.ID == ID);
            }
            else
                return null;
        }

        public List<Author> GetAllData()
        {
            if (DB.Database.CanConnect())
            {
                return DB.Author.ToList();
            }
            else
                return null;
        }

        public List<Author> GetDataByUser(string UserID)
        {
            throw new NotImplementedException();
        }

        public List<Author> Search(string SearchIthem)
        {
            if (DB.Database.CanConnect())
            {
                return DB.Author.Where(x => x.UserID.ToString().Contains(SearchIthem)
											|| x.ID.ToString().Contains(SearchIthem) 
                                            || x.UserName.Contains(SearchIthem) 
											|| x.FullName.Contains(SearchIthem)
											|| x.ProfileImageUrl.Contains(SearchIthem)
											|| x.Bio.Contains(SearchIthem)
											|| x.Facebook.Contains(SearchIthem)
                                       ).ToList();
            }
            else
                return null;
        }
    }
}
