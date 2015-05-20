using System;
using System.Windows.Forms;
using PI_AF.Common;

namespace PI_AF.Sinus
{
    public class RandomConfig
    {
        public RandomConfig()
        {
            Frequency = 100;
        }

        public int Frequency { get; set; }
        public bool UseCounter {get; set; }



        public string GetConfigString()
        {
            var cfHelp = new ConfigStringHelper(string.Empty);
            cfHelp.AddParameter("F", Frequency);
            cfHelp.AddParameter("CNT", UseCounter);

            var s = cfHelp.GetConfigurationString();
            return s;
        }

        public static RandomConfig FromConfigString(string confString)
        {
            RandomConfig res = new RandomConfig();
            try
            {
                var cfHelp = new ConfigStringHelper(confString);
                res.Frequency = cfHelp.GetValue<int>("F") ?? 100;
                res.UseCounter = cfHelp.GetValue<bool>("CNT") ?? false;
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("Invalid config string: {0}, E={1}", confString,e));
                return null;
            }
            return res;
        }
    }
}
