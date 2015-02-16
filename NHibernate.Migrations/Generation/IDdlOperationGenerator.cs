using System.Text;
using NHibernate.DdlGen.Operations;

namespace NHibernate.Migrations.Generation
{
    public interface IDdlOperationGenerator
    {
        void AppendOperation(StringBuilder sb, int index, IDdlOperation operation);
    }
}