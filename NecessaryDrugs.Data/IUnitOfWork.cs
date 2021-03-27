using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace NecessaryDrugs.Data
{
    public interface IUnitOfWork<T> where T:DbContext
    {
        void Save();
    }
}
