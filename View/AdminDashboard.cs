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
    public partial class AdminDashboard : Form
    {
        private CrmEngine crmEngine;
        private Stack<Form> formStack;
        public AdminDashboard(CrmEngine crmEngine, Stack<Form> formStack) { 
            InitializeComponent();
            this.crmEngine = crmEngine;
            this.formStack = formStack;
        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            crmEngine.Logout();
            ((LoginForm)formStack.Last()).Visible = true;
            formStack.Clear();
            this.Close();
        }

        private void bunifuButton22_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            formStack.Push(this);
            new AddUserForm(crmEngine, formStack).Show();
        }

        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            string admin_name = crmEngine.GetLoggedInUser().GetUserName();
            string greeting = "Hello " + admin_name.Substring(0, admin_name.IndexOf(' ')) + ",";
            bunifuLabel2.Text = admin_name;
            bunifuLabel4.Text = greeting;
        }

        private void bunifuButton23_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            formStack.Push(this);
            new UserList(crmEngine, formStack).Show();
        }

        private void bunifuButton24_Click(object sender, EventArgs e)
        {
            try
            {
                int user_id = int.Parse(bunifuTextBox1.Text);
                User user = new User();
                ErrorMessage errorMessage = crmEngine.GetUser(user, user_id);
                switch (errorMessage)
                {
                    case ErrorMessage.USER_NOT_FOUND:
                        MessageBox.Show("The user was not found.");
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
                        new EditUserForm(crmEngine, formStack, user).Show();
                        break;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid user ID.");
            }
        }
    }
}
