using BugBunty_Api.Infrastucture.Models.Domaine;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugBunty_Api.Infrastucture.Configurations
{
    public class DetaiProjetConfiguration : IEntityTypeConfiguration<DetailProjet>
    {
        public void Configure(EntityTypeBuilder<DetailProjet> builder)
        {
            builder.ToTable("DetailProjet");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();


            builder.Property(m => m.Explication_Globale).IsRequired().HasMaxLength(1000);
            builder.Property(m => m.Regles).IsRequired().HasMaxLength(500);
            builder.Property(m => m.Emplacement_Bug).IsRequired().HasMaxLength(1000);
            builder.Property(m => m.Vilnerabilite_Haute).IsRequired().HasMaxLength(250);
            builder.Property(m => m.Vilnerabilite_Moyenne).IsRequired().HasMaxLength(250);
            builder.Property(m => m.Vilnerabilite_Basse).IsRequired().HasMaxLength(250);
           
            builder
               .HasOne(dp => dp.Projet)
               .WithOne(p => p.detailProjet)
               .HasForeignKey<DetailProjet>(dp => dp.ProjetId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
