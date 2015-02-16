namespace NHibernate.Migrations.Generation
{
    public class OperationGeneratorNotFoundException : HibernateException
    {
        public OperationGeneratorNotFoundException(System.Type type)
            : base("Could not find a generator for " + type.FullName)
        {
            
        }
    }
}