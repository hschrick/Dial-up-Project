using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

namespace DTS_Project
{
    [Serializable()]
    public class TelephoneController
    {
        private Administrator admin = new Administrator();

        private String beginCallTime;

        private string endCallTime;
        

        private ITelephoneDevice telephoneDevice;
        
        // You need to add reference and/or value fields of TelephoneController
        // You may need to add Set methods to set the initlize values of these fields
        // These set methods are called from DTSInitializer.Initialize()
        public TelephoneController(ITelephoneDevice telephoneDevice)
        {
            this.telephoneDevice = telephoneDevice;
        }

        public void Activate()
        {
            // Receive an access code
            string accessCode = null;
            if (!telephoneDevice.GetAccessCode(ref accessCode)) return;
            if (admin.CompareAccess(accessCode) == true)
                MakeCall();
            }
            public void MakeCall()
        {
            // Recieve a telephone number
            string areaCode = null;
            string exchange = null;
            string number = null;
            if (!telephoneDevice.GetTelephoneNumber(ref areaCode, ref exchange, ref number)) return;
            if (admin.AttemptCall(areaCode,exchange,number))
            {
                beginCallTime = DateTime.Now.ToString();
                Connect();
                endCallTime = DateTime.Now.ToString();
                admin.RecordCall(areaCode, exchange, number,beginCallTime,endCallTime);
            }
        }



        public void Connect()
        {
            telephoneDevice.ConnectPhone();
        }

    }
}
