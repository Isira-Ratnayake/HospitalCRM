using HospitalCRM.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalCRM.View
{
    public partial class LoginForm : Form
    {
        private CrmEngine crmEngine;
        private Stack<Form> formStack;
        public LoginForm()
        {
            InitializeComponent();
            crmEngine = new CrmEngine();
            formStack = new Stack<Form>();
        }

        private void bunifuButton24_Click(object sender, EventArgs e)
        {
            string user_email = bunifuTextBox1.Text.Trim();
            string user_password = bunifuTextBox2.Text.Trim();
            bool isValidControls = true;
            if (string.IsNullOrWhiteSpace(user_email))
            {
                MessageBox.Show("Please enter your e-mail.");
                isValidControls = false;
            }
             if (string.IsNullOrWhiteSpace(user_password))
            {
                MessageBox.Show("Please enter your password.");
                isValidControls = false;
            }
            if (isValidControls) { 
                ErrorMessage errorMessage = crmEngine.Login(user_email, user_password);
                switch (errorMessage) {
                    case ErrorMessage.EMAIL_NOT_FOUND:
                        MessageBox.Show("User not found. Try a different e-mail.");
                        break;
                    case ErrorMessage.INCORRECT_PASSWORD:
                        MessageBox.Show("Password incorrect. Try a different password.");
                        break;
                    case ErrorMessage.OK:
                        switch (crmEngine.GetLoggedInUser().GetUserRoleId()) {
                            case 1:
                                this.Visible = false;
                                formStack.Push(this);
                                new AdminDashboard(crmEngine, formStack).Show();
                                break;
                            case 2:
                                this.Visible = false;
                                formStack.Push(this);
                                new DoctorPatientList(crmEngine, formStack).Show();
                                break;
                            case 3:
                                this.Visible = false;
                                formStack.Push(this);
                                new CheckUpList(crmEngine, formStack).Show();
                                break;
                            case 4:
                                this.Visible = false;
                                formStack.Push(this);
                                new ReceptionistDashboard(crmEngine, formStack).Show();
                                break;
                        }
                        break;
                }
            }
        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            string helpMessage = "Please contact our IT Department for more information.\nMobile: 0772294456\nE-mail: itDepartment@gmail.com";
            MessageBox.Show(helpMessage);
        }
    }
}
