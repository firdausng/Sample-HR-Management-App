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
    public class CreateResourceLeaveCommand : IRequest<CreatedDto>
    {
        public Guid ResourceId { get; set; }
        public Guid LeavePlanId { get; set; }
        public class CreateResourceLeaveHandler : IRequestHandler<CreateResourceLeaveCommand, CreatedDto>
        {
            private readonly LeaveDbContext context;

            public CreateResourceLeaveHandler(LeaveDbContext context)
            {
                this.context = context;
            }

            public async Task<CreatedDto> Handle(CreateResourceLeaveCommand request, CancellationToken cancellationToken)
            {
                var resourceLeaveEntity = await context
                    //.AsNoTracking()
                    .ResourceLeaves.SingleOrDefaultAsync(lt => lt.Id.Equals(request.ResourceId));

                if (resourceLeaveEntity != null)
                {
                    throw new EntityAlreadyExistException(nameof(ResourceLeave), request.ResourceId);
                }

                var leavePlansEntity = await context
                    //.AsNoTracking()
                    .LeavePlans
                    .Include(l => l.LeavePlanAssignments)
                    .SingleOrDefaultAsync(lt => lt.Id.Equals(request.LeavePlanId));

                if (leavePlansEntity == null)
                {
                    throw new EntityNotFoundException(nameof(LeavePlan), request.LeavePlanId);
                }

                var entity = new ResourceLeave()
                {
                    ResourceId = request.ResourceId,
                    LeavePlan = leavePlansEntity,
                };

                var resourceLeaveAssignmentList = leavePlansEntity.LeavePlanAssignments
                    .Select(lpa => new ResourceLeaveAssignment 
                    { 
                        Amount = lpa.Amount,
                        LeavePlanAssignment = lpa,
                        ResourceLeave = entity,
                        LeaveTypeId = lpa.LeaveTypeId
                    })
                    .ToList();

                entity.ResourceLeaveAssignments = resourceLeaveAssignmentList;


                context.ResourceLeaves.Add(entity);
                await context.SaveChangesAsync(cancellationToken);

                return new CreatedDto
                {
                    Id = entity.Id
                };

            }
        }
    }
}
