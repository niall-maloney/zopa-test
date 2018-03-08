using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using ZopaTest.Calculator;
using ZopaTest.Contracts;
using ZopaTest.Readers;

namespace ZopaTest.App
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var services = BuildDependencies();
            var app = services.GetRequiredService<IQuoteApp>();

            app.Start(args);
        }


        private static IServiceProvider BuildDependencies()
        {
            var serviceProvider = new ServiceCollection().AddSingleton<ILoggerFactory, LoggerFactory>()
                                                         .AddSingleton(typeof(ILogger<>), typeof(Logger<>))
                                                         .AddLogging(builder => builder.SetMinimumLevel(LogLevel.Trace))
                                                         .AddTransient<IQuoteApp, QuoteApp>()
                                                         .AddSingleton<IConfiguration>(new ConfigurationBuilder().AddJsonFile("config.json").Build())
                                                         .AddTransient<IArgumentValidator, ArgumentValidator>()
                                                         .AddTransient<IOffersReader, CsvOfferReader>()
                                                         .AddTransient<IQuoteCalculator, QuoteCalculator>()
                                                         .BuildServiceProvider();

            serviceProvider.GetRequiredService<ILoggerFactory>()
                           .AddNLog(new NLogProviderOptions {CaptureMessageTemplates = true, CaptureMessageProperties = true})
                           .ConfigureNLog("nlog.config");

            return serviceProvider;
        }
    }
}