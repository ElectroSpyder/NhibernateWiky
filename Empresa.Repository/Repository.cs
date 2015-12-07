using Empresa.Configuracion;
using NHibernate;
using NHibernate.Linq;  //usado para .Query<T>()
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresa.Repository
{
    //el Repositorio no sabe nada de la Configuracion
    public class Repository<T>: IRepository<T>
    {
        //interfaz de NHibernate
        //aplicando injection
        private readonly ISession Session;
        public Repository()
        {
            Session = SessionModulo.GetCurrentSession();
        }

        public IQueryable<T> GetAll()
        {
            return Session.Query<T>();
        }

        public IQueryable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> expresion)
        {
            return GetAll().Where(expresion);
        }

        public IEnumerable<T> SaveUpdateAll(params T[] entities)
        {
            foreach (var item in entities)
            {
                Session.SaveOrUpdate(item);
            }
            return entities;
        }

        public T SaveUpdate(T entity)
        {
            Session.SaveOrUpdate(entity);
            return entity;
        }
    }
}
