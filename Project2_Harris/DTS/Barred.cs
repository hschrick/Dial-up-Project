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
    public class Barred
    {
        private string areaCode;
        public string Area
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
        public string Exg
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

        public Barred(string ac, string ex,string num)
        {
            areaCode = ac;
            exchange = ex;
            number = num;
        }

        public override string ToString()
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append(areaCode + " " + exchange + " " + number);

            return buffer.ToString();
        }


    }
}
