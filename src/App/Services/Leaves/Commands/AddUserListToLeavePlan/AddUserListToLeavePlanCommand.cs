using App.Exceptions;
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

namespace App.Services.Leaves.Commands.AddUserListToLeavePlan
{
    public class AddUserListToLeavePlanCommand : IRequest<AddUserListToLeavePlanDto>
    {
        public Guid LeavePlanId { get; set; }
        public Guid ResourceId { get; set; }
        public class AddUserListToLeavePlanCommandHandler : IRequestHandler<AddUserListToLeavePlanCommand, AddUserListToLeavePlanDto>
        {
            private readonly LeaveDbContext context;

            public AddUserListToLeavePlanCommandHandler(LeaveDbContext context)
            {
                this.context = context;
            }

            public async Task<AddUserListToLeavePlanDto> Handle(AddUserListToLeavePlanCommand request, CancellationToken cancellationToken)
            {
                var leavePlanEntity = await context
                    //.AsNoTracking()
                    .LeavePlans.SingleOrDefaultAsync(lt => lt.Id.Equals(request.LeavePlanId));

                if (leavePlanEntity == null)
                {
                    throw new EntityNotFoundException(nameof(LeavePlan), request.LeavePlanId);
                }

                var resourceLeaveEntity = await context
                    //.AsNoTracking()
                    .ResourceLeaves.SingleOrDefaultAsync(lt => lt.ResourceId.Equals(request.ResourceId));

                if (resourceLeaveEntity == null)
                {
                    throw new EntityNotFoundException(nameof(ResourceLeave), request.ResourceId);
                }
                
                leavePlanEntity.ResourceLeaves.Add(resourceLeaveEntity);

                context.LeavePlans.Update(leavePlanEntity);
                await context.SaveChangesAsync(cancellationToken);



                return new AddUserListToLeavePlanDto
                {
                };

            }
        }
    }

    public class AddUserListToLeavePlanDto
    {

    }
}
