using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMUtil
{
    public class ObjectUtil
    {
        private ObjectUtil() { }

        public static String getCurrentServerStringDateTime()
        {
            return DateTime.Now.ToString("yyyyMMddhhmmss",new CultureInfo("en-US"));
        }

        public static String Convert2DateTimeDisplay(string datetime)
        {
            if (datetime == null || datetime.Length < 14)
            {
                return "";
            }
            return datetime.Substring(6, 2) + "/"+datetime.Substring(4, 2)+"/" +datetime.Substring(0,4) +" "+ datetime.Substring(8, 2)+":"+ datetime.Substring(10, 2)+":"+ datetime.Substring(12, 2);
        }

        public static String Convert2DateDisplay(string date)
        {
            if (date == null || date.Length < 8)
            {
                return "";
            }
            return date.Substring(6, 2) + "/" + date.Substring(4, 2) + "/" + date.Substring(0, 4);
        }
    }
}
