namespace Sample.Risk.DataAccess
{
    using System.Collections.Generic;
    using System.IO;

    using Sample.Risk.Common.Dto;

    public class CsvBetProvider : IBetProvider
    {
        #region Instance Fields

        private readonly string csvFolderPath;

        private readonly string settledFileName = "Settled.csv";

        private readonly string unsettledFileName = "Unsettled.csv";

        #endregion

        #region Constructors

        public CsvBetProvider(string csvFolderPath, string settledFileName = null, string unsettledFileName = null)
        {
            this.csvFolderPath = csvFolderPath;
            this.settledFileName = settledFileName ?? this.settledFileName;
            this.unsettledFileName = unsettledFileName ?? this.unsettledFileName;
        }

        #endregion

        #region IBetProvider Members

        public List<SettledDto> GetSettledDto()
        {
            return this.ReadBetFile<SettledDto>(this.settledFileName);
        }

        public List<UnsettledDto> GetUnsettledDtos()
        {
            return this.ReadBetFile<UnsettledDto>(this.unsettledFileName);
        }

        #endregion

        #region Private Methods

        private List<T> ReadBetFile<T>(string fileName) where T : BetDto
        {
            StreamReader reader = new StreamReader(File.OpenRead(Path.Combine(this.csvFolderPath, fileName)));
            List<T> betsDto = new List<T>();
            bool headerLine = true;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (headerLine)
                {
                    headerLine = false;
                    continue;
                }
                betsDto.Add(this.ReadBet<T>(line));
            }
            return betsDto;
        }

        private T ReadBet<T>(string csvLine) where T : BetDto
        {
            BetDto betDto;
            if (typeof(T) == typeof(UnsettledDto))
            {
                betDto = new UnsettledDto();
            }
            else
            {
                betDto = new SettledDto();
            }

            string[] values = csvLine.Split(',');

            betDto.Punter = new CustomerDto { Id = int.Parse(values[0]) };
            betDto.EventId = int.Parse(values[1]);
            betDto.ParticipantId = int.Parse(values[2]);
            betDto.Stake = int.Parse(values[3]);
            if (typeof(T) == typeof(UnsettledDto))
            {
                ((UnsettledDto)betDto).ToWin = int.Parse(values[4]);
            }
            else
            {
                ((SettledDto)betDto).Win = int.Parse(values[4]);
            }

            return (T)betDto;
        }

        #endregion
    }
}