namespace Sample.Risk.Domain
{
    using System.Collections.Generic;
    using System.Linq;

    using Sample.Risk.Common.Dto;
    using Sample.Risk.DataAccess;

    public class RiskBal
    {
        #region Instance Fields

        private readonly IBetProvider betProvider;

        private readonly int MaximumStake = 1000;

        private readonly double unusalPercentage = 0.6;

        private readonly decimal UnusualFactor = 10;

        private readonly decimal UnusualHighlyUnusualFactor = 30;

        #endregion

        #region Constructors

        public RiskBal(IBetProvider betProvider)
        {
            this.betProvider = betProvider;
        }

        #endregion

        #region Public Methods

        public Dictionary<int, CustomerDto> GetCustomers()
        {
            List<SettledDto> settledDtos = this.betProvider.GetSettledDto();

            Dictionary<int, CustomerDto> customers = settledDtos.GroupBy(p => p.Punter.Id).ToDictionary(p => p.Key, p => new CustomerDto { Id = p.Key, AverageStake = (decimal)p.Average(q => q.Stake), UnusuallyWins = p.Count(q => q.Win > 0) / p.Count() > this.unusalPercentage });
            return customers;
        }

        public List<UnsettledDto> GetUpcomingBets(Dictionary<int, CustomerDto> customerDtos)
        {
            List<UnsettledDto> unsettledDtos = this.betProvider.GetUnsettledDtos();
            foreach (UnsettledDto unsettledDto in unsettledDtos)
            {
                if (customerDtos.ContainsKey(unsettledDto.Punter.Id))
                {
                    unsettledDto.Punter = customerDtos[unsettledDto.Punter.Id];
                }
                if (unsettledDto.Punter.UnusuallyWins)
                {
                    unsettledDto.RiskType = unsettledDto.RiskType | RiskTypes.Risky;
                }
                if (unsettledDto.ToWin > this.UnusualFactor * unsettledDto.Punter.AverageStake)
                {
                    unsettledDto.RiskType = unsettledDto.RiskType | RiskTypes.Unusual;
                }
                if (unsettledDto.ToWin > this.UnusualHighlyUnusualFactor * unsettledDto.Punter.AverageStake)
                {
                    unsettledDto.RiskType = unsettledDto.RiskType | RiskTypes.HighlyUnusual;
                }
                if (unsettledDto.ToWin > this.MaximumStake)
                {
                    unsettledDto.RiskType = unsettledDto.RiskType | RiskTypes.StakesGreatherThanThousand;
                }
            }
            return unsettledDtos;
        }

        #endregion
    }
}