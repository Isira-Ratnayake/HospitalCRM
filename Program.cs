using HospitalCRM.Service;
using HospitalCRM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using HospitalCRM.View;

namespace HospitalCRM
{
    
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }  
    }
    
    /*
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            CrmEngine ce = new CrmEngine();
            ce.Login("receptionist@gmail.com", "pass@receptionist");
            Patient p = new Patient(7, false);
            ce.GetPatient(p);
            Console.Out.WriteLine(p.getPatientDob());
        }
    }
    */

}
