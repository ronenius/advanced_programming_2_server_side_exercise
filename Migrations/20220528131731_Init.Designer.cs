﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using advanced_programming_2_server_side_exercise.Data;

#nullable disable

namespace advanced_programming_2_server_side_exercise.Migrations
{
    [DbContext(typeof(advanced_programming_2_server_side_exerciseContext))]
    [Migration("20220528131731_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("advanced_programming_2_server_side_exercise.Models.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ContactNickname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactServer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactUsername")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Username");

                    b.ToTable("Contact");
                });

            modelBuilder.Entity("advanced_programming_2_server_side_exercise.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ContactId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsSent")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ContactId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("advanced_programming_2_server_side_exercise.Models.User", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Username");

                    b.ToTable("User");
                });

            modelBuilder.Entity("advanced_programming_2_server_side_exercise.Models.Contact", b =>
                {
                    b.HasOne("advanced_programming_2_server_side_exercise.Models.User", null)
                        .WithMany("Contacts")
                        .HasForeignKey("Username");
                });

            modelBuilder.Entity("advanced_programming_2_server_side_exercise.Models.Message", b =>
                {
                    b.HasOne("advanced_programming_2_server_side_exercise.Models.Contact", null)
                        .WithMany("Messages")
                        .HasForeignKey("ContactId");
                });

            modelBuilder.Entity("advanced_programming_2_server_side_exercise.Models.Contact", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("advanced_programming_2_server_side_exercise.Models.User", b =>
                {
                    b.Navigation("Contacts");
                });
#pragma warning restore 612, 618
        }
    }
}
