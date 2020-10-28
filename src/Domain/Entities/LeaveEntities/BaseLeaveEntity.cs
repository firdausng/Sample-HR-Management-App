using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.LeaveEntities
{
    public class BaseLeaveEntity: IBaseEntity
    {
        public Guid Id { get; set; }
    }
}
