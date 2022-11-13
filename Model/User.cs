using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalCRM.Model
{
    public class User
    {
        protected int User_id;
        protected string User_name;
        protected string User_email;
        protected string User_password;
        protected int User_role_id;

        public User() { 
            User_id = 0;
            User_name = string.Empty;
            User_email = string.Empty;
            User_password = string.Empty;
            User_role_id = 0;
        }
        public int GetUserId() {
            return User_id;
        }

        public string GetUserName() {
            return User_name;
        }

        public string GetUserEmail() {
            return User_email;
        }

        public string GetUserPassword() {
            return User_password;
        }
        public int GetUserRoleId()
        {
            return User_role_id;
        }


        public void SetUserRoleId(int user_role_id) {
            User_role_id =user_role_id;
        }

        public void SetUserId(int user_id)
        {
            User_id = user_id;
        }

        public void SetUserName(string user_name)
        {
            User_name = user_name;
        }

        public void SetUserEmail(string user_name)
        {
            User_email = user_name;
        }

        public void SetUserPassword(string user_password)
        {
            User_password = user_password;
        }

    }
}
