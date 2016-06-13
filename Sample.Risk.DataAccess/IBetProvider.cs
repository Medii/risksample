namespace Sample.Risk.DataAccess
{
    using System.Collections.Generic;

    using Sample.Risk.Common.Dto;

    public interface IBetProvider
    {
        #region Public Methods

        List<UnsettledDto> GetUnsettledDtos();

        List<SettledDto> GetSettledDto();

        #endregion
    }
}