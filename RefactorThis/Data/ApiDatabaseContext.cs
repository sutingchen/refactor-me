using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace refactor_me.Data
{
    public partial class ApiDatabaseContext : DbContext
    {
        public ApiDatabaseContext()
            : base("name=ApiDatabaseContext")
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductOption> ProductOptions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
