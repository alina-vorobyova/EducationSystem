﻿// <auto-generated />
using EducationSystem.StudentManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EducationSystem.StudentManagement.Infrastructure.Migrations
{
    [DbContext(typeof(StudentsDbContext))]
    [Migration("20200615130532_Passport")]
    partial class Passport
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EducationSystem.StudentManagement.Core.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("EducationSystem.StudentManagement.Core.Student", b =>
                {
                    b.OwnsOne("EducationSystem.Common.ValueObjects.FullName", "FullName", b1 =>
                        {
                            b1.Property<int>("StudentId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasColumnName("FirstName")
                                .HasColumnType("NVARCHAR(100)");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasColumnName("LastName")
                                .HasColumnType("NVARCHAR(100)");

                            b1.Property<string>("MiddleName")
                                .HasColumnName("MiddleName")
                                .HasColumnType("NVARCHAR(100)");

                            b1.HasKey("StudentId");

                            b1.ToTable("Student");

                            b1.WithOwner()
                                .HasForeignKey("StudentId");
                        });

                    b.OwnsOne("EducationSystem.Common.ValueObjects.Passport", "Passport", b1 =>
                        {
                            b1.Property<int>("StudentId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnName("Passport")
                                .HasColumnType("NVARCHAR(100)");

                            b1.HasKey("StudentId");

                            b1.ToTable("Student");

                            b1.WithOwner()
                                .HasForeignKey("StudentId");
                        });

                    b.OwnsMany("EducationSystem.Common.ValueObjects.Phone", "Phones", b1 =>
                        {
                            b1.Property<int>("StudentId")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnName("Number")
                                .HasColumnType("NVARCHAR(100)");

                            b1.Property<string>("Type")
                                .IsRequired()
                                .HasColumnName("Type")
                                .HasColumnType("NVARCHAR(100)");

                            b1.HasKey("StudentId", "Id");

                            b1.ToTable("Phone");

                            b1.WithOwner()
                                .HasForeignKey("StudentId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
