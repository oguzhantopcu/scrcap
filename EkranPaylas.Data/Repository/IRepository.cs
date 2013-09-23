using System.Collections.Generic;

namespace EkranPaylas.Data.Repository
{
    public interface IRepository<T>
    {
        T GetById(string id);
    
        T Add(T entity);
      
        void Add(IEnumerable<T> entities);
       
        T Update(T entity);

        void Update(IEnumerable<T> entities);
     
        void Delete(string id);
     
        void Delete(T entity);
     
        void DeleteAll();
      
        long Count();
     }
}