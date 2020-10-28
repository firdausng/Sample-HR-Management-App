using Domain.Entities.LeaveEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infra.Data.Seed
{
    public class DataSeed
    {
        private readonly LeaveDbContext context;
        private readonly ILogger<DataSeed> logger;

        public DataSeed(LeaveDbContext context, ILogger<DataSeed> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public void EnsureSeedData()
        {
            logger.LogInformation("Migrating db");
            context.Database.Migrate();

            //var defaultLeaveResourceGroup = context.LeaveResourceGroups.SingleOrDefault(lt => lt.Name.Equals("Default"));
            //if (defaultLeaveResourceGroup == null)
            //{

                    
            //}
        }
    }
}
