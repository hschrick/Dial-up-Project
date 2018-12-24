using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;  
using System.IO;
using System.Runtime.Serialization;
namespace DTS_Project
{
    [Serializable()]
    public class TerminalController
    {
        private ITerminalDevice terminalDevice;

        private const string file = "DTSsavefile.svf";

        private Administrator admin = new Administrator();

        private Tenant tenant;

        private List<Tenant> tList = new List<Tenant>();

        public TerminalController(ITerminalDevice terminalDevice)
        {
            this.terminalDevice = terminalDevice;
        }

        public void Activate()
        {
            string password = null;
            if (!terminalDevice.GetPassword(ref password)) return;
            if (admin.CheckPassword(password) == true) 
                terminalDevice.ShowMainMenuDialog(this);
        }

        // handlers for MainMenuDialog
        public void AddTenant_Handler()
        {
            string firstName = null;
            string lastName = null;
            string accessCode = null;
            if (!terminalDevice.GetTenantInfo(ref firstName, ref lastName, ref accessCode)) return;
            admin.AddTenant(firstName,lastName,accessCode);
            
        }

        public void DeleteTenant_Handler()
        {
            string firstName = null;
            string lastName = null;
        if (!terminalDevice.GetTenantName(ref firstName, ref lastName)) return;
            admin.DeleteTenant(firstName, lastName);
        }

        public void WorkOnTenant_Handler()
        {
            string firstName = null;
            string lastName = null;
            if (!terminalDevice.GetTenantName(ref firstName, ref lastName)) return;
            if (admin.FindTenant(firstName, lastName) != null)
            {
                tenant = admin.FindTenant(firstName, lastName);
                terminalDevice.ShowTenantMenuDialog(this);
            }
        }

       

        public void DisplayTenantList_Handler()
        {
            object[] objects = admin.DispTlist();
            terminalDevice.DisplayList(objects);
        }

      

        public void Save_Handler()
        {
            Stream stream = File.Open("DTSsavefile.svf", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, admin.ten);
            stream.Close();
        }

        public void Restore_Handler()
        {
            Stream stream = File.Open("DTSsavefile.svf", FileMode.Open);

            BinaryFormatter bf = new BinaryFormatter();

            admin.ten = (List<Tenant>)bf.Deserialize(stream);
            
            stream.Close();
            
        }

        public void ChangePassword_Handler()
        {
            string password = null;
            if (!terminalDevice.GetPassword(ref password)) return;
            admin.ChangePassword(password);
        }

        
        public void BarAreaCode_Handler()
        {
            string areaCode = null;
            int tracker = 1;
            if (!terminalDevice.GetAreaCode(ref areaCode)) return;
            admin.ReadInAreaCode(areaCode, tracker);
        }

        public void BarTelephoneNumber_Handler()
        {
            string areaCode = null;
            string exchange = null;
            string number = null;
            int tracker = 1;
            if (!terminalDevice.GetTelephoneNumber(ref areaCode, ref exchange, ref number)) return;
             admin.ReadInPhoneNumber(areaCode, exchange, number, tracker,tenant);
        }

        public void UnBarAreaCode_Handler()
        {
            string areaCode = null;
            int tracker = 2;
            if (!terminalDevice.GetAreaCode(ref areaCode)) return;
            admin.ReadInAreaCode(areaCode, tracker);
        }

        public void UnBarTelephoneNumber_Handler()
        {
            string areaCode = null;
            string exchange = null;
            string number = null;
            int tracker = 2;
            if (!terminalDevice.GetTelephoneNumber(ref areaCode, ref exchange, ref number)) return;
            admin.ReadInPhoneNumber(areaCode, exchange, number, tracker,tenant);
        }

        public void DisplayCallList_Handler()
        {
            object[] objects = tenant.BuildClist();
            terminalDevice.DisplayList(objects);
        }

        public void DisplayBarList_Handler()
        {
                object[] objects = tenant.BuildBlist();
                terminalDevice.DisplayList(objects);    
        }

        public void ClearCalls_Handler()
        {
            tenant.ClearCalls();
        }

        

       
    }
}
