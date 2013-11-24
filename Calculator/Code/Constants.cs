using System;
using System.Configuration;

namespace Calculator.Code
{
    public static class Constants
    {
        public static int MaxVariableCount
        {
            get
            {
                int max = 0;
                Int32.TryParse(ConfigurationManager.AppSettings["MaxVariableCount"], out max);

                return max;
            }
        }
        
        public static int MinVariableCount
        {
            get
            {
                int min = 0;
                Int32.TryParse(ConfigurationManager.AppSettings["MinVariableCount"], out min);

                return min;
            }
        }

        public static int RoundValue
        {
            get { return 7; }
        }
    }
}