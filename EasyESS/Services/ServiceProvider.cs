using EasyESS.Contracts;

namespace EasyESS.Services
{
    public static class ServiceProvider
    {
        public static void Install<TService>(InstallationInfo info) where TService : IService, new()
        {
            var service = new TService();
            service.ExtractFiles(info);
            service.FillConfig(info);
            if (service is IDataAccessable dataAccessable)
                dataAccessable.CreateDb(info);

            if (service is IHostable hostable)
                hostable.AddToIIS(info);

            if (service is IRegistrable registrable)
                registrable.Register(info);

            if (service is IConfigurable configurable)
                configurable.Configure(info);
        }
    }
}
