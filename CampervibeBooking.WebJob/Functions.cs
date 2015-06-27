using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace Campervibe.WebJob
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage([QueueTrigger("queue")] string message, TextWriter log)
        {
            log.WriteLine(message);
        }

        [NoAutomaticTrigger]
        public static void WriteToLog(TextWriter log)
        {
            //var log = new Campervibe.WebJob.Logging.SqlLog();
            //log.Add("Test log"); 
            Console.Out.Write("I'm alive");
            Console.WriteLine("I'm alive");
            System.Threading.Thread.Sleep(60000);
        }
    }
}
