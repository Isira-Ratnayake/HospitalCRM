using HospitalCRM.Service;
using HospitalCRM.Model;
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
    public partial class AddUserForm : Form
    {
        private CrmEngine crmEngine;
        private Stack<Form> formStack;
        public AddUserForm(CrmEngine crmEngine, Stack<Form> formStack)
        {
            InitializeComponent();
            this.crmEngine = crmEngine;
            this.formStack = formStack;
        }

        private void AddUserForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'hospitalDBDataSet.Role' table. You can move, or remove it, as needed.
            this.roleTableAdapter.Fill(this.hospitalDBDataSet.Role);

        }

        private void bunifuButton22_Click(object sender, EventArgs e)
        {
            bool isValidControl = true;
            string user_name = bunifuTextBox1.Text.Trim();
            string user_email = bunifuTextBox2.Text.Trim();
            string user_password = bunifuTextBox4.Text.Trim();
            if (string.IsNullOrWhiteSpace(user_name)) {
                MessageBox.Show("Please enter the user's name.");
                isValidControl = false;
            }
            if (string.IsNullOrWhiteSpace(user_email))
            {
                MessageBox.Show("Please enter the user's e-mail.");
                isValidControl = false;
            }
            if (string.IsNullOrWhiteSpace(user_password))
            {
                MessageBox.Show("Please enter the user's password.");
                isValidControl = false;
            }
            int user_role_id = int.Parse(bunifuDropdown1.SelectedValue.ToString());
            if (user_role_id <= 0 || user_role_id >= 6)
            {
                isValidControl = false;
                MessageBox.Show("Please select a user type from the given list.");
            }
            if (isValidControl) {
                User user = null;
                switch (user_role_id) {
                    case 1:
                        user = new Administrator(user_name, user_email, user_password);
                        break;
                    case 2:
                        user = new Doctor(user_name, user_email, user_password);
                        break;
                    case 3:
                        user = new LabOfficer(user_name, user_email, user_password);
                        break;
                    case 4:
                        user = new Receptionist(user_name, user_email, user_password);
                        break;
                }
                ErrorMessage errorMessage = crmEngine.InsertUser(user);
                switch (errorMessage) {
                    case ErrorMessage.OK:
                        MessageBox.Show("User successfully added.");
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
        }

        private void clearForm() {
            bunifuTextBox1.Clear();
            bunifuTextBox2.Clear();
            bunifuTextBox4.Clear();
            bunifuDropdown1.SelectedIndex = 0;
        }

        private void bunifuIconButton1_Click(object sender, EventArgs e)
        {
            ((AdminDashboard)formStack.Pop()).Visible = true;
            this.Close();
        }

        private void bunifuButton23_Click(object sender, EventArgs e)
        {
            crmEngine.Logout();
            ((LoginForm)formStack.Last()).Visible = true;
            formStack.Clear();
            this.Close();
        }
    }
}
