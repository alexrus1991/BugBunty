using BugBunty_Api.Infrastucture.Models.Domaine;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugBunty_Api.Infrastucture.Configurations
{
    public class RapportConfiguration : IEntityTypeConfiguration<Rapport>
    {
        public void Configure(EntityTypeBuilder<Rapport> builder)
        {
            builder.ToTable("Rapport");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(m => m.Description).IsRequired().HasMaxLength(1000);

            builder
              .HasOne(r => r.Projet)
              .WithOne(p => p.Rapport)
              .HasForeignKey<Rapport>(dp => dp.ProjetId)
              .OnDelete(DeleteBehavior.NoAction);

            builder
               .HasOne(r => r.UserChercheur)
               .WithMany(uc => uc.Chercheur_Reports)
               .HasForeignKey(m => m.UserChercheurId)
               .OnDelete(DeleteBehavior.NoAction);

            builder
               .HasOne(r => r.UserAdmin)
               .WithMany(uc => uc.Admin_Validations_Reports)
               .HasForeignKey(m => m.UserAdminId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
