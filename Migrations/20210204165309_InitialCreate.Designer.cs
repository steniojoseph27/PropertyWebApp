﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PropertyWebApp.DataAccess;

namespace PropertyWebApp.Migrations
{
    [DbContext(typeof(PropertyDbContext))]
    [Migration("20210204165309_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("PropertyWebApp.Models.Property", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("GrossYield")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ListPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MonthlyRent")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("YearBuilt")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Properties");
                });
#pragma warning restore 612, 618
        }
    }
}
