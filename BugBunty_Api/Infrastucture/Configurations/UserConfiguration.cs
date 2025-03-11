using BugBunty_Api.Infrastucture.Models.Domaine;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BugBunty_Api.Infrastucture.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.HasIndex(c => c.Email).IsUnique();

           // builder.HasOne(u => u.Entreprise)  // Chaque User appartient à une Entreprise
           //.WithMany(e => e.Employees) // Une Entreprise peut avoir plusieurs Users
           //.HasForeignKey(u => u.EntrepriseId)  // Utilise la clé étrangère EntrepriseId dans User
           //.OnDelete(DeleteBehavior.Restrict);
        }
    }
}
