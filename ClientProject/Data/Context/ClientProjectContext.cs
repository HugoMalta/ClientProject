using ClientProject.Data.Mapping;
using ClientProject.Models;
using System.Data.Entity;

namespace ClientProject.Data.Context
{
    public class ClientProjectContext : DbContext
    {
        public ClientProjectContext() : base("ClientProject")
        {
        }

        public DbSet<ClientModel> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //columns of type string will be created as varchar and not as nvarchar.
            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Configurations.Add(new ClientMapping());
        }
    }
}