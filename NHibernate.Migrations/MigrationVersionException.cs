using System;

namespace NHibernate.Migrations
{
    public class MigrationVersionException : HibernateException
    {
        public MigrationVersionException(string fmt, params object[] args) : base((string) String.Format(fmt, args))
        {
            
        }
    }
}