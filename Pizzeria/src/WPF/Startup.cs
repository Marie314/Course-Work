using Caliburn.Micro;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pizzeria.Infrastructure.Repositories;
using Pizzeria.Infrastructure.Repositories.Options;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using Pizzeria.Infrastructure.Repositories.Options.Impl;
using WPF.Common.Factories;
using WPF.Common.Factories.Impl;
using WPF.Common.Models;
using WPF.ViewModels;

namespace WPF
{
    internal partial class Startup : BootstrapperBase
    {
        private IHost _host;

        public Startup()
        {
            Initialize();
        }

        public IConfiguration Configuration { get; set; }

        protected override void Configure()
        {
            base.Configure();

            _host = Host.CreateDefaultBuilder()
               .ConfigureAppConfiguration((context, builder) =>
               {
                   builder.SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString());
                   builder.AddJsonFile("appsettings.json", optional: true);
               })
               .ConfigureServices((context, services) =>
               {
                   Configuration = context.Configuration;
                   ConfigureServices(services);
               })
               .Build();
        }

        protected override object GetInstance(Type service, string key) => _host.Services.GetService(service);


        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<AuthWindowViewModel>();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetSection("ConnectionStrings:DefaultConnection").Value;

            services.AddSingleton<IRepositoryOptions>(new RepositoryOptions
            {
                ConnectionString = connectionString,
            });

            services.AddSingleton(new SaveImageOptions
            {
                ImagesFolderPath = Configuration.GetValue<string>("SaveImageOptions:ImagesFolderPath"),
                FileExtensionsFilter = Configuration.GetValue<string>("SaveImageOptions:FileExtensionsFilter"),
                MultiSelect = Configuration.GetValue<bool>("SaveImageOptions:MultiSelect")
            });

            services.AddHttpContextAccessor();

            services.Scan(scan => scan
            .FromAssemblies(Assembly.GetExecutingAssembly())
                .AddClasses(@class => @class.Where(t => t.Name.EndsWith("Model", StringComparison.OrdinalIgnoreCase)))
            .AsSelf()
                .AddClasses(@class => @class.Where(t => t.Name.EndsWith("View", StringComparison.OrdinalIgnoreCase)))
            .AsSelf());

            services.Scan(scan => scan
            .FromAssemblies(new []
            {
                Assembly.Load("Pizzeria.Infrastructure"),
                Assembly.Load("Pizzeria.Application"),
            })
                .AddClasses(@class => @class.Where(t => t.Name.EndsWith("Service", StringComparison.OrdinalIgnoreCase)))
            .AsImplementedInterfaces()
                .AddClasses(classes => classes.AssignableTo(typeof(IRepository<>)))
            .AsImplementedInterfaces());


            services.AddSingleton<IEventAggregator, EventAggregator>();
            services.AddSingleton<IWindowManager, WindowManager>();
            services.AddSingleton<IEventAggregator, EventAggregator>();
            services.AddSingleton<IViewModelFactory, ViewModelFactory>();
        }
    }
}
