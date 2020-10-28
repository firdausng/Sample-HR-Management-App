﻿// <auto-generated />
using System;
using Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infra.Migrations
{
    [DbContext(typeof(LeaveDbContext))]
    partial class LeaveDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Domain.Entities.LeaveEntities.Leave", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("Emergency")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("LeaveTypeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ResourceId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ResourceLeaveId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("StatusReason")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("LeaveTypeId");

                    b.HasIndex("ResourceLeaveId");

                    b.ToTable("Leaves");
                });

            modelBuilder.Entity("Domain.Entities.LeaveEntities.LeavePlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("LeavePlans");
                });

            modelBuilder.Entity("Domain.Entities.LeaveEntities.LeavePlanAssignment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<Guid>("LeavePlanId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("LeaveTypeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("LeavePlanId");

                    b.HasIndex("LeaveTypeId");

                    b.ToTable("LeavePlanAssignments");
                });

            modelBuilder.Entity("Domain.Entities.LeaveEntities.LeaveType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("LeaveTypes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ea948a1d-1579-4651-8fe3-4632b1c41dcc"),
                            Name = "Annual"
                        },
                        new
                        {
                            Id = new Guid("94911d50-f55e-4641-ad16-e61311059b7f"),
                            Name = "Sick"
                        },
                        new
                        {
                            Id = new Guid("1e6236d5-04a9-4651-9ec9-a9f9cb9a6a75"),
                            Name = "Paternity"
                        },
                        new
                        {
                            Id = new Guid("eea4be83-e41a-4c59-a78e-d94be7768909"),
                            Name = "Maternity"
                        },
                        new
                        {
                            Id = new Guid("693ffd51-6030-41eb-90bd-a38c736f9b98"),
                            Name = "ChildCare"
                        });
                });

            modelBuilder.Entity("Domain.Entities.LeaveEntities.ResourceLeave", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("LeavePlanId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ResourceId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasAlternateKey("ResourceId");

                    b.HasIndex("LeavePlanId");

                    b.ToTable("ResourceLeaves");
                });

            modelBuilder.Entity("Domain.Entities.LeaveEntities.ResourceLeaveAssignment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<Guid>("LeavePlanAssignmentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("LeaveTypeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ResourceLeaveId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("LeavePlanAssignmentId");

                    b.HasIndex("LeaveTypeId");

                    b.HasIndex("ResourceLeaveId");

                    b.ToTable("ResourceLeaveAssignments");
                });

            modelBuilder.Entity("Domain.Entities.LeaveEntities.Leave", b =>
                {
                    b.HasOne("Domain.Entities.LeaveEntities.LeaveType", "LeaveType")
                        .WithMany("Leaves")
                        .HasForeignKey("LeaveTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.LeaveEntities.ResourceLeave", null)
                        .WithMany("Leaves")
                        .HasForeignKey("ResourceLeaveId");
                });

            modelBuilder.Entity("Domain.Entities.LeaveEntities.LeavePlanAssignment", b =>
                {
                    b.HasOne("Domain.Entities.LeaveEntities.LeavePlan", "LeavePlan")
                        .WithMany("LeavePlanAssignments")
                        .HasForeignKey("LeavePlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.LeaveEntities.LeaveType", "LeaveType")
                        .WithMany()
                        .HasForeignKey("LeaveTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.LeaveEntities.ResourceLeave", b =>
                {
                    b.HasOne("Domain.Entities.LeaveEntities.LeavePlan", "LeavePlan")
                        .WithMany("ResourceLeaves")
                        .HasForeignKey("LeavePlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.LeaveEntities.ResourceLeaveAssignment", b =>
                {
                    b.HasOne("Domain.Entities.LeaveEntities.LeavePlanAssignment", "LeavePlanAssignment")
                        .WithMany()
                        .HasForeignKey("LeavePlanAssignmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.LeaveEntities.LeaveType", "LeaveType")
                        .WithMany()
                        .HasForeignKey("LeaveTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.LeaveEntities.ResourceLeave", "ResourceLeave")
                        .WithMany("ResourceLeaveAssignments")
                        .HasForeignKey("ResourceLeaveId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
