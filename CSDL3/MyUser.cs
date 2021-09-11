using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSDL3
{
    class MyUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        private static string erorr = "There was some erorr";

        public static void ShowErorr()
        {
            System.Windows.Forms.MessageBox.Show(erorr);
        }

        public static bool IsEqual(MyUser user1, MyUser user2)
        {
            if (user1 == null || user2 == null) { return false; }
            if (user1.Username != user2.Username)
            {
                erorr = "Username does not exist!";
                return false;
            }
            else if (user1.Password != user2.Password)
            {
                erorr = "Username and Password does not match!";
                return false;
            }
            return true;
        }
    }
    class Data
    {
        public string Time { get; set; }
        public string Temperature { get; set; }
        public string Humidity { get; set; }
    }

    class Warning
    {
        public string GHNDT { get; set; }
        public string GHNDD { get; set; }
        public string GHDAT { get; set; }
        public string GHDAD { get; set; }
    }

    class Data1
    {
        public string ND { get; set; }
        public string DA { get; set; }
    }
    class setDelete
    {
        public string DeleteValue { get; set; }
    }

    class StDevice
    {
        public string btRoom1 { get; set; }
        public string btRoom2 { get; set; }
        public string btRoom3 { get; set; }
        public string btAir { get; set; }
        public string btTV { get; set; }
        public string btFan { get; set; }
    }

    class fDevice
    {
        public string oRoom1 { get; set; }
        public string oRoom2 { get; set; }
        public string oRoom3 { get; set; }
        public string oAir { get; set; }
        public string oTV { get; set; }
        public string oFan { get; set; }
    }
    class setClock
    {
        public int timeLock1 { get; set; }
        public int timeLock2 { get; set; }
    }

    class pointWarning
    {
        public string pNDT { get; set; }
        public string pNDD { get; set; }
        public string pDAT { get; set; }
        public string pDAD { get; set; }
    }

}