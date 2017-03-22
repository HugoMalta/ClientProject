using ClientProject.Models;
using System.Data.Entity.ModelConfiguration;

namespace ClientProject.Data.Mapping
{
    public class ClientMapping : EntityTypeConfiguration<ClientModel>
    {
        public ClientMapping()
        {
            HasKey(client => client.Id);

            Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(200);

            Property(client => client.Cpf)
                .IsRequired()
                .HasMaxLength(11);

            Property(client => client.Email)
                .IsRequired()
                .HasMaxLength(100);

            Property(client => client.MaritalStatus)
                .IsRequired();

            Property(client => client.Street)
                .IsRequired()
                .HasMaxLength(100);

            Property(client => client.Number)
                .IsRequired()
                .HasMaxLength(50);

            Property(client => client.City)
                .IsRequired()
                .HasMaxLength(50);

            Property(client => client.State)
                .IsRequired()
                .HasMaxLength(50);

            Property(client => client.ZipCode)
                .IsRequired()
                .HasMaxLength(20);

            Property(client => client.Country)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}