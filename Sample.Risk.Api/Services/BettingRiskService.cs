namespace Sample.Risk.Api.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using Sample.Risk.Common.Dto;
    using Sample.Risk.Common.ServiceContracts;
    using Sample.Risk.DataAccess;
    using Sample.Risk.Domain;

    public class BettingRiskService : IBettingRiskService
    {
        #region Instance Fields

        private readonly string cvsFolderPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Csv");

        #endregion

        #region IBettingRiskService Members

        public List<CustomerDto> GetCustomers()
        {
            return new BettingRiskBal(new CsvBetProvider(this.cvsFolderPath)).GetCustomers().Values.ToList();
        }

        public List<UnsettledDto> GetUpcomingBets()
        {
            return new BettingRiskBal(new CsvBetProvider(this.cvsFolderPath)).GetUpcomingBets().ToList();
        }

        #endregion
    }
}