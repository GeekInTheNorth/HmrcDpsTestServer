using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using HmrcTpvsProxy.DAL.Entities;

namespace HmrcTpvsProxy.DAL
{
    public class DpsContext : DbContext
    {
        public DpsContext() : base("DpsTestData")
        {
        }

        public DbSet<Dataset> Datasets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}