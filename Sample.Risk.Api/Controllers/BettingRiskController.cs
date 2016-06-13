namespace Sample.Risk.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Description;

    using Sample.Risk.Api.Services;
    using Sample.Risk.Common.Dto;
    using Sample.Risk.Common.ServiceContracts;

    public class BettingRiskController : ApiController
    {
        #region Instance Fields

        private readonly IBettingRiskService bettingRiskService = new BettingRiskService();

        #endregion

        #region Public Methods

        [Route("Api/BettingRisk/Customer")]
        [HttpGet]
        [ResponseType(typeof(List<List<CustomerDto>>))]
        public IHttpActionResult GetCustomers()
        {
            List<CustomerDto> result = this.bettingRiskService.GetCustomers();
            return this.Ok(result);
        }

        [Route("Api/BettingRisk/UnsettledBet")]
        [HttpGet]
        [ResponseType(typeof(List<List<UnsettledDto>>))]
        public IHttpActionResult GetSettledBets()
        {
            List<UnsettledDto> result = this.bettingRiskService.GetUpcomingBets();
            return this.Ok(result);
        }

        #endregion
    }
}