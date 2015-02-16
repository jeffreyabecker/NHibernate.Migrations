using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Migrations.Fluent;

namespace NHibernate.Migrations.Generation
{
    public class CSharpMigrationGenerator : IMigrationGenerator
    {
        private readonly IDictionary<System.Type, IDdlOperationGenerator> _operationGenerators = GetGenerators();

        private static IDictionary<System.Type, IDdlOperationGenerator> GetGenerators()
        {
            return new Dictionary<System.Type, IDdlOperationGenerator>
            {               
                
                //{typeof(NHibernate.DdlGen.Operations.AlterTableAddColumnDdlOperation), new CSharpAlterTableAddColumnDdlOperationGenerator()},
                //{typeof(NHibernate.DdlGen.Operations.AlterTableAlterColumnDdlOperation), new CSharpAlterTableAlterColumnDdlOperationGenerator()},
                {typeof(NHibernate.DdlGen.Operations.AlterTableDropColumnDdlOperation), new CSharpAlterTableDropColumnDdlOperationGenerator()},
                
                {typeof(NHibernate.DdlGen.Operations.CreateIndexDdlOperation), new CSharpCreateIndexDdlOperationGenerator()},                
                //{typeof(NHibernate.DdlGen.Operations.CreateTableDdlOperation), new CSharpCreateTableDdlOperationGenerator()},

                {typeof(NHibernate.DdlGen.Operations.AddTableCommentsDdlOperation), new CSharpAddTableCommentsDdlOperationGenerator()},
                {typeof(NHibernate.DdlGen.Operations.CreateForeignKeyOperation), new CSharpCreateForeignKeyOperationGenerator()},
                {typeof(NHibernate.DdlGen.Operations.CreateSequenceDdlOperation), new CSharpCreateSequenceDdlOperationGenerator()},
                {typeof(NHibernate.DdlGen.Operations.DisableForeignKeyConstraintDdlOperation), new CSharpDisableForeignKeyConstraintDdlOperationGenerator()},
                {typeof(NHibernate.DdlGen.Operations.DropForeignKeyDdlOperation), new CSharpDropForeignKeyDdlOperationGenerator()},
                {typeof(NHibernate.DdlGen.Operations.DropIndexDdlOperation), new CSharpDropIndexDdlOperationGenerator()},
                {typeof(NHibernate.DdlGen.Operations.DropSequenceDdlOperation), new CSharpDropSequenceDdlOperationGenerator()},
                {typeof(NHibernate.DdlGen.Operations.DropTableDdlOperation), new CSharpDropTableDdlOperationGenerator()},
                {typeof(NHibernate.DdlGen.Operations.EnableForeignKeyConstratintDdlOperation), new CSharpEnableForeignKeyConstratintDdlOperationGenerator()},
                
            };

        }

        public string GenerateMigration(MigrationGenerationArguments args)
        {
            var sb = new StringBuilder();
            sb.Append("using NHibernate.Cfg;\r\n");
            sb.Append("using NHibernate.Migrations.Fluent;\r\n");
            sb.Append("using NHibernate.Migrations.Fluent.Builders;\r\n");
            sb.Append("\r\n");
            sb.AppendFormat("namespace {0}\r\n", args.CodeNamespace);
            sb.Append("{\r\n");
            sb.AppendFormat("    public class {0} : FluentMigration\r\n",args.Name);
            sb.Append("    {\r\n");
            sb.AppendFormat("        private const string _configurationData = \"{0}\";", FluentMigration.SerializeConfiguration(args.Cfg));
            sb.AppendFormat("        public {0}()\r\n", args.Name);
            sb.AppendFormat("            : base(\"{0:s}_{1}\", \"{2}\")\r\n",DateTime.Now, args.Name, args.Context);
            sb.Append("        {\r\n");
            sb.Append("        }\r\n");
            sb.Append("\r\n");
            sb.Append("        protected override void BuildOperations(IDdlOperationBuilderSurface builder)\r\n");
            sb.Append("        {\r\n");

            var idx = 0;
            foreach (var op in args.Operations)
            {
                var operationType = op.GetType();
                if (!_operationGenerators.ContainsKey(operationType))
                {
                    throw new OperationGeneratorNotFoundException(operationType);
                }
                _operationGenerators[operationType].AppendOperation(sb, idx, op);
                idx++;
            }

            sb.Append("        }\r\n");
            sb.Append("\r\n");
            sb.Append("        protected override Configuration GetConfiguration()\r\n");
            sb.Append("        {\r\n");
            sb.Append("            return GetConfigurationFromCompressedBytes(_configurationData);\r\n");
            sb.Append("        }\r\n");
            sb.Append("    }\r\n");
            sb.Append("}\r\n");

            return sb.ToString();
        }
    }
}