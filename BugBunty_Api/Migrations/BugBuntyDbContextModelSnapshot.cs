﻿// <auto-generated />
using System;
using BugBunty_Api.Infrastucture.ContextDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BugBunty_Api.Migrations
{
    [DbContext(typeof(BugBuntyDbContext))]
    partial class BugBuntyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BugBunty_Api.Infrastucture.Models.Domaine.DetailProjet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Emplacement_Bug")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Explication_Globale")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("ProjetId")
                        .HasColumnType("int");

                    b.Property<string>("Regles")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Vilnerabilite_Basse")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Vilnerabilite_Haute")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Vilnerabilite_Moyenne")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("ProjetId")
                        .IsUnique();

                    b.ToTable("DetailProjet", (string)null);
                });

            modelBuilder.Entity("BugBunty_Api.Infrastucture.Models.Domaine.Projet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Niveau_Vulnerabilite")
                        .HasColumnType("int");

                    b.Property<int>("Prix_Severite")
                        .HasColumnType("int");

                    b.Property<bool>("Statut")
                        .HasColumnType("bit");

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Projet", (string)null);
                });

            modelBuilder.Entity("BugBunty_Api.Infrastucture.Models.Domaine.Rapport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateRapport")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("ProjetId")
                        .HasColumnType("int");

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserAdminId")
                        .HasColumnType("int");

                    b.Property<int>("UserChercheurId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjetId")
                        .IsUnique();

                    b.HasIndex("UserAdminId");

                    b.HasIndex("UserChercheurId");

                    b.ToTable("Rapport", (string)null);
                });

            modelBuilder.Entity("BugBunty_Api.Infrastucture.Models.Domaine.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleUser")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("BugBunty_Api.Infrastucture.Models.Domaine.DetailProjet", b =>
                {
                    b.HasOne("BugBunty_Api.Infrastucture.Models.Domaine.Projet", "Projet")
                        .WithOne("detailProjet")
                        .HasForeignKey("BugBunty_Api.Infrastucture.Models.Domaine.DetailProjet", "ProjetId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Projet");
                });

            modelBuilder.Entity("BugBunty_Api.Infrastucture.Models.Domaine.Projet", b =>
                {
                    b.HasOne("BugBunty_Api.Infrastucture.Models.Domaine.User", "UserEntreprise")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("UserEntreprise");
                });

            modelBuilder.Entity("BugBunty_Api.Infrastucture.Models.Domaine.Rapport", b =>
                {
                    b.HasOne("BugBunty_Api.Infrastucture.Models.Domaine.Projet", "Projet")
                        .WithOne("Rapport")
                        .HasForeignKey("BugBunty_Api.Infrastucture.Models.Domaine.Rapport", "ProjetId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BugBunty_Api.Infrastucture.Models.Domaine.User", "UserAdmin")
                        .WithMany("Admin_Validations_Reports")
                        .HasForeignKey("UserAdminId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("BugBunty_Api.Infrastucture.Models.Domaine.User", "UserChercheur")
                        .WithMany("Chercheur_Reports")
                        .HasForeignKey("UserChercheurId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Projet");

                    b.Navigation("UserAdmin");

                    b.Navigation("UserChercheur");
                });

            modelBuilder.Entity("BugBunty_Api.Infrastucture.Models.Domaine.Projet", b =>
                {
                    b.Navigation("Rapport");

                    b.Navigation("detailProjet");
                });

            modelBuilder.Entity("BugBunty_Api.Infrastucture.Models.Domaine.User", b =>
                {
                    b.Navigation("Admin_Validations_Reports");

                    b.Navigation("Chercheur_Reports");
                });
#pragma warning restore 612, 618
        }
    }
}
