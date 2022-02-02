﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Tushino;

#nullable disable

namespace ParseTsgReplays.Migrations
{
    [DbContext(typeof(ReplaysContext))]
    partial class ReplaysContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Tushino.EnterExit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsEnter")
                        .HasColumnType("boolean");

                    b.Property<int>("ReplayId")
                        .HasColumnType("integer");

                    b.Property<int>("Time")
                        .HasColumnType("integer");

                    b.Property<int>("UnitId")
                        .HasColumnType("integer");

                    b.Property<string>("User")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("ReplayId");

                    b.ToTable("EnterExit");
                });

            modelBuilder.Entity("Tushino.Goal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsPlayer")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsUnconscious")
                        .HasColumnType("boolean");

                    b.Property<string>("Message")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("ReplayId")
                        .HasColumnType("integer");

                    b.Property<double>("Score")
                        .HasColumnType("double precision");

                    b.Property<int>("Time")
                        .HasColumnType("integer");

                    b.Property<int>("UnitId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ReplayId");

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("Tushino.Hit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Ammo")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<double>("Damage")
                        .HasColumnType("double precision");

                    b.Property<double>("Distance")
                        .HasColumnType("double precision");

                    b.Property<bool>("IsUnconscious")
                        .HasColumnType("boolean");

                    b.Property<string>("Magazine")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("ReplayId")
                        .HasColumnType("integer");

                    b.Property<int>("ShooterId")
                        .HasColumnType("integer");

                    b.Property<int?>("ShooterVehicleId")
                        .HasColumnType("integer");

                    b.Property<int>("TargetId")
                        .HasColumnType("integer");

                    b.Property<int>("Time")
                        .HasColumnType("integer");

                    b.Property<string>("Weapon")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("ReplayId");

                    b.ToTable("Hits");
                });

            modelBuilder.Entity("Tushino.Kill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("KillerId")
                        .HasColumnType("integer");

                    b.Property<int>("ReplayId")
                        .HasColumnType("integer");

                    b.Property<int>("TargetId")
                        .HasColumnType("integer");

                    b.Property<int>("Time")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ReplayId");

                    b.ToTable("Kills");
                });

            modelBuilder.Entity("Tushino.Medical", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Action")
                        .HasColumnType("text");

                    b.Property<bool>("IsPlayer")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsUnconscious")
                        .HasColumnType("boolean");

                    b.Property<int>("MedicId")
                        .HasColumnType("integer");

                    b.Property<int>("PatientId")
                        .HasColumnType("integer");

                    b.Property<int>("ReplayId")
                        .HasColumnType("integer");

                    b.Property<int>("Time")
                        .HasColumnType("integer");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("ReplayId");

                    b.ToTable("Medicals");
                });

            modelBuilder.Entity("Tushino.Replay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Admin")
                        .HasColumnType("text");

                    b.Property<string>("CommanderEast")
                        .HasColumnType("text");

                    b.Property<string>("CommanderGuer")
                        .HasColumnType("text");

                    b.Property<string>("CommanderWest")
                        .HasColumnType("text");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("boolean");

                    b.Property<string>("Island")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Mission")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("PlayTime")
                        .HasColumnType("integer");

                    b.Property<string>("Server")
                        .HasMaxLength(2)
                        .HasColumnType("character varying(2)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("WinnerSide")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Replays");
                });

            modelBuilder.Entity("Tushino.Unit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Class")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<double>("Damage")
                        .HasColumnType("double precision");

                    b.Property<string>("Icon")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("IsVehicle")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("ReplayId")
                        .HasColumnType("integer");

                    b.Property<int>("Side")
                        .HasColumnType("integer");

                    b.Property<string>("Squad")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("TimeInVehicle")
                        .HasColumnType("integer");

                    b.Property<int?>("TimeOfDeath")
                        .HasColumnType("integer");

                    b.Property<int>("TimeOnFoot")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int?>("VehicleOrDriverId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ReplayId");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("Tushino.EnterExit", b =>
                {
                    b.HasOne("Tushino.Replay", null)
                        .WithMany("Events")
                        .HasForeignKey("ReplayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tushino.Goal", b =>
                {
                    b.HasOne("Tushino.Replay", null)
                        .WithMany("Goals")
                        .HasForeignKey("ReplayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tushino.Hit", b =>
                {
                    b.HasOne("Tushino.Replay", null)
                        .WithMany("Hits")
                        .HasForeignKey("ReplayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tushino.Kill", b =>
                {
                    b.HasOne("Tushino.Replay", null)
                        .WithMany("Kills")
                        .HasForeignKey("ReplayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tushino.Medical", b =>
                {
                    b.HasOne("Tushino.Replay", null)
                        .WithMany("Medicals")
                        .HasForeignKey("ReplayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tushino.Unit", b =>
                {
                    b.HasOne("Tushino.Replay", null)
                        .WithMany("Units")
                        .HasForeignKey("ReplayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tushino.Replay", b =>
                {
                    b.Navigation("Events");

                    b.Navigation("Goals");

                    b.Navigation("Hits");

                    b.Navigation("Kills");

                    b.Navigation("Medicals");

                    b.Navigation("Units");
                });
#pragma warning restore 612, 618
        }
    }
}
