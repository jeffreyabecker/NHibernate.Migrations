using System.Text;
using NHibernate.DdlGen.Operations;

namespace NHibernate.Migrations.Generation
{
    public class CSharpEnableForeignKeyConstratintDdlOperationGenerator : IDdlOperationGenerator
    {
        public void AppendOperation(StringBuilder sb, int index, IDdlOperation op)
        {
            sb.AppendLine("            builder.Run(new EnableForeignKeyConstratintDdlOperation());");
        }
    }
}