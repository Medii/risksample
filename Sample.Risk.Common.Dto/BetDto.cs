namespace Sample.Risk.Common.Dto
{
    public abstract class BetDto
    {
        #region Properties and Indexers

        public CustomerDto Punter
        {
            get;
            set;
        }

        public int EventId
        {
            get;
            set;
        }

        public int ParticipantId
        {
            get;
            set;
        }

        public int Stake
        {
            get;
            set;
        }

        #endregion
    }
}