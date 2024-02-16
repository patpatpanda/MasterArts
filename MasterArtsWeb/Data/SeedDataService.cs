//using MasterArtsWeb.Data;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using System;
//using System.Threading;
//using System.Threading.Tasks;

//public class SeedDataService : IHostedService
//{
//    private readonly IServiceProvider _serviceProvider;

//    public SeedDataService(IServiceProvider serviceProvider)
//    {
//        _serviceProvider = serviceProvider;
//    }

//    public Task StartAsync(CancellationToken cancellationToken)
//    {
//        using (var scope = _serviceProvider.CreateScope())
//        {
//            var initializer = scope.ServiceProvider.GetRequiredService<DataInitializer>();
//            initializer.SeedData();
//        }
//        return Task.CompletedTask;
//    }

//    public Task StopAsync(CancellationToken cancellationToken)
//    {
//        // Inget att göra här
//        return Task.CompletedTask;
//    }
//}
