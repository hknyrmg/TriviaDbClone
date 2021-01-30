using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TriviaDbClone.Repository
{

    public interface IGenericRepository<T> where T : class
    {

        IQueryable<T> GetAll();

        T Find(int id);

        T Add(T entity);

        List<T> AddRange(List<T> entityList);

        T Update(T entity);

        void Delete(int id);

        void Delete(T entity);

        int executeSqlQuery(string sqlQuery);
        int executeSqlQuery(string sqlQuery, params object[] parameters);
        int executeSqlQuery(string sqlQuery, IEnumerable<object> parameters);



    }
}
