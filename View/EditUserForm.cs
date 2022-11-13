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
    public partial class EditUserForm : Form
    {
        private User user;
        private CrmEngine crmEngine;
        private Stack<Form> formStack;
        public EditUserForm(CrmEngine crmEngine, Stack<Form> formStack, User user)
        {
            InitializeComponent();
            this.crmEngine = crmEngine;
            this.formStack = formStack;
            this.user = user;
        }

        private void bunifuIconButton1_Click(object sender, EventArgs e)
        {
            Form prev_form = formStack.Pop();
            try
            {
                ((AdminDashboard)prev_form).Visible = true;
            }
            catch (InvalidCastException) {
                ((UserList)prev_form).Visible = true;
            }
            this.Close();
        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            crmEngine.Logout();
            ((LoginForm)formStack.Last()).Visible = true;
            formStack.Clear();
            this.Close();
        }

        private void EditUserForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'hospitalDBDataSet.Role' table. You can move, or remove it, as needed.
            this.roleTableAdapter.Fill(this.hospitalDBDataSet.Role);
            string admin_name = crmEngine.GetLoggedInUser().GetUserName();
            bunifuLabel14.Text = admin_name;

            bunifuTextBox1.Text = user.GetUserName();
            bunifuTextBox2.Text = user.GetUserEmail();
            bunifuTextBox3.Text = user.GetUserPassword();
            bunifuDropdown1.Text = "Administrator";

            bunifuDropdown1.SelectedIndex = user.GetUserRoleId() - 1;
            bunifuDropdown1.SelectedValue = user.GetUserRoleId();
            bunifuDropdown1.SelectedText = bunifuDropdown1.Items[bunifuDropdown1.SelectedIndex].ToString();

        }

        private void bunifuButton22_Click(object sender, EventArgs e)
        {
            bool isValidControl = true;
            string userName = bunifuTextBox1.Text.Trim();
            string userEmail = bunifuTextBox2.Text.Trim();
            string userPassword = bunifuTextBox3.Text.Trim();
            if (string.IsNullOrWhiteSpace(userName))
            {
                MessageBox.Show("Please enter the user's name.");
                isValidControl = false;
            }
            if (string.IsNullOrWhiteSpace(userEmail))
            {
                MessageBox.Show("Please enter the user's e-mail.");
                isValidControl = false;
            }
            if (string.IsNullOrWhiteSpace(userPassword))
            {
                MessageBox.Show("Please enter the user's password.");
                isValidControl = false;
            }
            int userRoleId = int.Parse(bunifuDropdown1.SelectedValue.ToString());
            if (userRoleId <= 0 || userRoleId >= 6) {
                isValidControl = false;
                MessageBox.Show("Please select a user type from the given list.");
            }
            if (isValidControl) {
                user.SetUserName(userName);
                user.SetUserEmail(userEmail);
                user.SetUserPassword(userPassword);
                user.SetUserRoleId(userRoleId);
                ErrorMessage errorMessage = crmEngine.UpdateUser(user);
                switch (errorMessage) {
                    case ErrorMessage.OK:
                        MessageBox.Show("User successfully updated.");
                        Form prev_form = formStack.Pop();
                        try
                        {
                            ((AdminDashboard)prev_form).Visible = true;
                        }
                        catch (InvalidCastException)
                        {
                            UserList pvf = (UserList)prev_form;
                            pvf.RefreshDataGrid();
                            pvf.Visible = true;
                        }
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

        private void bunifuButton23_Click(object sender, EventArgs e)
        {
            ErrorMessage errorMessage = crmEngine.DeleteUser(user);
            switch (errorMessage)
            {
                case ErrorMessage.OK:
                    MessageBox.Show("User successfully deleted.");
                    Form prev_form = formStack.Pop();
                    try
                    {
                        ((AdminDashboard)prev_form).Visible = true;
                    }
                    catch (InvalidCastException)
                    {
                        UserList pvf = (UserList)prev_form;
                        pvf.RefreshDataGrid();
                        pvf.Visible = true;
                    }
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