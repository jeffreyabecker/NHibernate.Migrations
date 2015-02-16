using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using NHibernate.Cfg;
using NHibernate.DdlGen.Operations;
using NHibernate.Migrations.Fluent.Builders;

namespace NHibernate.Migrations.Fluent
{
    public abstract class FluentMigration : IMigration
    {

        private readonly Lazy<IMigrationVersion> _migrationVersion;
        protected FluentMigration(string version, string context)
        {
            _migrationVersion = new Lazy<IMigrationVersion>(()=> new MigrationVersion(version, GetConfiguration(), context));
        }

        public IEnumerable<IDdlOperation> GetOperations()
        {
            var builder = new FluentBuilder();
            BuildOperations(builder);
            return builder.GeneratedOperations;
        }

        protected abstract void BuildOperations(IDdlOperationBuilderSurface builder);
        protected abstract Configuration GetConfiguration();

        public static Configuration GetConfigurationFromCompressedBytes(string base64GzipedConfig)
        {
            var bytes = Convert.FromBase64String(base64GzipedConfig);
            using (var stream = new GZipStream(new MemoryStream(bytes), CompressionMode.Decompress))
            {
                var bf = new BinaryFormatter();
                return (Configuration)bf.Deserialize(stream);
            }
        }

        public static string SerializeConfiguration(Configuration cfg)
        {
            var bf = new BinaryFormatter();
            var stream = new MemoryStream();
            var gz = new GZipStream(stream, CompressionMode.Compress, true);
            bf.Serialize(gz, cfg);
            gz.Flush();
            gz.Close();
            return Convert.ToBase64String(stream.ToArray());
        }

        public IMigrationVersion GetVersion()
        {
            return _migrationVersion.Value;
        }

        private class MigrationVersion : IMigrationVersion
        {
            public MigrationVersion(string version, Cfg.Configuration configuration, string context)
            {
                Context = context;
                Version = version;
                Configuration = configuration;
            }

            public string Context { get; private set; }
            public string Version { get; private set; }
            public Cfg.Configuration Configuration { get; private set; }
        }
    }
}
