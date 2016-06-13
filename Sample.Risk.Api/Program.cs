namespace Sample.Risk.Api
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;

    using Microsoft.Owin.Hosting;

    using Newtonsoft.Json;

    using Sample.Risk.Common.Dto;

    public class Program
    {
        #region Private Methods

        private static void Main()
        {
            string baseAddress = "http://localhost:9000/";
            StartOptions options = new StartOptions(baseAddress) { ServerFactory = "Microsoft.Owin.Host.HttpListener" };
            options.Urls.Add("http://localhost:9000");
            options.Urls.Add("http://127.0.0.1:9000");
            //options.Urls.Add(string.Format("http://{0}:9000", Environment.MachineName));
            using (WebApp.Start<Startup>(options))
            {
                Console.WriteLine("Betting API service started...");
                Console.WriteLine("Available services: ");
                Console.WriteLine("http://localhost:9000/Api/BettingRisk/Customer/Api/BettingRisk/Customer");
                Console.WriteLine("http://localhost:9000/Api/BettingRisk/Customer/Api/BettingRisk/UnsettledBet");
                Console.WriteLine("Press enter to shutdown the service");
                Console.ReadLine();
            }
        }

        #endregion
    }
}