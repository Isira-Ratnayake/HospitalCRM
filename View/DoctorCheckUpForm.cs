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
    public partial class DoctorCheckUpForm : Form
    {
        private Patient patient;
        private CrmEngine crmEngine;
        private Stack<Form> formStack;
        public DoctorCheckUpForm(CrmEngine crmEngine, Stack<Form> formStack, Patient patient)
        {
            InitializeComponent();
            this.crmEngine = crmEngine;
            this.formStack = formStack;
            this.patient = patient;
        }

        private void bunifuIconButton1_Click(object sender, EventArgs e)
        {
            ((DoctorPatientList)formStack.Pop()).Visible = true;
            this.Close();
        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            crmEngine.Logout();
            ((LoginForm)formStack.Last()).Visible = true;
            formStack.Clear();
            this.Close();
        }

        private void DoctorCheckUpForm_Load(object sender, EventArgs e)
        {
            bunifuLabel2.Text += patient.getPatientName();
            string gender = string.Empty;
            if (patient.getPatientGender() == 0)
            {
                gender = "Male";
            }
            else
            {
                gender = "Female";
            }
            bunifuLabel6.Text += gender;
            bunifuLabel3.Text += patient.getPatientDob();
            medHistory.Text = patient.getPatientMedicalHistory();

            DataTable dataTable = new DataTable();
            crmEngine.GetPatientCheckUpData(dataTable, patient);
            bunifuDataGridView1.DataSource = dataTable;
        }
    }
}
