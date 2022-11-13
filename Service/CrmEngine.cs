using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using HospitalCRM.Model;
using System.Data;

namespace HospitalCRM.Service
{
    public class CrmEngine
    {
        private DatabaseConnection Db_connection;
        private User User;

        public CrmEngine() { 
            Db_connection = new DatabaseConnection();
            User = null;
        }

        public ErrorMessage Login(string user_email, string password) { 
            string loginQuery = "select * from [User] where user_mail='" + user_email + "';";
            SqlCommand loginCmd = new SqlCommand(loginQuery, Db_connection.GetConnection());
            ErrorMessage errorCode = ErrorMessage.OK;
            try
            {
                Db_connection.GetConnection().Open();
                using (SqlDataReader dataReader = loginCmd.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        if ((string)dataReader.GetValue(3) == password)
                        {
                            switch (dataReader.GetValue(4)) {
                                case 1:
                                    User = new Administrator((int)dataReader.GetValue(0), (string)dataReader.GetValue(1), (string)dataReader.GetValue(2), (string)dataReader.GetValue(3));
                                    break;
                                case 2:
                                    User = new Doctor((int)dataReader.GetValue(0), (string)dataReader.GetValue(1), (string)dataReader.GetValue(2), (string)dataReader.GetValue(3));
                                    break;
                                case 3:
                                    User = new LabOfficer((int)dataReader.GetValue(0), (string)dataReader.GetValue(1), (string)dataReader.GetValue(2), (string)dataReader.GetValue(3));
                                    break;
                                case 4:
                                    User = new Receptionist((int)dataReader.GetValue(0), (string)dataReader.GetValue(1), (string)dataReader.GetValue(2), (string)dataReader.GetValue(3));
                                    break;
                            }
                        }
                        else {
                            errorCode = ErrorMessage.INCORRECT_PASSWORD;
                        }
                    }
                    else
                    {
                        errorCode = ErrorMessage.EMAIL_NOT_FOUND;
                    }
                }
            }
            catch (SqlException)
            {
                errorCode = ErrorMessage.SQL_FAILED;
            }
            finally { 
                Db_connection.GetConnection().Close();
            }
            return errorCode;
        }

        public bool IsLoggedIn() {
            return (User != null);
        }

        public void Logout() {
            User = null;
        }

        public ErrorMessage InsertUser(User user) {
            ErrorMessage errorCode;
            if (IsLoggedIn())
            {
                try
                {
                    Administrator administrator = (Administrator)User;
                    errorCode = administrator.InsertUserRecord(Db_connection, user);
                }
                catch (InvalidCastException) { 
                    errorCode = ErrorMessage.INVALID_USER;
                }
            }
            else {
                errorCode = ErrorMessage.NOT_LOGGED_IN;
            }
            return errorCode;
        }

        public ErrorMessage DeleteUser(User user) {
            ErrorMessage errorCode;
            if (IsLoggedIn()) {
                try {
                    Administrator administrator = (Administrator)User;
                    errorCode = administrator.DeleteUserRecord(Db_connection, user);
                }
                catch (InvalidCastException) {
                    errorCode = ErrorMessage.INVALID_USER;
                }
            }
            else {
                errorCode = ErrorMessage.NOT_LOGGED_IN;
            }
            return errorCode;
        }

        public ErrorMessage UpdateUser(User user) {
            ErrorMessage errorCode;
            if (IsLoggedIn()) {
                try {
                    Administrator administrator = (Administrator)User;
                    errorCode = administrator.UpdateUserRecord(Db_connection, user);
                }
                catch (InvalidCastException) {
                    errorCode = ErrorMessage.INVALID_USER;
                }
            }
            else {
                errorCode = ErrorMessage.NOT_LOGGED_IN;
            }
            return errorCode;
        }

        public ErrorMessage InsertPatient(Patient patient) {
            ErrorMessage errorCode;
            if (IsLoggedIn())
            {
                try
                {
                    Receptionist receptionist = (Receptionist)User;
                    errorCode = receptionist.InsertPatientRecord(Db_connection, patient);
                }
                catch (InvalidCastException)
                {
                    errorCode = ErrorMessage.INVALID_USER;
                }
            }
            else
            {
                errorCode = ErrorMessage.NOT_LOGGED_IN;
            }
            return errorCode;
        }

        public User GetLoggedInUser() {
            if (IsLoggedIn()) {
                return User;
            }
            return null;
        }

        public ErrorMessage GetUserData(DataTable dataTable) {
            ErrorMessage errorCode;
            if (IsLoggedIn())
            {
                try
                {
                    Administrator administrator = (Administrator)User;
                    errorCode = administrator.GetAllUserRecords(Db_connection, dataTable);
                    
                }
                catch (InvalidCastException)
                {
                    errorCode = ErrorMessage.INVALID_USER;
                }
            }
            else
            {
                errorCode = ErrorMessage.NOT_LOGGED_IN;
            }
            return errorCode;
        }

        public ErrorMessage GetPatientData(DataTable dataTable)
        {
            ErrorMessage errorCode;
            if (IsLoggedIn())
            {
                try
                {
                    Receptionist receptionist = (Receptionist)User;
                    errorCode = receptionist.GetAllPatientRecords(Db_connection, dataTable);

                }
                catch (InvalidCastException)
                {
                    errorCode = ErrorMessage.INVALID_USER;
                }
            }
            else
            {
                errorCode = ErrorMessage.NOT_LOGGED_IN;
            }
            return errorCode;
        }

        public ErrorMessage GetUser(User user, int user_id) {
            ErrorMessage errorCode;
            if (IsLoggedIn())
            {
                try
                {
                    Administrator administrator = (Administrator)User;
                    errorCode = administrator.GetUserRecord(Db_connection, user, user_id);
                }
                catch (InvalidCastException)
                {
                    errorCode = ErrorMessage.INVALID_USER;
                }
            }
            else
            {
                errorCode = ErrorMessage.NOT_LOGGED_IN;
            }
            return errorCode;
        }

        public ErrorMessage GetPatient(Patient patient) {
            ErrorMessage errorCode;
            if (IsLoggedIn())
            {
                try
                {
                    Receptionist receptionist = (Receptionist)User;
                    errorCode = receptionist.GetPatientRecord(Db_connection, patient);
                }
                catch (InvalidCastException ex)
                {
                    errorCode = ErrorMessage.INVALID_USER;
                    Console.Out.WriteLine(ex.Message);
                }
            }
            else
            {
                errorCode = ErrorMessage.NOT_LOGGED_IN;
            }
            return errorCode;
        }

        public ErrorMessage GetCheckUpPatient(Patient patient)
        {
            ErrorMessage errorCode;
            if (IsLoggedIn())
            {
                try
                {
                    LabOfficer lo = (LabOfficer)User;
                    errorCode = lo.GetPatientRecord(Db_connection, patient);
                }
                catch (InvalidCastException ex)
                {
                    errorCode = ErrorMessage.INVALID_USER;
                    Console.Out.WriteLine(ex.Message);
                }
            }
            else
            {
                errorCode = ErrorMessage.NOT_LOGGED_IN;
            }
            return errorCode;
        }

        public ErrorMessage GetCheckUpData(DataTable dataTable)
        {
            ErrorMessage errorCode;
            if (IsLoggedIn())
            {
                try
                {
                    LabOfficer labOfficer = (LabOfficer)User;
                    errorCode = labOfficer.GetAllPatientRecords(Db_connection, dataTable);

                }
                catch (InvalidCastException)
                {
                    errorCode = ErrorMessage.INVALID_USER;
                }
            }
            else
            {
                errorCode = ErrorMessage.NOT_LOGGED_IN;
            }
            return errorCode;
        }
        public ErrorMessage InsertCheckUp(CheckUp checkUp)
        {
            ErrorMessage errorCode;
            if (IsLoggedIn())
            {
                try
                {
                    LabOfficer labOfficer = (LabOfficer)User;
                    errorCode = labOfficer.insertCheckUpRecord(Db_connection, checkUp);

                }
                catch (InvalidCastException)
                {
                    errorCode = ErrorMessage.INVALID_USER;
                }
            }
            else
            {
                errorCode = ErrorMessage.NOT_LOGGED_IN;
            }
            return errorCode;
        }

        public ErrorMessage UpdatePatient(Patient patient) {
            ErrorMessage errorCode;
            if (IsLoggedIn())
            {
                try
                {
                    Receptionist receptionist = (Receptionist)User;
                    errorCode = receptionist.UpdatePatientRecord(Db_connection, patient);
                }
                catch (InvalidCastException)
                {
                    errorCode = ErrorMessage.INVALID_USER;
                }
            }
            else
            {
                errorCode = ErrorMessage.NOT_LOGGED_IN;
            }
            return errorCode;
        }

        public ErrorMessage DeletePatient(Patient patient)
        {
            ErrorMessage errorCode;
            if (IsLoggedIn())
            {
                try
                {
                    Receptionist receptionist = (Receptionist)User;
                    errorCode = receptionist.DeletePatientRecord(Db_connection, patient);
                }
                catch (InvalidCastException)
                {
                    errorCode = ErrorMessage.INVALID_USER;
                }
            }
            else
            {
                errorCode = ErrorMessage.NOT_LOGGED_IN;
            }
            return errorCode;
        }

        public ErrorMessage GetDoctorPatient(Patient patient)
        {
            ErrorMessage errorCode;
            if (IsLoggedIn())
            {
                try
                {
                    Doctor doctor = (Doctor)User;
                    errorCode = doctor.GetPatientRecord(Db_connection, patient);
                }
                catch (InvalidCastException ex)
                {
                    errorCode = ErrorMessage.INVALID_USER;
                    Console.Out.WriteLine(ex.Message);
                }
            }
            else
            {
                errorCode = ErrorMessage.NOT_LOGGED_IN;
            }
            return errorCode;
        }

        public ErrorMessage GetDoctorPatientData(DataTable dataTable)
        {
            ErrorMessage errorCode;
            if (IsLoggedIn())
            {
                try
                {
                    Doctor doctor = (Doctor)User;
                    errorCode = doctor.GetAllPatientRecords(Db_connection, dataTable);

                }
                catch (InvalidCastException)
                {
                    errorCode = ErrorMessage.INVALID_USER;
                }
            }
            else
            {
                errorCode = ErrorMessage.NOT_LOGGED_IN;
            }
            return errorCode;
        }

        public ErrorMessage GetPatientCheckUpData(DataTable dataTable, Patient patient)
        {
            ErrorMessage errorCode;
            if (IsLoggedIn())
            {
                try
                {
                    Doctor doctor = (Doctor)User;
                    errorCode = doctor.GetAllCheckUpRecords(Db_connection, dataTable, patient);

                }
                catch (InvalidCastException)
                {
                    errorCode = ErrorMessage.INVALID_USER;
                }
            }
            else
            {
                errorCode = ErrorMessage.NOT_LOGGED_IN;
            }
            return errorCode;
        }
    }
}
