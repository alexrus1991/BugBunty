using BugBunty_Api.Infrastucture.Configurations;
using BugBunty_Api.Infrastucture.Models.Domaine;
using Microsoft.EntityFrameworkCore;

namespace BugBunty_Api.Infrastucture.ContextDB
{
    public class BugBuntyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Rapport> Rapports { get; set; }
        public DbSet<Projet> Projets { get; set; }
        public DbSet<DetailProjet> Details { get; set; }
        public BugBuntyDbContext(DbContextOptions options) : base(options)
        {
        }

        protected BugBuntyDbContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RapportConfiguration());
            modelBuilder.ApplyConfiguration(new ProjetConfiguration());
            modelBuilder.ApplyConfiguration(new DetaiProjetConfiguration());
            
        }
        
    }
}
