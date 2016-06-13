namespace Sample.Risk.Common.ServiceContracts
{
    using System.Collections.Generic;

    using Sample.Risk.Common.Dto;

    public interface IBettingRiskService
    {
        #region Public Methods

        /// <summary>
        ///     Todo:
        /// </summary>
        /// <returns></returns>
        //List<SettledDto> GetSettledBets();
        List<CustomerDto> GetCustomers();

        List<UnsettledDto> GetUpcomingBets();

        #endregion
    }
}