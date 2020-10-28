using App.Services.Leaves;
using App.Services.Leaves.Common;
using Domain.Entities.LeaveEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace App.IntegrationTest
{
    using static SliceFixture;
    public abstract class IntegrationTestBase : IAsyncLifetime
    {
        private static readonly AsyncLock _mutex = new AsyncLock();

        private static bool _initialized;

        protected Guid[] resourceIdList = new Guid[] 
        {
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
        };

        public virtual Task DisposeAsync() => Task.CompletedTask;

        public virtual async Task InitializeAsync()
        {
            if (_initialized)
                return;

            using (await _mutex.LockAsync())
            {
                if (_initialized)
                    return;

                await SliceFixture.ResetCheckpoint();

                _initialized = true;
            }
        }

        protected async Task<CreatedDto> CreateLeavePlanAsync()
        {
            var lista = await GetLeaveTypeListAsync();
            var list = lista.Select(l => new KeyValuePair<Guid, double>(l.Id, 20)).ToList();

            var createLeavePlanCommand = new CreateLeavePlanCommand()
            {
                Name = "Leave1",
                Leaves = list
            };
            return await SendAsync(createLeavePlanCommand);
        }

        protected async Task<CreatedDto> CreateResourceLeaveAsync(Guid LeavePlanId, Guid resourceId)
        {

            var resourceLeave = await SendAsync(new CreateResourceLeaveCommand()
            {
                LeavePlanId = LeavePlanId,
                ResourceId = resourceId
            });
            return resourceLeave;
        }

        protected async Task<List<LeaveType>> GetLeaveTypeListAsync()
        {
            var list = await ExecuteDbContextAsync(db => db.LeaveTypes
                //.Include(l => l.LeavePlanAssignments)
                .ToListAsync());
            return list;
        }
    }
}
