using Bunifu.UI.WinForms;
using HospitalCRM.Model;
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
    public partial class PatientList : Form
    {
        private CrmEngine crmEngine;
        private Stack<Form> formStack;
        public PatientList(CrmEngine crmEngine, Stack<Form> formStack)
        {
            InitializeComponent();
            this.crmEngine = crmEngine;
            this.formStack = formStack;
        }

        private void PatientList_Load(object sender, EventArgs e)
        {
            string receptionist_name = crmEngine.GetLoggedInUser().GetUserName();
            bunifuLabel2.Text = receptionist_name;

            DataTable dataTable = new DataTable();
            crmEngine.GetPatientData(dataTable);
            bunifuDataGridView1.DataSource = dataTable;
        }

        private void bunifuIconButton1_Click(object sender, EventArgs e)
        {
            ((ReceptionistDashboard)formStack.Pop()).Visible = true;
            this.Close();
        }

        private void bunifuButton23_Click(object sender, EventArgs e)
        {
            crmEngine.Logout();
            ((LoginForm)formStack.Last()).Visible = true;
            formStack.Clear();
            this.Close();
        }

        private void bunifuButton24_Click(object sender, EventArgs e)
        {
            int patient_id = int.Parse(bunifuTextBox1.Text);
            Patient patient = new Patient(patient_id, false);
            ErrorMessage errorMessage = crmEngine.GetPatient(patient);
            switch (errorMessage)
            {
                case ErrorMessage.PATIENT_NOT_FOUND:
                    MessageBox.Show("The patient was not found.");
                    break;
                case ErrorMessage.NOT_LOGGED_IN:
                    //Back to login
                    ((LoginForm)formStack.Last()).Visible = true;
                    formStack.Clear();
                    this.Close();
                    break;
                case ErrorMessage.INVALID_USER:
                    MessageBox.Show("You are not authorised to perform this operation.");
                    break;
                case ErrorMessage.SQL_FAILED:
                    MessageBox.Show("An issue occured with the database connection.\nPlease contact our IT department for further support.");
                    break;
                case ErrorMessage.OK:
                    //Move to edit user form
                    this.Visible = false;
                    formStack.Push(this);
                    new EditPatientForm(crmEngine, formStack, patient).Show();
                    break;
            }
        }

        public void RefreshPatientList() {
            bunifuTextBox1.Clear();
            DataTable dataTable = new DataTable();
            crmEngine.GetPatientData(dataTable);
            bunifuDataGridView1.DataSource = dataTable;
        }
    }
}
