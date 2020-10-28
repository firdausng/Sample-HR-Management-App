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
    public class LeavePlanTest: IntegrationTestBase
    {
        [Fact]
        public async Task ShouldCreateLeavePlan()
        {
            var leaveTypelist = await GetLeaveTypeListAsync();
            //var leaveTypelist = lista.Select(l => new KeyValuePair<Guid, double>(l, 20)).ToList();

            var leaveTypeKVPlist = leaveTypelist.Select(l => new KeyValuePair<Guid, double>(l.Id, 20)).ToList();

            var leavePlan = await SendAsync(new CreateLeavePlanCommand()
            {
                Name = "Leave1",
                Leaves = leaveTypeKVPlist
            });

            leavePlan.ShouldNotBeNull();

            var created = await ExecuteDbContextAsync(db => db.LeavePlans
            .Include(l => l.LeavePlanAssignments)
            .Where(l => l.Id.Equals(leavePlan.Id))
            .SingleOrDefaultAsync());

            created.ShouldNotBeNull();
            created.Id.ShouldBe(leavePlan.Id);
            created.Name.ShouldBe("Leave1");
            created.LeavePlanAssignments.Count.ShouldBe(leaveTypelist.Count);
        }
    }
}
