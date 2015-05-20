using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PI_AF.Common
{
        
    public class ConfigStringHelper
    {

        private const char parameterDelimiter = ',';
        private const char valueDelimiter = '=';

        public void AddParameter(string parameter, object parameterValue)
        {
            parameterName2StringValue[parameter] = parameterValue.ToString();
        }



        public string GetConfigurationString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var paar in parameterName2StringValue)
            {
                if (sb.Length > 0)
                    sb.Append(parameterDelimiter);

                sb.AppendFormat("{0}{1}{2}", paar.Key, valueDelimiter, paar.Value ?? "NULL");
            }
            return sb.ToString();
        }


        private Dictionary<string, string> parameterName2StringValue = new Dictionary<string, string>();
        public ConfigStringHelper(string configurationString)
        {
            if (string.IsNullOrEmpty(configurationString))
                return;
 
            foreach (var parVal in configurationString.Split(parameterDelimiter))
            {
                var paar = parVal.Split(valueDelimiter);
                if (paar.Length != 2)
                    continue;
                parameterName2StringValue[paar[0]] = paar[1];
            }
        }


        public string GetValueString(string parameterName)
        {
            string val;
            if (!parameterName2StringValue.TryGetValue(parameterName, out val))
                return null;

            return val;
        }

       
        public T? GetValue<T>(string parameterName) where T: struct
        {
            string val;
            if(! parameterName2StringValue.TryGetValue(parameterName, out val))
                return null;
            
            var typ = typeof(T);

            var miParse = typ.GetMethod("Parse",  System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public, null, new Type[] {typeof(string)}, null);

            if (miParse != null)
            {
                T value = (T) miParse.Invoke(null,new object[] { val});
                return value;
            }


            MessageBox.Show(string.Format("{1}: Cannot parse type {0}", typ, this.GetType()));
            
            return null;
        }
    }
}
