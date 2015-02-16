using System.Collections.Generic;
using System.Xml;
using NHibernate.Cfg;
using NHibernate.DdlGen.Operations;

namespace NHibernate.Migrations.Generation
{
    public class MigrationGenerationArguments
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Context { get; set; }
        public string CodeNamespace { get; set; }
        public IEnumerable<IDdlOperation> Operations { get; set; }
        public Configuration Cfg { get; set; }
    }


}