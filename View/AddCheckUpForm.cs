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

    public partial class AddCheckUpForm : Form
    {
        private Patient patient;
        private CrmEngine crmEngine;
        private Stack<Form> formStack;
        public AddCheckUpForm(CrmEngine crmEngine, Stack<Form> formStack, Patient patient)
        {
            InitializeComponent();
            this.crmEngine = crmEngine;
            this.formStack = formStack;
            this.patient = patient;
        }

        private void bunifuIconButton1_Click(object sender, EventArgs e)
        {
            ((CheckUpList)formStack.Pop()).Visible = true;
            this.Close();
        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            crmEngine.Logout();
            ((LoginForm)formStack.Last()).Visible = true;
            formStack.Clear();
            this.Close();
        }

        private void AddCheckUpForm_Load(object sender, EventArgs e)
        {
            bunifuLabel2.Text += patient.getPatientName();
            string gender = string.Empty;
            if (patient.getPatientGender() == 0) {
                gender = "Male";
            }
            else {
                gender = "Female";
            }
            bunifuLabel6.Text += gender;
            bunifuLabel3.Text += patient.getPatientDob();
            bunifuLabel4.Text += patient.getPatientStreetAddress() + ", " + patient.getPatientCity();
            bunifuLabel5.Text += patient.getPatientMobile();
        }

        private void bunifuButton22_Click(object sender, EventArgs e)
        {
            bool isValidControl = true;
            double height = 0;
            try
            {
                height = double.Parse(bunifuTextBox1.Text.Trim());
            }
            catch (Exception) {
                MessageBox.Show("Please enter a valid height.");
                isValidControl = false;
            }

            double weight = 0;
            try
            {
                weight = double.Parse(bunifuTextBox2.Text.Trim());
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a valid weight.");
                isValidControl = false;
            }
            double bp = 0 ;
            try
            {
                bp = double.Parse(bunifuTextBox3.Text.Trim());
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a valid blood pressure.");
                isValidControl = false;
            }
            double cholesterol = 0;
            try
            {
                cholesterol = double.Parse(bunifuTextBox5.Text.Trim());
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a valid serum cholesterol level.");
                isValidControl = false;
            }
            double sugar = 0;
            try
            {
                sugar = double.Parse(bunifuTextBox4.Text.Trim());
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a valid plasma glucose level.");
                isValidControl = false;
            }
            if (isValidControl) {
                CheckUp checkUp = new CheckUp(height, weight, bp, cholesterol, sugar, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), patient.getPatientId(), crmEngine.GetLoggedInUser().GetUserId());
                ErrorMessage errorMessage = crmEngine.InsertCheckUp(checkUp);
                switch (errorMessage)
                {
                    case ErrorMessage.OK:
                        MessageBox.Show("Physical Examination successfully added.");
                        CheckUpList cl = (CheckUpList)formStack.Pop();
                        cl.RefreshCheckUpList();
                        (cl).Visible = true;
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
        }
    }
}
