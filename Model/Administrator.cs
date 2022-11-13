using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using HospitalCRM.Service;
using System.Data;

namespace HospitalCRM.Model
{
    public class Administrator:User
    {
        public Administrator(int user_id, string user_name, string user_email, string user_password) { 
            this.User_id = user_id;
            this.User_name = user_name;
            this.User_email = user_email;
            this.User_password = user_password;
            this.User_role_id = 1;
        }
        public Administrator(string user_name, string user_email, string user_password)
        {
            this.User_id = -1;
            this.User_name = user_name;
            this.User_email = user_email;
            this.User_password = user_password;
            this.User_role_id = 1;
        }

        public ErrorMessage InsertUserRecord(DatabaseConnection connection, User user) {
            string insertUserQuery = "insert into [User] (user_name, user_mail, user_password, user_role_id) values ('" + user.GetUserName() + "', '" + user.GetUserEmail() + "', '" + user.GetUserPassword() + "', " +  user.GetUserRoleId() + ");";
            SqlCommand insertUserCmd = new SqlCommand(insertUserQuery, connection.GetConnection());
            ErrorMessage errorCode = ErrorMessage.OK;
            try
            {
                connection.GetConnection().Open();
                insertUserCmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                errorCode = ErrorMessage.SQL_FAILED;
            }
            finally {
                connection.GetConnection().Close();
            }
            return errorCode;
        }

        public ErrorMessage UpdateUserRecord(DatabaseConnection connection, User user)
        {
            string updateUserQuery = "update [User] set user_name = '" + user.GetUserName() + "', user_mail = '" + user.GetUserEmail() + "', user_password = '" + user.GetUserPassword() + "', user_role_id = '" + user.GetUserRoleId() + "' where user_id = " + user.GetUserId() + ";";
            SqlCommand updateUserCmd = new SqlCommand(updateUserQuery, connection.GetConnection());
            ErrorMessage errorCode = ErrorMessage.OK;
            try
            {
                connection.GetConnection().Open();
                updateUserCmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                errorCode = ErrorMessage.SQL_FAILED;
            }
            finally
            {
                connection.GetConnection().Close();
            }
            return errorCode;
        }
        public ErrorMessage DeleteUserRecord(DatabaseConnection connection, User user)
        {
            string deleteUserQuery = "delete from [User] where user_id=" + user.GetUserId() + ";";
            SqlCommand deleteUserCmd = new SqlCommand(deleteUserQuery, connection.GetConnection());
            ErrorMessage errorCode = ErrorMessage.OK;
            try
            {
                connection.GetConnection().Open();
                deleteUserCmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                errorCode = ErrorMessage.SQL_FAILED;
            }
            finally
            {
                connection.GetConnection().Close();
            }
            return errorCode;
        }

        public ErrorMessage GetAllUserRecords(DatabaseConnection connection, DataTable dataTable) {
            ErrorMessage errorCode = ErrorMessage.OK;
            string getUserRecordsQuery = "select [User].user_id as [User ID], [User].user_name as [User Name], [User].user_mail as [User E-mail], [User].user_password as [User Password], Role.role_name as [User Type] from [User] inner join Role on [User].user_role_id = Role.role_id;";
            SqlDataAdapter adapter = new SqlDataAdapter(getUserRecordsQuery, connection.GetConnection());
            try
            {
                adapter.Fill(dataTable);
            }
            catch (Exception) {
                errorCode = ErrorMessage.SQL_FAILED;
            }
            return errorCode;
        }

        public ErrorMessage GetUserRecord(DatabaseConnection connection, User user, int user_id) {
            string getUserQuery = "select * from [User] where user_id = " + user_id + ";";
            SqlCommand getUserCmd = new SqlCommand(getUserQuery, connection.GetConnection());
            ErrorMessage errorCode = ErrorMessage.OK;
            try
            {
                connection.GetConnection().Open();
                using (SqlDataReader dataReader = getUserCmd.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        user.SetUserId((int)dataReader.GetValue(0));
                        user.SetUserName((string)dataReader.GetValue(1));
                        user.SetUserEmail((string)dataReader.GetValue(2));
                        user.SetUserPassword((string)dataReader.GetValue(3));
                        user.SetUserRoleId((int)dataReader.GetValue(4));
                    }
                    else
                    {
                        errorCode = ErrorMessage.USER_NOT_FOUND;
                    }
                }
            }
            catch (SqlException)
            {
                errorCode = ErrorMessage.SQL_FAILED;
            }
            finally
            {
                connection.GetConnection().Close();
            }
            return errorCode;
        }
    }
}
