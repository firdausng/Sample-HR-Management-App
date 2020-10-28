using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.LeaveEntities
{
    public class Leave: BaseLeaveEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }       
        public string Description { get; set; }
        public bool Emergency { get; set; }
        public Guid ResourceId { get; set; }
        public LeaveStatus Status { get; set; }
        public string StatusReason { get; set; }
        public LeaveType LeaveType { get; set; }
        public Guid LeaveTypeId { get; set; }
    }


    public enum LeaveStatus
    {
        Denied,
        Approved,
        Pending
    }

    public class ResourceLeave : BaseLeaveEntity
    {
        public Guid ResourceId { get; set; }
        public LeavePlan LeavePlan { get; set; }
        public Guid LeavePlanId { get; set; }
        public ICollection<Leave> Leaves { get; set; } = new List<Leave>();
        public ICollection<ResourceLeaveAssignment> ResourceLeaveAssignments { get; set; } = new List<ResourceLeaveAssignment>();
    }

    public class ResourceLeaveAssignment : BaseLeaveEntity
    {
        public double Amount { get; set; }
        public Guid ResourceLeaveId { get; set; }
        public ResourceLeave ResourceLeave { get; set; }
        public Guid LeaveTypeId { get; set; }
        public LeaveType LeaveType { get; set; }
        public Guid LeavePlanAssignmentId { get; set; }
        public LeavePlanAssignment LeavePlanAssignment { get; set; }
    }

    public class LeaveType : BaseLeaveEntity
    {
        public string Name { get; set; }
        public ICollection<Leave> Leaves { get; set; } = new List<Leave>();
    }

    public class LeavePlan : BaseLeaveEntity
    {
        public string Name { get; set; }
        public ICollection<LeavePlanAssignment> LeavePlanAssignments { get; set; } = new List<LeavePlanAssignment>();
        public ICollection<ResourceLeave> ResourceLeaves { get; set; } = new List<ResourceLeave>();
    }

    public class LeavePlanAssignment : BaseLeaveEntity
    {
        public double Amount { get; set; }
        public Guid LeavePlanId { get; set; }
        public LeavePlan LeavePlan { get; set; }
        public Guid LeaveTypeId { get; set; }
        public LeaveType LeaveType { get; set; }
    }

    
}
