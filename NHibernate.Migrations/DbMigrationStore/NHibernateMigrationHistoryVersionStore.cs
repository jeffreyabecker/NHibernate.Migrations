using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;

namespace NHibernate.Migrations.DbMigrationStore
{
    public class NHibernateMigrationHistoryVersionStore : IMigrationVersionStore
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly string _context;

        public NHibernateMigrationHistoryVersionStore(ISessionFactory sessionFactory, string context)
        {
            _sessionFactory = sessionFactory;
            _context = context;
        }


        public IMigrationVersion CurrentVersion
        {
            get
            {
                using (var session = _sessionFactory.OpenStatelessSession())
                {
                    try
                    {
                        return session.Query<NHibernateMigrationHistory>()
                               .Where(h => h.Context == _context)
                               .FirstOrDefault();
                    }
                    catch (HibernateException)
                    {
                        return null;   
                    }
                    
                }
            }
            set
            {
                using (var session = _sessionFactory.OpenStatelessSession())
                using(var tx = session.BeginTransaction())
                {
                    session.CreateQuery("delete from NHibernateMigrationHistory where Context = :context")
                           .SetParameter("context", _context)
                           .ExecuteUpdate();
                    var newVersion = new NHibernateMigrationHistory(value.Version, value.Configuration, _context);
                    session.Insert(newVersion);
                    tx.Commit();
                }
            }
        }
    }
}