using NHibernate.SqlTypes;

namespace NHibernate.Migrations.Fluent.Builders
{
    public class FluentColumnModel
    {
        public virtual string Name { get; set; }
        public virtual string SqlType { get; set; }
        public virtual SqlType SqlTypeCode { get; set; }
        public virtual bool Nullable { get; set; }
        public virtual string DefaultValue { get; set; }
    }
}