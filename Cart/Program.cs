using System.Text;
using CartDaoApp.Dao.FileDao;
using CartDaoApp.Dao.Interface;
using CartDaoApp.Dao.MemoryDao;
using CartDaoApp.Services;
using log4net;
using log4net.Config;
using Microsoft.Extensions.DependencyInjection;

[assembly: XmlConfigurator(ConfigFile = "Configs/log4net.config", Watch = true)]

namespace CartDaoApp
{
    internal static class Program
    {
        public static void Main()
        {
            // To support text encoding in the console
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // Створення сервіс-контейнера
            var services = new ServiceCollection();
            ConfigureServices(services);

            // Створення постачальника сервісів
            var serviceProvider = services.BuildServiceProvider();

            // Отримання екземпляра Solution і виконання методу Run
            var solution = serviceProvider.GetRequiredService<Solution>();

            solution.Run();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Реєстрація залежностей
            //services.AddSingleton<IProductDao, MemoryProductDao>();
            //services.AddSingleton<ICartDao, MemoryCartDao>();
            //services.AddSingleton<ICartItemDao, MemoryCartItemDao>();
            //services.AddSingleton<IOrderDao, MemoryOrderDao>();

            services.AddSingleton<IProductDao, FileProductDao>();
            services.AddSingleton<ICartDao, FileCartDao>();
            services.AddSingleton<ICartItemDao, FileCartItemDao>();
            services.AddSingleton<IOrderDao,FileOrderDao>();

            // Реєстрація сервісів
            services.AddTransient<ProductService>();
            services.AddTransient<CartService>();
            services.AddTransient<OrderService>();

            // Реєстрація логера
            services.AddSingleton<ILog>(_ => LogManager.GetLogger("mylog"));

            services.AddTransient<Solution>();
        }
    }
}
