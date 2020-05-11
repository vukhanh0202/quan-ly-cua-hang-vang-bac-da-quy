using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemstonesBusinessSystem.Utils
{
    class ConvertUtils
    {
        public static string convertString(string name)
        {
            if (name != null)
            {
                return name;
            }
            return "";
        }

        public static string convertDoubleToMoney(double moneyDouble)
        {
            return string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", moneyDouble);
        }

        public static double convertMoneyToDouble(string moneyString)
        {
            return (double.Parse(moneyString.Replace(".", "")));
        }

    }
}
