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
    public partial class DoctorPatientList : Form
    {
        private CrmEngine crmEngine;
        private Stack<Form> formStack;
        public DoctorPatientList(CrmEngine crmEngine, Stack<Form> formStack)
        {
            InitializeComponent();
            this.crmEngine = crmEngine;
            this.formStack = formStack;
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
            //Search
            int patient_id = int.Parse(bunifuTextBox1.Text);
            Patient patient = new Patient(patient_id, false);
            ErrorMessage errorMessage = crmEngine.GetDoctorPatient(patient);
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
                    new DoctorCheckUpForm(crmEngine, formStack, patient).Show();
                    break;
            }
        }

        private void DoctorPatientList_Load(object sender, EventArgs e)
        {
            string doctor_name = crmEngine.GetLoggedInUser().GetUserName();
            bunifuLabel2.Text += doctor_name;

            DataTable dataTable = new DataTable();
            crmEngine.GetDoctorPatientData(dataTable);
            bunifuDataGridView1.DataSource = dataTable;
        }

        public void RefreshPatientList() { 
        
        }

        private void bunifuGradientPanel1_Click(object sender, EventArgs e)
        {

        }
    }
}
