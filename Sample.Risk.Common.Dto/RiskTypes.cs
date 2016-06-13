namespace Sample.Risk.Common.Dto
{
    using System;

    [Flags]
    public enum RiskTypes
    {
        Normal = 0,

        Risky = 2,

        Unusual = 4,

        HighlyUnusual = 8,

        StakesGreatherThanThousand = 16
    }
}