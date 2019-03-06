using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;


namespace Logging.Functions
{
    public class LogAllEvents 
    {
        [FunctionName("GetAllEvents")]
        public static async Task Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, ExecutionContext context)
        {
            
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile(@"appSettings.json", false, true)
                .Build();

            //--------------Logger Configuration using inline configuration  ---------------- 
            Log.Logger = new LoggerConfiguration()
                .WriteTo.AzureBlobStorage(" --- REPLACE WITH STORAGEACCOUNT BLOB CONNECTION STRING --- ",
                    LogEventLevel.Information,
                    null,
                    "{yyyy}/{MM}/{dd}/MyLogFile.txt")
                .CreateLogger();


            //--------------Logger Configuration using appSettings.json values ---------------- 
            //Log.Logger = new LoggerConfiguration()
            //    .ReadFrom.Configuration(configuration) // UPDATE appsettings.json WITH STORAGEACCOUNT BLOB CONNECTION STRING
            //    .CreateLogger();


            Log.Logger.Information("Hello World.");
        }
    }
}