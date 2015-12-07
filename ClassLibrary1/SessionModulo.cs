using System.Web;
using NHibernate;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Automapping;
using NHibernate.Cfg;
using FluentNHibernate.Cfg;
using NHibernate.Context;
using System;
using FluentNHibernate.Conventions.Helpers;
using NHibernate.Tool.hbm2ddl;

namespace Empresa.Configuracion
{
    //agrear en referencias la dll System.Web para IHttpModule
    public class SessionModulo : IHttpModule
    {
        static readonly ISessionFactory sessionFactory;

        static SessionModulo()
        {
            sessionFactory = CreateSessionFactory();
        }

        public void Dispose()
        {

        }

        /// <summary>
        /// Inicializa el modulo Http
        /// </summary>
        /// <param name="context"></param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += BeginRequest;
            context.EndRequest += EndRequest;
        }

        /// <summary>
        /// Retorna el Session Factory
        /// </summary>
        /// <returns></returns>
        public static ISession GetCurrentSession()
        {
            return sessionFactory.GetCurrentSession();
        }

        private static void BeginRequest(object sender, EventArgs e)
        {
            ISession session = sessionFactory.OpenSession();
            session.BeginTransaction();
            CurrentSessionContext.Bind(session);
        }

        /// <summary>
        /// <configuration>
        //  <system.web>
        //    <httpModules>
        //      <add name="NHibernateSessionPerRequest" type="NhibernateEmpresa.Empresa.Configuracion.SessionModulo" />
        //     </httpModules>
        //  </system.web>
        //</configuration>
        //        <connectionStrings>
        //  <add name="testConn" connectionString="Server=.\SQLEXPRESS;Database=DBEmpresa providerName="System.Data.SqlClient" />
        //</connectionStrings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void EndRequest(object sender, EventArgs e)
        {
            ISession session = CurrentSessionContext.Unbind(sessionFactory);
            if (session == null) return;
            try
            {
                session.Transaction.Commit();
            }
            catch (Exception)
            {
                session.Transaction.Rollback();
            }
            finally
            {
                session.Close();
                session.Dispose();
            }
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(CreateDbConfiguration)
                .Mappings(m => m.AutoMappings.Add(CreateMapping()))
                .ExposeConfiguration(UpdatreSchema)
                .CurrentSessionContext<WebSessionContext>()
                .BuildSessionFactory();
        }

        /// <summary>
        /// Crear la cadena de coneccion
        /// </summary>
        /// <returns>Cadena de coneccion</returns>
        private static MsSqlConfiguration CreateDbConfiguration()
        {
            return MsSqlConfiguration
                .MsSql2008
                .ConnectionString(c => c.FromConnectionStringWithKey("db"));

        }

        /// <summary>
        /// Crear el Mapeo
        /// </summary>
        /// <returns>Map</returns>
        private static AutoPersistenceModel CreateMapping()
        {
            return AutoMap
                .Assembly(System.Reflection.Assembly.GetCallingAssembly())
                .Where(t => t.Namespace != null && t.Namespace.EndsWith("Models"))
                .Conventions.Setup(c => c.Add(DefaultCascade.SaveUpdate()));
        }

        private static void UpdatreSchema(Configuration cfg)
        {
            new SchemaUpdate(cfg)
            .Execute(false, true);
        }
    }
}
