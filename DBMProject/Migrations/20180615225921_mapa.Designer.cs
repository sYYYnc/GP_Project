﻿// <auto-generated />
using DBMProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace DBMProject.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180615225921_mapa")]
    partial class mapa
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DBMProject.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("Number");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("DBMProject.Models.Comentario", b =>
                {
                    b.Property<int>("ComentarioId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descricao");

                    b.Property<int>("ProjetoId");

                    b.HasKey("ComentarioId");

                    b.HasIndex("ProjetoId");

                    b.ToTable("Comentarios");
                });

            modelBuilder.Entity("DBMProject.Models.Coordenadas", b =>
                {
                    b.Property<int>("CoordenadasId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Altitude");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.HasKey("CoordenadasId");

                    b.ToTable("Coordenadas");
                });

            modelBuilder.Entity("DBMProject.Models.ProjectsManagement.AcademicDegree", b =>
                {
                    b.Property<int>("AcademicDegreeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AcademicDegreeName")
                        .IsRequired();

                    b.HasKey("AcademicDegreeId");

                    b.ToTable("AcademicDegrees");
                });

            modelBuilder.Entity("DBMProject.Models.ProjectsManagement.Projeto", b =>
                {
                    b.Property<int>("ProjetoId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AcademicDegreeId");

                    b.Property<string>("Autor");

                    b.Property<double>("Classificacao");

                    b.Property<int?>("CoordenadasId");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Imagem");

                    b.Property<string>("Localizacao");

                    b.Property<int>("NrDeVotos");

                    b.Property<string>("ProjectFileName");

                    b.Property<string>("ProjectName")
                        .IsRequired();

                    b.Property<double>("Size");

                    b.Property<string>("Technology")
                        .IsRequired();

                    b.Property<bool>("Validado");

                    b.HasKey("ProjetoId");

                    b.HasIndex("AcademicDegreeId");

                    b.HasIndex("CoordenadasId");

                    b.ToTable("Projetos");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("DBMProject.Models.Comentario", b =>
                {
                    b.HasOne("DBMProject.Models.ProjectsManagement.Projeto", "Projeto")
                        .WithMany()
                        .HasForeignKey("ProjetoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DBMProject.Models.ProjectsManagement.Projeto", b =>
                {
                    b.HasOne("DBMProject.Models.ProjectsManagement.AcademicDegree", "AcademicDegree")
                        .WithMany()
                        .HasForeignKey("AcademicDegreeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DBMProject.Models.Coordenadas", "Coordenadas")
                        .WithMany()
                        .HasForeignKey("CoordenadasId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DBMProject.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DBMProject.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DBMProject.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("DBMProject.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
