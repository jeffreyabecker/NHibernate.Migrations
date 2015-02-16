using System;
using System.Linq;

namespace NHibernate.Migrations
{
  public class ActivatorMigrationFactory : IMigrationFactory
  {
    private IMigrationVersionStoreFactory _migrationVersionStoreFactory;

    public IMigration CreateMigration(System.Type type)
    {
      if (!type.GetInterfaces().Contains(typeof(IMigration)))
        return null;

      foreach (var parameterInfos in type.GetConstructors().Select(constructorInfo => constructorInfo.GetParameters()))
      {
        if (parameterInfos.Length == 1 && parameterInfos[0].ParameterType.IsAssignableFrom(typeof(IMigrationVersionStoreFactory)))
          return (IMigration)Activator.CreateInstance(type, this._migrationVersionStoreFactory);
        if (parameterInfos.Length == 0)
          return (IMigration)Activator.CreateInstance(type);
      }

      return null;
    }

    public void AttachMigrationVersionStoreFactory(IMigrationVersionStoreFactory migrationVersionStoreFactory)
    {
      _migrationVersionStoreFactory = migrationVersionStoreFactory;
    }
  }
}