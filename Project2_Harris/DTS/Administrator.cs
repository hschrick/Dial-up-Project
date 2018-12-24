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
    public class Administrator : ISerializable
    {
            public Administrator()
        {

        }

        private Tenant tnat;

        private string password = "ksu";

        public static List<Tenant> tenantList = new List<Tenant>();

        public List<Tenant> ten
        {
            get { return tenantList; }
            set { tenantList = value; }
        }
        
        public bool CheckPassword(string s)
        {
            if (s == password) return true;
            else return false;
        }

        public void ChangePassword(string s)
        {
            password = s;
        }

        public void AddTenant(string firstname, string lastname, string accesscode)
        {
            List<Calls> clist = new List<Calls>();
            List<Barred> blist = new List<Barred>();

            tnat = new Tenant(firstname, lastname, accesscode, blist, clist);
            tenantList.Add(tnat);
        }

        public bool CompareAccess(string access)
        {
            if (tenantList.Any(x => x.accessCode.Equals(access)) == true) return true;
            else return false;
        }

        public void DeleteTenant(string fN, string lN)
        {
            Tenant found = tenantList.Find(x => fN.Equals(x.firstName) && lN.Equals(x.lastName));
            tenantList.Remove(found);
        }
           

        public Tenant FindTenant(string fN, string lN)
        {
            foreach (Tenant tenant in tenantList)
            {
                if (tenant.firstName.Equals(fN) && tenant.lastName.Equals(lN)) return tenant;
            }
            return null;
        }
       
        
        public void ReadInAreaCode(string areaCode, int tracker)
        {
            if (tracker == 1)
                tenantList.ForEach(x => {
                x.barList = new List<Barred>();
                x.BarAreaCode(areaCode); });
            else
                tenantList.ForEach(x => { x.UnBarAreaCode(areaCode); });
            }
     
        
        public void ReadInPhoneNumber(string areaCode, string exg, string num, int tracker, Tenant tenant)
        {
            if (tracker == 1)
                tenant.BarTelephoneNum(areaCode, exg, num, tenant);
            else
                tenant.UnBarTelephone(areaCode, exg, num);
        }

        
        public bool AttemptCall(string areaCode, string exchange, string number)
        {
            if (tenantList.Any(x => x.TestNums(areaCode, exchange, number)) == true) return false;
            else return true;
        }

        
        public void RecordCall(string areaCode, string exchange, string number, string starTime, string endTime)
        {
            Calls call = new Calls(areaCode, exchange, number, starTime, endTime);
            Tenant.callList.Add(call);
        }

        
        public object[] DispTlist()
        {
            object[] objects;
            return objects = tenantList.ToArray();
        }

       
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("list", tenantList);
        }

       
        public Administrator(SerializationInfo info, StreamingContext context)
        {
            tenantList = (List<Tenant>)info.GetValue("list", typeof(List<Tenant>));
        }

    }

}

