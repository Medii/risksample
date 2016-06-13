namespace Sample.Risk.Api.Tests
{
    using System.Collections.Generic;
    using System.Net.Http;

    using Microsoft.Owin.Hosting;

    using Newtonsoft.Json;

    using NUnit.Framework;

    using Sample.Risk.Common.Dto;

    [TestFixture]
    public class IntegrationTest
    {
        #region Public Methods

        [Explicit]
        [Test]
        public void ApiTest()
        {
            string baseAddress = "http://localhost:9000/";
            StartOptions options = new StartOptions(baseAddress) { ServerFactory = "Microsoft.Owin.Host.HttpListener" };
            options.Urls.Add("http://localhost:9000");
            options.Urls.Add("http://127.0.0.1:9000");
            using (WebApp.Start<Startup>(baseAddress))
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.GetAsync(baseAddress + "Api/BettingRisk/Customer").Result;
                List<CustomerDto> customers = JsonConvert.DeserializeObject<List<CustomerDto>>(response.Content.ReadAsStringAsync().Result);
                Assert.IsNotNull(customers);
                Assert.AreEqual(6, customers.Count);

                response = new HttpClient().GetAsync(baseAddress + "Api/BettingRisk/UnsettledBet").Result;
                List<UnsettledDto> unsettled = JsonConvert.DeserializeObject<List<UnsettledDto>>(response.Content.ReadAsStringAsync().Result);

                Assert.AreEqual(22, unsettled.Count);
            }
        }

        #endregion
    }
}