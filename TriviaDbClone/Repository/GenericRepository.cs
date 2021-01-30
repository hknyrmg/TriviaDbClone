﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TriviaDbClone.Context;
using TriviaDbClone.Entities;

namespace TriviaDbClone.Repository
{
   
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region Variables

        private DbContext _context;

        private DbSet<T> _dbset;

        #endregion Variables

        #region Constructor

      
        public GenericRepository(DbContext context)
        {
            //Değişkenlere değer ataması yapıyoruz
            _context = context;
            _dbset = _context.Set<T>();
        }

        #endregion Constructor

        #region GetterMethods

       
        public IQueryable<T> GetAll()
        {
            
            return _dbset;
        }

    

        #endregion GetterMethods

        #region SetterMethods


        public T Update(T entity)
        {
            _dbset.Update(entity);
            return entity;
        }

        /// <summary>
        /// Gönderilen entity nesnesinin veritabanına eklenmesi için kuyruğa alan metot
        /// </summary>
        /// <param name="entity">Eklenmek istenen sınıfın örneği</param>
        public T Add(T entity)
        {
            _dbset.Add(entity);
            return entity;
        }


        public List<T> AddRange(List<T> entityList)
        {
            _dbset.AddRange(entityList);
            return entityList;
        }
        public void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Attach(entity);
            }
            _dbset.Remove(entity);
        }

        public T Find(int id)
        {
 
                return _context.Set<T>().Find(id);
           
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int executeSqlQuery(string sqlQuery, params object[] parameters)
        {
            return _context.Database.ExecuteSqlRaw(sqlQuery, parameters);
             
        }
        public int executeSqlQuery(string sqlQuery, IEnumerable<object> parameters)
        {

            return _context.Database.ExecuteSqlRaw(sqlQuery, parameters);
        }
  
        public int executeSqlQuery(string sqlQuery)
        {
          return _context.Database.ExecuteSqlRaw(sqlQuery);
            
        }

      



        #endregion SetterMethods
    }
}
