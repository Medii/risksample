namespace Sample.Risk.Api
{
    using System;

    using Microsoft.Owin.Hosting;

    public class Program
    {
        #region Private Methods

        private static void Main()
        {
            string baseAddress = "http://localhost:9000/";

            using (WebApp.Start<Startup>(baseAddress))
            {
                Console.WriteLine("Betting API service started...");
                Console.WriteLine("Available services: ");
                Console.WriteLine("http://localhost:9000/Api/BettingRisk/Customer/Api/BettingRisk/Customer");
                Console.WriteLine("http://localhost:9000/Api/BettingRisk/Customer/Api/BettingRisk/UnsettledBet");
                Console.WriteLine("Press enter to shutdown the service");
                Console.ReadLine();
                //HttpResponseMessage response = client.GetAsync(baseAddress + "Api/BettingRisk/Customer").Result;
                //Console.WriteLine(response);
                //Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }
        }

        #endregion
    }
}