﻿// <auto-generated />
using System;
using CallForPapers.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CallForPapers.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240331131049_changeNotNullToRequed")]
    partial class changeNotNullToRequed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "uuid-ossp");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CallForPapers.Infrastructure.Model.Admin", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("admins");

                    b.HasData(
                        new
                        {
                            id = new Guid("498aeec6-ec93-47ad-a32c-7638b194a0e3"),
                            login = "admin",
                            password = "admin"
                        });
                });

            modelBuilder.Entity("CallForPapers.Infrastructure.Model.Application", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<DateTime>("added_date")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValue(new DateTime(2024, 3, 31, 16, 10, 49, 700, DateTimeKind.Local).AddTicks(7880));

                    b.Property<bool>("daft")
                        .HasColumnType("boolean");

                    b.Property<string>("description")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("name")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("plan")
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.Property<Guid>("user_id")
                        .HasColumnType("uuid");

                    b.HasKey("id");

                    b.HasIndex("user_id");

                    b.ToTable("applications", t =>
                        {
                            t.HasCheckConstraint("ValidApplication", "user_id IS NOT NULL AND (activity IS NOT NULL  OR name IS NOT NULL OR description IS NOT NULL OR plan IS NOT NULL )");
                        });
                });

            modelBuilder.Entity("CallForPapers.Infrastructure.Model.User", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<string>("login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("CallForPapers.Infrastructure.Model.Application", b =>
                {
                    b.HasOne("CallForPapers.Infrastructure.Model.User", "user")
                        .WithMany("applications")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("CallForPapers.Infrastructure.Model.Activity.ActivityClass", "activity", b1 =>
                        {
                            b1.Property<Guid>("Applicationid")
                                .HasColumnType("uuid");

                            b1.Property<string>("activity")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("Applicationid");

                            b1.ToTable("applications");

                            b1.WithOwner()
                                .HasForeignKey("Applicationid");
                        });

                    b.Navigation("activity")
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("CallForPapers.Infrastructure.Model.User", b =>
                {
                    b.Navigation("applications");
                });
#pragma warning restore 612, 618
        }
    }
}