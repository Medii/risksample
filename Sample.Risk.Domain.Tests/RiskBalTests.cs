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

        private List<UnsettledDto> unsettledDtos;

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

            this.unsettledDtos = new List<UnsettledDto>();
            this.unsettledDtos.Add(new UnsettledDto { Punter = new CustomerDto { Id = 1 }, Stake = 20, ToWin = 100, EventId = 3, ParticipantId = 1 });
            this.unsettledDtos.Add(new UnsettledDto { Punter = new CustomerDto { Id = 1 }, Stake = 200, ToWin = 1100, EventId = 3, ParticipantId = 2 });
            this.unsettledDtos.Add(new UnsettledDto { Punter = new CustomerDto { Id = 2 }, Stake = 100, ToWin = 200, EventId = 3, ParticipantId = 1 });
            this.unsettledDtos.Add(new UnsettledDto { Punter = new CustomerDto { Id = 2 }, Stake = 2000, ToWin = 3000, EventId = 3, ParticipantId = 2 });
            this.unsettledDtos.Add(new UnsettledDto { Punter = new CustomerDto { Id = 3 }, Stake = 220, ToWin = 300, EventId = 3, ParticipantId = 1 });
            this.unsettledDtos.Add(new UnsettledDto { Punter = new CustomerDto { Id = 3 }, Stake = 610, ToWin = 700, EventId = 4, ParticipantId = 1 });
        }

        [Test]
        public void GetCustomers_Should_Return_List_Of_Customers_By_Processing_Fetched_Settled_Dtos()
        {
            Mock<IBetProvider> mockProvider = new Mock<IBetProvider>();
            mockProvider.Setup(p => p.GetSettledDto()).Returns(this.settledDtos);
            BettingRiskBal riskBal = new BettingRiskBal(mockProvider.Object);
            Dictionary<int, CustomerDto> customers = riskBal.GetCustomers();

            Assert.AreEqual(3, customers.Count);
            Assert.AreEqual(70, customers[1].AverageStake);
            Assert.AreEqual(true, customers[1].UnusuallyWins);
            Assert.AreEqual(150, customers[2].AverageStake);
            Assert.AreEqual(false, customers[2].UnusuallyWins);
            Assert.AreEqual(20, customers[3].AverageStake);
            Assert.AreEqual(false, customers[3].UnusuallyWins);
        }

        [Test]
        public void GetUpcomingBets_Should_Return_List_Of_Upcoming_Bets_By_Processing_Recieved_Customers_And_Fetched_UnsettledDtos()
        {
            Mock<IBetProvider> mockProvider = new Mock<IBetProvider>();
            mockProvider.Setup(p => p.GetSettledDto()).Returns(this.settledDtos);
            mockProvider.Setup(p => p.GetUnsettledDtos()).Returns(this.unsettledDtos);
            BettingRiskBal riskBal = new BettingRiskBal(mockProvider.Object);

            List<UnsettledDto> unsettled = riskBal.GetUpcomingBets();
            Assert.AreEqual(6, unsettled.Count);
            Assert.AreEqual(RiskTypes.Risky, unsettled[0].RiskType);
            Assert.AreEqual(RiskTypes.Unusual | RiskTypes.Risky | RiskTypes.StakesGreatherThanThousand, unsettled[1].RiskType);
            Assert.AreEqual(RiskTypes.Normal, unsettled[2].RiskType);
            Assert.AreEqual(RiskTypes.Unusual | RiskTypes.StakesGreatherThanThousand, unsettled[3].RiskType);
            Assert.AreEqual(RiskTypes.Unusual, unsettled[4].RiskType);
            Assert.AreEqual(RiskTypes.Unusual | RiskTypes.HighlyUnusual, unsettled[5].RiskType);
        }

        #endregion
    }
}