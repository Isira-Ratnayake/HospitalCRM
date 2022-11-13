using HospitalCRM.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalCRM.View
{
    public partial class ReceptionistDashboard : Form
    {
        private CrmEngine crmEngine;
        private Stack<Form> formStack;
        public ReceptionistDashboard(CrmEngine crmEngine, Stack<Form> formStack)
        {
            InitializeComponent();
            this.crmEngine = crmEngine;
            this.formStack = formStack;
        }

        private void ReceptionistDashboard_Load(object sender, EventArgs e)
        {
            string receptionist_name = crmEngine.GetLoggedInUser().GetUserName();
            bunifuLabel2.Text = receptionist_name;
        }

        private void bunifuButton23_Click(object sender, EventArgs e)
        {
            crmEngine.Logout();
            ((LoginForm)formStack.Last()).Visible = true;
            formStack.Clear();
            this.Close();
        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            formStack.Push(this);
            new AddPatientForm(crmEngine, formStack).Show();
        }

        private void bunifuButton22_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            formStack.Push(this);
            new PatientList(crmEngine, formStack).Show();
        }
    }
}
