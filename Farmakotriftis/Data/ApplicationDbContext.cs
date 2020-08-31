using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmakotriftis.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=mysql-ck1.clfj0tupwlr8.eu-west-1.rds.amazonaws.com;port=3306;database=farmakotriftis;user=mysqlckuser;password=mysqlckpass");
        }
        public DbSet<Model.Medicine> Medicines { get; set; }
    }
}
