using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BelferCS
{
    public class ScoreAnalyser
    {
        private int[] scoreCount = new int[7];
        public string Label { get; set; }
        public int StudentCount { get; set; }
        public int[] ScoreCount
        {
            get
            {
                return scoreCount;
            }
            set
            {
                scoreCount = value;
            }
        }
        public int TotalScoreCount
        {
            get
            {
                return scoreCount.Skip(1).Sum();
            }
        }
        public int ExcelentCount
        {
            get
            {
                return scoreCount[6];
            }
        }
        public int VeryGoodCount
        {
            get
            {
                return scoreCount[5];
            }
        }
        public int GoodCount
        {
            get
            {
                return scoreCount[4];
            }
        }
        public int SufficientCount
        {
            get
            {
                return scoreCount[3];
            }
        }
        public int PassedCount
        {
            get
            {
                return scoreCount[2];
            }
        }
        public int FailedCount
        {
            get
            {
                return scoreCount[1];
            }
        }
        public int UnclassifiedCount
        {
            get
            {
                return scoreCount[0];
            }
        }
    }
    public class ScoreAggregate : ScoreAnalyser
    {
        public float Avg()
        {
            int Total = 0;
            for (int i = 1; i < ScoreCount.Count(); i++)
            {
                Total += i * ScoreCount[i];
            }
            if (Total == 0) return 0;
            return (float)Total / TotalScoreCount;
        }
        public float Avg(byte decimalPlaces)
        {
            return (float)Math.Round(Avg(), decimalPlaces);
        }
        public float Median()
        {
            List<int> Numbers = new List<int>();
            for (int i = 1; i < ScoreCount.Count(); i++)
            {
                for (int j = 0; j < ScoreCount[i]; j++)
                {
                    Numbers.Add(i);
                }
            }
            if (Numbers.Count == 0) return 0;
            int HalfIndex = Numbers.Count() / 2;
            if (Numbers.Count() % 2 == 0)
            {
                return (float)(Numbers[HalfIndex] + Numbers[HalfIndex - 1]) / 2;
            }
            else
            {
                return Numbers[HalfIndex];
            }
        }
        public int Dominant()
        {
            int modal = 1;
            for (int i = 2; i < ScoreCount.Count(); i++)
            {
                if (ScoreCount[i] > ScoreCount[modal]) modal = i;
            }
            return modal;
        }
    }

    public class ScoreAnalyserByPercent : ScoreAnalyser
    {
        public float TotalScoreCountByPercent()
        {
            return (float)TotalScoreCount * 100 / StudentCount;
        }
        public float TotalScoreCountByPercent(byte decimalPlaces)
        {
            return (float)Math.Round(TotalScoreCountByPercent(), decimalPlaces);
        }
        public float ExcelentCountByPercent()
        {
            if (TotalScoreCount == 0) return 0;
            return (float)ScoreCount[6] * 100 / TotalScoreCount;
        }
        public float ExcelentCountByPercent(byte decimalPlaces)
        {
            return (float)Math.Round(ExcelentCountByPercent(), decimalPlaces);
        }
        public float VeryGoodCountByPercent()
        {
            if (TotalScoreCount == 0) return 0;
            return (float)ScoreCount[5] * 100 / TotalScoreCount;
        }
        public float VeryGoodCountByPercent(byte decimalPlaces)
        {
            return (float)Math.Round(VeryGoodCountByPercent(), decimalPlaces);
        }
        public float GoodCountByPercent()
        {
            if (TotalScoreCount == 0) return 0;
            return (float)ScoreCount[4] * 100 / TotalScoreCount;
        }
        public float GoodCountByPercent(byte decimalPlaces)
        {
            return (float)Math.Round(GoodCountByPercent(), decimalPlaces);
        }
        public float SufficientCountByPercent()
        {
            if (TotalScoreCount == 0) return 0;
            return (float)ScoreCount[3] * 100 / TotalScoreCount;
        }
        public float SufficientCountByPercent(byte decimalPlaces)
        {
            return (float)Math.Round(SufficientCountByPercent(), decimalPlaces);
        }
        public float PassedCountByPercent()
        {
            if (TotalScoreCount == 0) return 0;
            return (float)ScoreCount[2] * 100 / TotalScoreCount;
        }
        public float PassedCountByPercent(byte decimalPlaces)
        {
            return (float)Math.Round(PassedCountByPercent(), decimalPlaces);
        }
        public float FailedCountByPercent()
        {
            if (TotalScoreCount == 0) return 0;
            return (float)ScoreCount[1] * 100 / TotalScoreCount;
        }
        public float FailedCountByPercent(byte decimalPlaces)
        {
            return (float)Math.Round(FailedCountByPercent(), decimalPlaces);
        }
        public float UnclassifiedCountByPercent()
        {
            if (TotalScoreCount == 0) return 0;
            return (float)ScoreCount[0] * 100 / TotalScoreCount;
        }
        public float UnclassifiedCountByPercent(byte decimalPlaces)
        {
            return (float)Math.Round(UnclassifiedCountByPercent(), decimalPlaces);
        }
    }
  
}
