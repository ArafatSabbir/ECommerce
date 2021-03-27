using Microsoft.EntityFrameworkCore;
using NecessaryDrugs.Core.Entities;
using NecessaryDrugs.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace NecessaryDrugs.Core.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
