namespace Sample.Risk.Common.ServiceContracts
{
    using System.Collections.Generic;

    using Sample.Risk.Common.Dto;

    public interface IBettingRiskService
    {
        #region Public Methods

        List<SettledDto> GetCustomerSettledBetts(int CustomerId);

        List<CustomerDto> GetCustomers();

        List<UnsettledDto> GetUpcomingBetts();

        #endregion
    }
}