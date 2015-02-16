using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;

namespace NHibernate.Migrations.DbMigrationStore
{
    public class NHibernateMigrationHistoryMapping : ClassMapping<NHibernateMigrationHistory>
    {
        public NHibernateMigrationHistoryMapping(string catalog = null, string schema = null, string table = null)
        {
            if (catalog != null)
            {
                Catalog(catalog);
            }
            if (schema != null)
            {
                Schema(schema);
            }
            table = table ?? "NHibernateMigrationHistory"; // not using a default because the caller might pass null
            Table(table);
            ComposedId(m =>
            {
                m.Property(x=>x.Context, p=> p.Length(255));
                m.Property(x => x.Version, p => p.Length(255));
            });
            Property(x=>x.Configuration, m =>
            {
                m.Type<CompressedSerializableType>();
                m.NotNullable(true);
                m.Length(2147483647); // Make SqlServer use image/varbinary(max)
                
            });
        }

        
    }
}