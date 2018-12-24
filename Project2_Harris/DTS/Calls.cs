using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace DTS_Project
{
    [Serializable()]
    public class Calls
    {
        private string areaCode;
        public string AreaCode
        {
            get
            {
                return areaCode;
            }
            set
            {
                areaCode = value;
            }
        }

        private string exchange;
        public string Exchange
        {
            get
            {
                return exchange;
            }
            set
            {
                exchange = value;
            }
        }

        private string number;
        public string Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
            }
        }
        private string startTime;
        public string sT
        {
            get
            {
                return startTime;
            }
            set
            {
                startTime = value;
            }
        }
        private string endTime;
        public string eT
        {
            get
            {
                return endTime;
            }
            set
            {
                endTime = value;
            }
        }

        public static StringBuilder buffer = new StringBuilder();
        public Calls(string a, string e, string n, string st, string et)
        {
            areaCode = a;
            exchange = e;
            number = n;
            startTime = st;
            endTime = et;
        }

        public override string ToString()
        {
            buffer.Append(areaCode + "-" + exchange +"-" + number + " : " + startTime + " - " + endTime.ToString());
            
            return buffer.ToString();
        }
    }


    

}