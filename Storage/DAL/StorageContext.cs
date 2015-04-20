using Storage.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Storage.DAL
{
    public class StorageContext : DbContext
    {

        public StorageContext()
            : base("StorageContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Material> Material { get; set; }
        public DbSet<Detail> Detail { get; set; }
        public DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            this.Configuration.ProxyCreationEnabled = false;
        }
    }
}