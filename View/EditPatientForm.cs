using Bunifu.UI.WinForms.Helpers.Transitions;
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
    public partial class EditPatientForm : Form
    {
        private Patient patient;
        private CrmEngine crmEngine;
        private Stack<Form> formStack;
        public EditPatientForm(CrmEngine crmEngine, Stack<Form> formStack, Patient patient)
        {
            InitializeComponent();
            this.crmEngine = crmEngine;
            this.formStack = formStack;
            this.patient = patient;
        }

        private void bunifuIconButton1_Click(object sender, EventArgs e)
        {
            ((PatientList)formStack.Pop()).Visible = true;
            this.Close();
        }

        private void bunifuButton23_Click(object sender, EventArgs e)
        {
            crmEngine.Logout();
            ((LoginForm)formStack.Last()).Visible = true;
            formStack.Clear();
            this.Close();
        }

        private void EditPatientForm_Load(object sender, EventArgs e)
        {
            string receptionist_name = crmEngine.GetLoggedInUser().GetUserName();
            bunifuLabel1.Text = receptionist_name;

            bunifuTextBox1.Text = patient.getPatientName();
            bunifuTextBox2.Text = patient.getPatientEmail();
            bunifuTextBox3.Text = patient.getPatientStreetAddress();
            bunifuTextBox5.Text = patient.getPatientCity();
            bunifuTextBox4.Text = patient.getPatientMobile();
            bunifuTextBox6.Text = patient.getPatientMedicalHistory();
            string[] strArr = patient.getPatientDob().Split('-');
            DateTime dt = new DateTime(int.Parse(strArr[0]), int.Parse(strArr[1]), int.Parse(strArr[2]));
            if (dt <= bunifuDatePicker1.MinDate)
            {
                bunifuCheckBox1.Checked = false;
            }
            else {
                bunifuDatePicker1.Value = dt;
            }
            int patient_gender = patient.getPatientGender();
            if (patient_gender == 0) {
                bunifuRadioButton1.Checked = true;
                bunifuRadioButton2.Checked = false;
            }
            else
            {
                bunifuRadioButton1.Checked = false;
                bunifuRadioButton2.Checked = true;
            }
        }

        private void bunifuCheckBox1_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            if (bunifuCheckBox1.Checked)
            {
                bunifuDatePicker1.Visible = true;
            }
            else
            {
                bunifuDatePicker1.Visible = false;
            }
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            //Update
            string patient_name = bunifuTextBox1.Text.Trim();
            if (string.IsNullOrWhiteSpace(patient_name))
            {
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
            else
            {
                patient_dob = new DateTime(1, 1, 1, 0, 0, 0);
            }
            patient.SetPatientName(patient_name);
            patient.SetPatientGender(patient_gender);
            patient.SetPatientDob(patient_dob);
            patient.SetPatientStreetAddress(street_address);
            patient.SetPatientCity(city);
            patient.SetPatientMobile(patient_phone);
            patient.SetPatientMedicalHistory(patient_medical_history);
            patient.SetPatientEmail(patient_email);
            ErrorMessage errorMessage = crmEngine.UpdatePatient(patient);
            switch (errorMessage)
            {
                case ErrorMessage.OK:
                    MessageBox.Show("Patient successfully Updated.");
                    PatientList pvf = (PatientList)formStack.Pop();
                    pvf.RefreshPatientList();
                    pvf.Visible = true;
                    this.Close();
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
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            ErrorMessage errorMessage = crmEngine.DeletePatient(patient);
            switch (errorMessage)
            {
                case ErrorMessage.OK:
                    MessageBox.Show("Patient successfully deleted.");
                    PatientList pvf = (PatientList)formStack.Pop();
                    pvf.RefreshPatientList();
                    pvf.Visible = true;
                    this.Close();
                    break;
                case ErrorMessage.INVALID_USER:
                    MessageBox.Show("You are not authorised to perform this operation.");
                    break;
                case ErrorMessage.NOT_LOGGED_IN:
                    ((LoginForm)formStack.Last()).Visible = true;
                    formStack.Clear();
                    this.Close();
                    break;
            }
        }
    }
}
