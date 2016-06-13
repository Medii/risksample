namespace Sample.Risk.Api.Tests
{
    using System;
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

        [Test]
        public void ApiTest()
        {
            string baseAddress = "http://localhost:9000/";

            using (WebApp.Start<Startup>(baseAddress))
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.GetAsync(baseAddress + "Api/BettingRisk/Customer").Result;
                List<CustomerDto> resultCol = JsonConvert.DeserializeObject<List<CustomerDto>>(response.Content.ReadAsStringAsync().Result);
                Assert.IsNotNull(resultCol);
                Assert.AreEqual(6, resultCol.Count);
            }
        }

        #endregion
    }
}