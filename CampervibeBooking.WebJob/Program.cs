using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace Campervibe.WebJob
{
    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    public class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            var host = new JobHost();
            // The following code ensures that the WebJob will be running continuously
            Console.WriteLine("starting...");
            host.Call(typeof(Program).GetMethod("RunOnce"));

            //host.RunAndBlock();
        }

        [NoAutomaticTrigger]
        public static void RunOnce()
        {
            Console.WriteLine("RunOnce called...");
        }
    }
}
