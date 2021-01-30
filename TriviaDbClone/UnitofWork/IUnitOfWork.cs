using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TriviaDbClone.Repository;

namespace TriviaDbClone.UnitofWork
{
    public interface IUnitOfWork : IDisposable
    {

        IGenericRepository<T> GetRepository<T>() where T : class;

        bool BeginNewTransaction();

        bool RollBackTransaction();

        int SaveChanges();

    }
}
