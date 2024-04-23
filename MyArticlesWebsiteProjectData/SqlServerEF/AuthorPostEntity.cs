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
	public class AuthorPostEntity : IDataHelper<AuthorPost>
	{
		private MyDBContext DB;
		private AuthorPost _Table;
		public AuthorPostEntity()
		{
			DB = new MyDBContext();
		}

		public int Add(AuthorPost table)
		{
			if (DB.Database.CanConnect())
			{
				DB.AuthorPost.Add(table);
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
				DB.AuthorPost.Remove(_Table);
				DB.SaveChanges();
				return 1;
			}
			else
				return 0;
		}

		public int Edit(int ID, AuthorPost table)
		{
			DB = new MyDBContext();
			if (DB.Database.CanConnect())
			{
				DB.AuthorPost.Update(table);
				DB.SaveChanges();
				return 1;
			}
			else
				return 0;
		}

		public AuthorPost Find(int ID)
		{
			if (DB.Database.CanConnect())
			{
				return DB.AuthorPost.Where(x => x.ID == ID).First();
			}
			else
				return null;
		}

		public List<AuthorPost> GetAllData()
		{
			if (DB.Database.CanConnect())
			{
				return DB.AuthorPost.ToList();
			}
			else
				return null;
		}

		public List<AuthorPost> GetDataByUser(string UserID)
		{
			if (DB.Database.CanConnect())
			{
				return DB.AuthorPost.Where(x=> x.UserID==UserID).ToList();
			}
			else
				return null;
		}

		public List<AuthorPost> Search(string SearchIthem)
		{
			if (DB.Database.CanConnect())
			{
				return DB.AuthorPost.Where(x => x.UserID.ToString().Contains(SearchIthem)
										   || x.ID.ToString().Contains(SearchIthem)
										   || x.UserName.Contains(SearchIthem)
										   || x.FullName.Contains(SearchIthem)
										   || x.PostCategory.Contains(SearchIthem)
										   || x.PostTitle.Contains(SearchIthem)
										   || x.PostDescription.Contains(SearchIthem)
										   || x.PostImageUrl.Contains(SearchIthem)
										   || x.AddedDate.ToString().Contains(SearchIthem)
										   || x.AuthorID.ToString().Contains(SearchIthem)
										   || x.CategoryID.ToString().Contains(SearchIthem)
									  ).ToList();
			}
			else
				return null;
		}
	}
}
