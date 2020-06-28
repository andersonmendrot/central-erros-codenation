﻿// <auto-generated />
using System;
using CentralErros.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CentralErros.Infrastructure.Migrations
{
    [DbContext(typeof(CentralErrosContext))]
    partial class CentralErrosContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CentralErros.Domain.Models.ApplicationLayer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("ApplicationLayer");
                });

            modelBuilder.Entity("CentralErros.Domain.Models.Environment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Environment");
                });

            modelBuilder.Entity("CentralErros.Domain.Models.Error", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApplicationLayerId")
                        .HasColumnType("int");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("EnvironmentId")
                        .HasColumnType("int");

                    b.Property<int>("LanguageId")
                        .HasColumnType("int");

                    b.Property<int>("LevelId")
                        .HasColumnType("int");

                    b.Property<int>("NumberEvents")
                        .HasColumnType("int");

                    b.Property<string>("Origin")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("char");

                    b.Property<byte[]>("Timestamp")
                        .IsRequired()
                        .HasColumnType("timestamp");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationLayerId");

                    b.HasIndex("EnvironmentId");

                    b.HasIndex("LanguageId");

                    b.HasIndex("LevelId");

                    b.ToTable("Error");

                    b.HasCheckConstraint("constraint_status", "Status = 'y' or Status = 'n'");
                });

            modelBuilder.Entity("CentralErros.Domain.Models.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Language");
                });

            modelBuilder.Entity("CentralErros.Domain.Models.Level", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Level");
                });

            modelBuilder.Entity("CentralErros.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(254)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.Property<byte[]>("Timestamp")
                        .IsRequired()
                        .HasColumnName("CreatedAt")
                        .HasColumnType("timestamp");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("CentralErros.Domain.Models.Error", b =>
                {
                    b.HasOne("CentralErros.Domain.Models.ApplicationLayer", "ApplicationLayer")
                        .WithMany("Errors")
                        .HasForeignKey("ApplicationLayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CentralErros.Domain.Models.Environment", "Environment")
                        .WithMany("Errors")
                        .HasForeignKey("EnvironmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CentralErros.Domain.Models.Language", "Language")
                        .WithMany("Errors")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CentralErros.Domain.Models.Level", "Level")
                        .WithMany("Errors")
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
