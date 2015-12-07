using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Empresa.Repository
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(
            Expression<Func<T,bool>> expresion);
        IEnumerable<T> SaveUpdateAll(params T[] entities);
        T SaveUpdate(T entity);
    }
}
