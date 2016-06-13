namespace Sample.Risk.DataAccess.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    using NUnit.Framework;

    using Sample.Risk.Common.Dto;

    [TestFixture]
    public class CsvBetProviderTests
    {
        #region Public Methods

        [Test]
        public void GetSettledDtoTest()
        {
            CsvBetProvider betProvider = new CsvBetProvider(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestData"));
            List<SettledDto> result = betProvider.GetSettledDto();
            Assert.AreEqual(50, result.Count);
        }

        [Test]
        public void GetUnsettledDtoTest()
        {
            CsvBetProvider betProvider = new CsvBetProvider(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TestData"));
            List<UnsettledDto> result = betProvider.GetUnsettledDtos();
            Assert.AreEqual(22, result.Count);
        }

        #endregion
    }
}