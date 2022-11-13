using HospitalCRM.Service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalCRM.Model
{
    public class LabOfficer:User
    {
        public LabOfficer(int user_id, string user_name, string user_email, string user_password)
        {
            this.User_id = user_id;
            this.User_name = user_name;
            this.User_email = user_email;
            this.User_password = user_password;
            this.User_role_id = 3;
        }

        public LabOfficer(string user_name, string user_email, string user_password)
        {
            this.User_id = -1;
            this.User_name = user_name;
            this.User_email = user_email;
            this.User_password = user_password;
            this.User_role_id = 3;
        }

        public ErrorMessage GetAllPatientRecords(DatabaseConnection connection, DataTable dataTable)
        {
            ErrorMessage errorCode = ErrorMessage.OK;
            string getPatientRecordsQuery = "select Patient.patient_id as [Patient ID], patient_name as [Patient Name], gender_table.patient_gender as [Patient Gender], patient_email as [Patient E-mail], patient_mobile as [Patient Contact Number], patient_dob as [Patient Date of Birth], patient_street_address as [Patient Street Address], patient_city as [Patient City], patient_medical_history as [Patient Medical History] from patient inner join (select patient_id, patient_gender = case when patient_gender = 0 then 'Male' else 'Female' end from patient) as gender_table on patient.patient_id = gender_table.patient_id;";
            SqlDataAdapter adapter = new SqlDataAdapter(getPatientRecordsQuery, connection.GetConnection());
            try
            {
                adapter.Fill(dataTable);
            }
            catch (Exception)
            {
                errorCode = ErrorMessage.SQL_FAILED;
            }
            return errorCode;
        }

        public ErrorMessage insertCheckUpRecord(DatabaseConnection connection, CheckUp checkUp) {
            string insertCUQuery = "insert into checkup (height, weight, blood_pressure, cholesterol, blood_sugar, checkup_date, checkup_patient_id, checkup_lab_officer_id) values (" + checkUp.getHeight() + ", " + checkUp.getWeight() + ", " + checkUp.getBP() + ", " + checkUp.getCholesterol() + ", " + checkUp.getSugar() + ", '" + checkUp.getCheckup_date() + "', " + checkUp.getPatient_id() + ", " + checkUp.getLabOfficer_id() + ");";
            SqlCommand insertCUCmd = new SqlCommand(insertCUQuery, connection.GetConnection());
            ErrorMessage errorCode = ErrorMessage.OK;
            try
            {
                connection.GetConnection().Open();
                insertCUCmd.ExecuteNonQuery();
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

        public ErrorMessage GetPatientRecord(DatabaseConnection connection, Patient patient)
        {
            string getPatientQuery = "select * from patient where patient_id = " + patient.getPatientId() + ";";
            SqlCommand getPatientCmd = new SqlCommand(getPatientQuery, connection.GetConnection());
            ErrorMessage errorCode = ErrorMessage.OK;
            try
            {
                connection.GetConnection().Open();
                using (SqlDataReader dataReader = getPatientCmd.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        patient.setPatientId((int)dataReader.GetValue(0));
                        patient.SetPatientName((string)dataReader.GetValue(1));
                        patient.SetPatientGender((bool)dataReader.GetValue(2));
                        patient.SetPatientEmail((string)dataReader.GetValue(3));
                        patient.SetPatientMobile((string)dataReader.GetValue(4));
                        patient.SetPatientDob(dataReader.GetValue(5));
                        patient.SetPatientStreetAddress((string)dataReader.GetValue(6));
                        patient.SetPatientCity((string)dataReader.GetValue(7));
                        patient.SetPatientMedicalHistory((string)dataReader.GetValue(8));
                    }
                    else
                    {
                        errorCode = ErrorMessage.PATIENT_NOT_FOUND;
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
