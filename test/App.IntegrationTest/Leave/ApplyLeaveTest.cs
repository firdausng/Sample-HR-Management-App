using App.Services.Leaves;
using Domain.Entities.LeaveEntities;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace App.IntegrationTest.Leave
{
    using static SliceFixture;
    [Collection("Sequential")]
    public class ApplyLeaveTest : IntegrationTestBase
    {
        [Fact]
        public async Task ShouldApplyLeave()
        {
            var leavePlanDto = await CreateLeavePlanAsync();
            var resourceLeave = await CreateResourceLeaveAsync(leavePlanDto.Id, resourceIdList[0]);
            var leaveTypeList = await GetLeaveTypeListAsync();

            var command = new ApplyLeaveCommand()
            {
                ResourceId = resourceIdList[0],
                Description = "test leave",
                Emergency = false,
                LeaveTypeId = leaveTypeList.Single(l => l.Name.Equals("Annual")).Id,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(3),
                TotalApplied = 3
            };
            var leave = await SendAsync(command);

            var created = await ExecuteDbContextAsync(db => db.Leaves
                //.Include(l => l.LeavePlanAssignments)
                .SingleOrDefaultAsync(l => l.Id.Equals(leave.Id)));

            created.ShouldNotBeNull();
            created.Id.ShouldBe(leave.Id);
            created.ResourceId.ShouldBe(resourceIdList[0]);
            created.LeaveTypeId.ShouldBe(leaveTypeList.Single(l => l.Name.Equals("Annual")).Id);
            created.StartDate.Equals(command.StartDate);
            created.EndDate.Equals(command.EndDate);
            created.Status.Equals(LeaveStatus.Pending);
            created.Description.Equals(command.Description);
            created.StatusReason.ShouldBeNull();

            var resourceLeaveAssignmentsEntity = await ExecuteDbContextAsync(db => 
                db.ResourceLeaveAssignments
                    .SingleOrDefaultAsync(l => l.LeaveType.Name.Equals("Annual"))
                );

            leave.RemainingLeaveCount.ShouldBe(resourceLeaveAssignmentsEntity.Amount);
        }
    }
}
