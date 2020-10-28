using Domain.Entities.LeaveEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Data
{
    public class LeaveDbContext : DbContext
    {
        public LeaveDbContext(DbContextOptions<LeaveDbContext> options) : base(options)
        {

        }

        public DbSet<Leave> Leaves { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<ResourceLeave> ResourceLeaves { get; set; }
        public DbSet<ResourceLeaveAssignment> ResourceLeaveAssignments { get; set; }
        public DbSet<LeavePlan> LeavePlans { get; set; }
        public DbSet<LeavePlanAssignment> LeavePlanAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<ResourceLeave>()
                .HasAlternateKey(r => r.ResourceId);

            modelBuilder.Entity<LeaveType>().HasData(
                new LeaveType { Name = "Annual", Id= Guid.NewGuid() },
                new LeaveType { Name = "Sick", Id = Guid.NewGuid() },
                new LeaveType { Name = "Paternity", Id = Guid.NewGuid() },
                new LeaveType { Name = "Maternity", Id = Guid.NewGuid() },
                new LeaveType { Name = "ChildCare", Id = Guid.NewGuid() }
                );
        }
    }
}
