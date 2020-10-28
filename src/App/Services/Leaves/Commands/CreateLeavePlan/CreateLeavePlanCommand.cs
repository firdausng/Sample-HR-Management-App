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
    public class CreateLeavePlanCommand : IRequest<CreatedDto>
    {
        public CreateLeavePlanCommand()
        {

        }
        public string Name { get; set; }
        public List<KeyValuePair<Guid, double>> Leaves { get; set; } = new List<KeyValuePair<Guid, double>>();
        public class CreateLeavePlanCommandHandler : IRequestHandler<CreateLeavePlanCommand, CreatedDto>
        {
            private readonly LeaveDbContext context;

            public CreateLeavePlanCommandHandler(LeaveDbContext context)
            {
                this.context = context;
            }

            public async Task<CreatedDto> Handle(CreateLeavePlanCommand request, CancellationToken cancellationToken)
            {

                var entity = new LeavePlan()
                {
                    Name = request.Name
                };

                var leaveTypeEntityList = await context
                    //.AsNoTracking()
                    .LeaveTypes
                    .ToListAsync();



                var leavePlanAssignmentList = request.Leaves.Select(l =>
                {
                    var leaveType = leaveTypeEntityList.SingleOrDefault(lt => lt.Id.Equals(l.Key));
                    if (leaveType == null)
                    {
                        throw new EntityNotFoundException(nameof(LeaveType), l.Key);
                    }
                    return new LeavePlanAssignment
                    {
                        LeaveType = leaveType,
                        Amount = l.Value,
                        LeavePlan = entity,
                    };
                });

                context.LeavePlanAssignments.AddRange(leavePlanAssignmentList);
                await context.SaveChangesAsync(cancellationToken);

                return new CreatedDto
                {
                    Id = entity.Id
                };
            }
        }
    }


}
