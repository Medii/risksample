namespace Sample.Risk.Domain.Tests
{
    using System.Collections.Generic;

    using Moq;

    using NUnit.Framework;

    using Sample.Risk.Common.Dto;
    using Sample.Risk.DataAccess;

    [TestFixture]
    public class RiskBalTests
    {
        #region Instance Fields

        private List<SettledDto> settledDtos;

        #endregion

        #region Public Methods

        [SetUp]
        public void InitializedData()
        {
            this.settledDtos = new List<SettledDto>();
            this.settledDtos.Add(new SettledDto { Punter = new CustomerDto { Id = 1 }, Stake = 20, Win = 100, EventId = 1, ParticipantId = 1 });
            this.settledDtos.Add(new SettledDto { Punter = new CustomerDto { Id = 1 }, Stake = 100, Win = 500, EventId = 1, ParticipantId = 2 });
            this.settledDtos.Add(new SettledDto { Punter = new CustomerDto { Id = 1 }, Stake = 90, Win = 80, EventId = 2, ParticipantId = 1 });
            this.settledDtos.Add(new SettledDto { Punter = new CustomerDto { Id = 2 }, Stake = 100, Win = 200, EventId = 1, ParticipantId = 1 });
            this.settledDtos.Add(new SettledDto { Punter = new CustomerDto { Id = 2 }, Stake = 200, Win = 0, EventId = 2, ParticipantId = 1 });
            this.settledDtos.Add(new SettledDto { Punter = new CustomerDto { Id = 3 }, Stake = 20, Win = 1100, EventId = 1, ParticipantId = 1 });
            this.settledDtos.Add(new SettledDto { Punter = new CustomerDto { Id = 3 }, Stake = 20, Win = 0, EventId = 2, ParticipantId = 1 });
        }

        [Test]
        public void GetCustomers_Should_Return_List_Of_Customers_By_Processing_Fetched_Settled_Dtos()
        {
            Mock<IBetProvider> mockProvider = new Mock<IBetProvider>();
            mockProvider.Setup(p => p.GetSettledDto()).Returns(this.settledDtos);
            RiskBal riskBal = new RiskBal(mockProvider.Object);
            Dictionary<int, CustomerDto> result = riskBal.GetCustomers();
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(70, result[1].AverageStake);
            Assert.AreEqual(true, result[1].UnusuallyWins);
            Assert.AreEqual(true, result[1].UnusuallyWins);


        }

        #endregion
    }
}