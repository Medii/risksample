namespace Sample.Risk.Common.Dto
{
    public class UnsettledDto : BetDto
    {
        #region Properties and Indexers

        public int ToWin
        {
            get;
            set;
        }

        public RiskTypes RiskType
        {
            get;
            set;
        }

        #endregion
    }
}