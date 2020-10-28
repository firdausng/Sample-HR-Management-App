using App.Services.Leaves;
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
    public class ResourceLeaveTest : IntegrationTestBase
    {
        [Fact]
        public async Task ShouldCreateResourceLeave()
        {
            var leavePlanDto = await CreateLeavePlanAsync();

            var resourceLeave = await SendAsync(new CreateResourceLeaveCommand()
            {
                LeavePlanId = leavePlanDto.Id,
                ResourceId = resourceIdList[0]
            });

            var created = await ExecuteDbContextAsync(db => db.ResourceLeaves
                //.Include(l => l.LeavePlanAssignments)
                .SingleOrDefaultAsync(l => l.Id.Equals(resourceLeave.Id)));

            created.ShouldNotBeNull();
            created.Id.ShouldBe(resourceLeave.Id);
            created.ResourceId.ShouldBe(resourceIdList[0]);
            created.LeavePlanId.ShouldBe(leavePlanDto.Id);
        }
    }
}
