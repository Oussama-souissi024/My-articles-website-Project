using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyArticlesWebsiteProject.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyArticlesWebsiteProject.Data.SqlServerEF
{
    public class MyDBContext : IdentityDbContext

	{
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\\SQL5113.site4now.net;Database=db_aa7093_myarticleswebsit;User Id=db_aa7093_myarticleswebsit_admin;Password=2009OUssama024#;timeout=120");
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<AuthorPost> AuthorPost { get; set; }
}
}
