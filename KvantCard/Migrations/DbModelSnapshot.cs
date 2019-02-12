﻿// <auto-generated />
using System;
using KvantShared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KvantCard.Migrations
{
    [DbContext(typeof(Db))]
    partial class DbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity("KvantCard.Model.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Apartment");

                    b.Property<string>("City");

                    b.Property<int?>("ContactId");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("Deleted");

                    b.Property<string>("Street");

                    b.Property<DateTime>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("ContactId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("KvantCard.Model.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("Deleted");

                    b.Property<DateTime>("Updated");

                    b.Property<string>("Emails");

                    b.Property<string>("PhoneNumbers");

                    b.HasKey("Id");

                    b.ToTable("Contact");
                });

            modelBuilder.Entity("KvantCard.Model.DictionaryItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("Deleted");

                    b.Property<string>("Title");

                    b.Property<DateTime>("Updated");

                    b.HasKey("Id");

                    b.ToTable("DictionaryItems");
                });

            modelBuilder.Entity("KvantCard.Model.Parent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BirthDate");

                    b.Property<int?>("ContactId");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("Deleted");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("MiddleName");

                    b.Property<int>("StatusId");

                    b.Property<int?>("StudentId");

                    b.Property<DateTime>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("ContactId");

                    b.HasIndex("StudentId");

                    b.ToTable("Parents");
                });

            modelBuilder.Entity("KvantCard.Model.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BirthDate");

                    b.Property<int?>("ContactId");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("DateTime");

                    b.Property<DateTime?>("Deleted");

                    b.Property<int>("DocumentSetId");

                    b.Property<string>("FirstName");

                    b.Property<int>("GroupId");

                    b.Property<int>("KvantumId");

                    b.Property<string>("LastName");

                    b.Property<int>("LevelId");

                    b.Property<int>("MentorId");

                    b.Property<string>("MiddleName");

                    b.Property<int>("Parent1Id");

                    b.Property<int>("ProgramId");

                    b.Property<int>("SchoolId");

                    b.Property<DateTime>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("ContactId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("KvantCard.Model.Address", b =>
                {
                    b.HasOne("KvantCard.Model.Contact")
                        .WithMany("Address")
                        .HasForeignKey("ContactId");
                });

            modelBuilder.Entity("KvantCard.Model.Parent", b =>
                {
                    b.HasOne("KvantCard.Model.Contact", "Contact")
                        .WithMany()
                        .HasForeignKey("ContactId");

                    b.HasOne("KvantCard.Model.Student")
                        .WithMany("Parents")
                        .HasForeignKey("StudentId");
                });

            modelBuilder.Entity("KvantCard.Model.Student", b =>
                {
                    b.HasOne("KvantCard.Model.Contact", "Contact")
                        .WithMany()
                        .HasForeignKey("ContactId");
                });
#pragma warning restore 612, 618
        }
    }
}
