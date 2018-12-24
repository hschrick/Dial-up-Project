using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

namespace DTS_Project
{
    [Serializable()]
    public class Tenant
    {
        public string firstName;

        public string lastName;

        public string accessCode;

        public List<Barred> barAreaList = new List<Barred>();

        public List<Barred> barList;

        public static List<Calls> callList;

        public List<Calls> cList
        { get { return callList; } }
        
        
        public Tenant(string first, string last, string access, List<Barred> blist, List<Calls> clist)
        {
            firstName = first;
            lastName = last;
            accessCode = access;
            barList = blist;
            callList = clist;
        }

        
        public void BarAreaCode(string areaCode)
        {
            string exg = null;
            string num = null;
            Barred Cur = new Barred(areaCode,exg,num);

           if (areaCode.Equals(Cur.Area))
               barAreaList.Add(Cur);
        }

        
        public void UnBarAreaCode(string areaCode)
        {
            Barred found = barAreaList.Find(x => areaCode.Equals(x.Area));
            barAreaList.Remove(found);
            }

        
        public void BarTelephoneNum(string areaCode, string exg, string num, Tenant tenant)
        {
            Barred Cur = new Barred(areaCode, exg, num);

            if (areaCode.Equals(Cur.Area) && exg.Equals(Cur.Exg) && num.Equals(Cur.Number))
            barList.Add(Cur);
        }

      
        public void UnBarTelephone(string areaCode, string exg, string number)
        {
            Barred found = barList.Find(x => areaCode.Equals(x.Area) && exg.Equals(x.Exg) && number.Equals(x.Number));
            barList.Remove(found);
        }

        
        public bool TestNums(string areaCode, string exg, string number)
        {
            if(barAreaList.Any(x => areaCode.Equals(x.Area)) == true) return true;
            if (barList.Any(x => areaCode.Equals(x.Area) && exg.Equals(x.Exg) && number.Equals(x.Number)) == true) return true;
            else return false;
        }

        
        public object[] BuildBlist()
        {
            List<string> str = new List<string>();
            object[] objects;

            if (barList.Any(x => x != null)) return objects = barList.ToArray();
            else objects = str.ToArray();
            return objects;
        }

        
        public object[] BuildClist()
        {
            List<string> str = new List<string>();
            object[] objects;

            if (callList.Any(x => x != null)) return objects = callList.ToArray();
            else objects = str.ToArray();
            return objects;
        }
           

        
        public override string ToString()
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append(firstName + " " + lastName + " " + accessCode);
            return buffer.ToString();
        }

        
        public void ClearCalls()
        {
            callList.Clear();
        }
    }
}
