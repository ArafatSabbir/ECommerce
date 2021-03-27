using Microsoft.EntityFrameworkCore;
using NecessaryDrugs.Core.Contexts;
using NecessaryDrugs.Core.Entities;
using NecessaryDrugs.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace NecessaryDrugs.Core.Repositories
{
    public class MedicineCategoryRepository : Repository<MedicineCategory>, IMedicineCategoryRepository
    {
        public MedicineCategoryRepository(DbContext dbContext): base(dbContext)
        {
            
        }
    }
}
