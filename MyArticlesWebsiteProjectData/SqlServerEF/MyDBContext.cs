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
            //optionsBuilder.UseSqlServer(@"Server=.;Database=My_articles_website_Project;User Id=sa;Password=sa123456;Encrypt=True;TrustServerCertificate=True;Trusted_Connection=True;"
            optionsBuilder.UseSqlServer(@"Data Source=SQL8006.site4now.net;Initial Catalog=db_aa9f9e_myarticleswebsit;User Id=db_aa9f9e_myarticleswebsit_admin;Password=2009OUssama024#;TrustServerCertificate=True;"

);
                
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Author> Author { get; set; }

        public DbSet<AuthorPost> AuthorPost { get; set; }
}
}
