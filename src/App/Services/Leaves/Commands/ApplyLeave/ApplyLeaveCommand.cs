using App.Exceptions;
using App.Services.Leaves.Common;
using Domain.Entities.LeaveEntities;
using Infra.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Services.Leaves
{
    public class ApplyLeaveCommand: IRequest<ApplyLeaveDto>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid LeaveTypeId { get; set; }
        public string Description { get; set; }
        public double TotalApplied { get; set; }
        public bool Emergency { get; set; }
        public Guid ResourceId { get; set; }

        public class ApplyLeaveCommandHandler : IRequestHandler<ApplyLeaveCommand, ApplyLeaveDto>
        {
            private readonly LeaveDbContext context;

            public ApplyLeaveCommandHandler(LeaveDbContext context)
            {
                this.context = context;
            }

            public async Task<ApplyLeaveDto> Handle(ApplyLeaveCommand request, CancellationToken cancellationToken)
            {
                var resourceLeaveEntity = await context
                    //.AsNoTracking()
                    .ResourceLeaves
                    .Include(r => r.ResourceLeaveAssignments)
                    .Include(r => r.LeavePlan)
                    .ThenInclude(l => l.LeavePlanAssignments)
                    .SingleOrDefaultAsync(lt => lt.ResourceId.Equals(request.ResourceId));

                if (resourceLeaveEntity == null)
                {
                    throw new EntityNotFoundException(nameof(ResourceLeave), request.ResourceId);
                }

                var leavePlanAssignment = resourceLeaveEntity.LeavePlan
                    .LeavePlanAssignments
                    .SingleOrDefault(l => l.LeaveTypeId.Equals(request.LeaveTypeId));

                if (leavePlanAssignment == null)
                {
                    throw new EntityNotAssignedException(nameof(LeaveType), request.LeaveTypeId, nameof(LeavePlanAssignment));
                }

                var resourceLeaveAssignment = resourceLeaveEntity.ResourceLeaveAssignments
                    .SingleOrDefault(rla => rla.LeavePlanAssignmentId.Equals(leavePlanAssignment.Id));

                if (resourceLeaveAssignment == null)
                {
                    throw new EntityNotAssignedException(nameof(leavePlanAssignment), leavePlanAssignment.Id, nameof(ResourceLeaveAssignment));
                }

                var remainingLeave = leavePlanAssignment.Amount - resourceLeaveAssignment.Amount;
                if (remainingLeave < 0)
                {
                    throw new LeaveNotEnoughException(request.TotalApplied, remainingLeave);
                }
                else
                {
                    resourceLeaveAssignment.Amount = remainingLeave;
                }

                var entity = new Leave
                {
                    Description = request.Description,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    ResourceId = request.ResourceId,
                    Status = LeaveStatus.Pending,
                    Emergency = request.Emergency,
                    LeaveTypeId = leavePlanAssignment.LeaveTypeId,
                };

                await context.Leaves.AddAsync(entity, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                return new ApplyLeaveDto
                {
                    Id = entity.Id,
                    RemainingLeaveCount = resourceLeaveAssignment.Amount
                };
            }
        }
    }

    public class ApplyLeaveDto: CreatedDto
    {
        public double RemainingLeaveCount { get; set; }
    }

    public class LeaveNotEnoughException : Exception, IAppException
    {
        public LeaveNotEnoughException(double appliedLeave, double remainingLeave)
            : base($"Leave not enough, remaining leave is {remainingLeave} but applied for {appliedLeave}")
        {
        }

    }
}
