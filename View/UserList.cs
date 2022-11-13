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
    public partial class UserList : Form
    {
        private CrmEngine crmEngine;
        private Stack<Form> formStack;
        public UserList(CrmEngine crmEngine, Stack<Form> formStack)
        {
            InitializeComponent();
            this.crmEngine = crmEngine;
            this.formStack = formStack;
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

        private void UserList_Load(object sender, EventArgs e)
        {
            string admin_name = crmEngine.GetLoggedInUser().GetUserName();
            bunifuLabel14.Text = admin_name;
            DataTable dataTable = new DataTable();
            crmEngine.GetUserData(dataTable);
            bunifuDataGridView1.DataSource = dataTable;
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

        public void RefreshDataGrid() {
            bunifuTextBox1.Clear();
            string admin_name = crmEngine.GetLoggedInUser().GetUserName();
            bunifuLabel14.Text = admin_name;
            DataTable dataTable = new DataTable();
            crmEngine.GetUserData(dataTable);
            bunifuDataGridView1.DataSource = dataTable;
        }

        private void bunifuShadowPanel1_ControlAdded(object sender, ControlEventArgs e)
        {

        }
    }
}
