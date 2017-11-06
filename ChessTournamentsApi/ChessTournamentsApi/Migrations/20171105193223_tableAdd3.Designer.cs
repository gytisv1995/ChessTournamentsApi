﻿// <auto-generated />
using ChessTournamentsApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace ChessTournamentsApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20171105193223_tableAdd3")]
    partial class tableAdd3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ChessTournamentsApi.Models.Player", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category");

                    b.Property<string>("Name");

                    b.Property<int>("Rating");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("ChessTournamentsApi.Models.Tournament", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Country");

                    b.Property<bool>("HasBegun");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("ChessTournamentsApi.Models.TournamentPlayer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("PlayerId");

                    b.Property<double>("Points");

                    b.Property<long>("TournamentId");

                    b.HasKey("Id");

                    b.ToTable("TournamentPlayers");
                });

            modelBuilder.Entity("ChessTournamentsApi.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password");

                    b.Property<string>("Role");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
