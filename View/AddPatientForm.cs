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
using HospitalCRM.Model;

namespace HospitalCRM.View
{
    public partial class AddPatientForm : Form
    {
        private CrmEngine crmEngine;
        private Stack<Form> formStack;
        public AddPatientForm(CrmEngine crmEngine, Stack<Form> formStack)
        {
            InitializeComponent();
            this.crmEngine = crmEngine;
            this.formStack = formStack;
        }

        private void bunifuIconButton1_Click_1(object sender, EventArgs e)
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

        private void AddPatientForm_Load(object sender, EventArgs e)
        {
            string receptionist_name = crmEngine.GetLoggedInUser().GetUserName();
            bunifuLabel1.Text = receptionist_name;
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            string patient_name = bunifuTextBox1.Text.Trim();
            if (string.IsNullOrWhiteSpace(patient_name)) {
                patient_name = "Unknown";
            }
            string patient_email = bunifuTextBox2.Text.Trim();
            if (string.IsNullOrWhiteSpace(patient_email))
            {
                patient_email = "Unknown";
            }
            string street_address = bunifuTextBox3.Text.Trim();
            if (string.IsNullOrWhiteSpace(street_address))
            {
                street_address = "Unknown";
            }
            string city = bunifuTextBox5.Text.Trim();
            if (string.IsNullOrWhiteSpace(city))
            {
                city = "Unknown";
            }
            string patient_phone = bunifuTextBox4.Text.Trim();
            if (string.IsNullOrWhiteSpace(patient_phone))
            {
                patient_phone = "Unknown";
            }
            string patient_medical_history = bunifuTextBox6.Text.Trim();
            if (string.IsNullOrWhiteSpace(patient_medical_history))
            {
                patient_medical_history = "Unknown";
            }
            bool patient_gender = (!bunifuRadioButton1.Checked);

            DateTime patient_dob;
            if (bunifuDatePicker1.Visible)
            {
                patient_dob = bunifuDatePicker1.Value;
            }
            else {
                patient_dob = new DateTime(1, 1, 1, 0, 0, 0);
            }
            Patient patient = new Patient(patient_gender, patient_name, patient_email, patient_phone, street_address, city, patient_dob:patient_dob.ToString("yyyy-MM-dd HH:mm:ss"), patient_medical_history: patient_medical_history);
            ErrorMessage errorMessage = crmEngine.InsertPatient(patient);
            switch(errorMessage) {
                case ErrorMessage.OK:
                    MessageBox.Show("Patient successfully added.");
                    break;
                case ErrorMessage.INVALID_USER:
                    MessageBox.Show("You are not authorised to perform this operation.");
                    break;
                case ErrorMessage.NOT_LOGGED_IN:
                    //Send back to login
                    ((LoginForm)formStack.Last()).Visible = true;
                    formStack.Clear();
                    this.Close();
                    break;
            }
            clearForm();
        }

        private void bunifuCheckBox1_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            if (bunifuCheckBox1.Checked)
            {
                bunifuDatePicker1.Visible = true;
            }
            else {
                bunifuDatePicker1.Visible = false;
            }
        }

        private void clearForm()
        {
            bunifuTextBox1.Clear();
            bunifuTextBox2.Clear();
            bunifuTextBox3.Clear();
            bunifuTextBox4.Clear();
            bunifuTextBox5.Clear();
            bunifuTextBox6.Clear();
            bunifuRadioButton1.Checked = true;
            bunifuRadioButton2.Checked = false;
            bunifuCheckBox1.Checked = true;
            bunifuDatePicker1.Value = DateTime.Now;
        }
    }
}
