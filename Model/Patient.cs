using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalCRM.Model
{
    public class Patient
    {
        private int Patient_id;
        private string Patient_name;
        private bool Patient_gender;
        private string Patient_email;
        private string Patient_mobile;
        private string Patient_dob;
        private string Patient_street_address;
        private string Patient_city;
        private List<string> Patient_emergency_contacts;
        private string Patient_medical_history;

        public Patient(int patient_id, bool patient_gender, string patient_name = "Unknown", string patient_email = "Unknown", string patient_mobile = "Unknown", string patient_street_address = "Unknown", string patient_city = "Unknown", List<string> patient_emergency_contacts = null, string patient_dob = "0001-01-01", string patient_medical_history = "Unknown") { 
            Patient_id = patient_id;
            Patient_name = patient_name;
            Patient_gender = patient_gender;
            Patient_email = patient_email;
            Patient_mobile = patient_mobile;
            Patient_street_address = patient_street_address;
            Patient_city = patient_city;
            Patient_emergency_contacts = patient_emergency_contacts;
            Patient_dob = patient_dob;
            Patient_medical_history = patient_medical_history;
        }

        public Patient(bool patient_gender, string patient_name = "Unknown", string patient_email = "Unknown", string patient_mobile = "Unknown", string patient_street_address = "Unknown", string patient_city = "Unknown", List<string> patient_emergency_contacts = null, string patient_dob = "0001-01-01", string patient_medical_history = "Unknown") {
            Patient_id = -1;
            Patient_name = patient_name;
            Patient_gender = patient_gender;
            Patient_email = patient_email;
            Patient_mobile = patient_mobile;
            Patient_street_address = patient_street_address;
            Patient_city = patient_city;
            Patient_emergency_contacts = patient_emergency_contacts;
            Patient_dob = patient_dob;
            Patient_medical_history = patient_medical_history;
        }

        public int getPatientId() { 
            return Patient_id;
        }

        public string getPatientName() {
            return Patient_name;
        }

        public int getPatientGender() {        
            if (Patient_gender) {
                return 1;
            }
            return 0;
        }

        public string getPatientEmail() {
            return Patient_email;
        }

        public string getPatientMobile() {
            return Patient_mobile;
        }

        public string getPatientDob() {
            return Patient_dob;
        }

        public string getPatientStreetAddress() {
            return Patient_street_address;
        }

        public string getPatientCity() {
            return Patient_city;
        }

        public List<string> getPatientEmergencyContacts() {
            return Patient_emergency_contacts;
        }

        public string getPatientMedicalHistory() {
            return Patient_medical_history;
        }

        public void setPatientId(int patientId) { 
            this.Patient_id = patientId;
        }

        public void SetPatientName(string patient_name) {
            this.Patient_name = patient_name;
        }

        public void SetPatientGender(bool patient_gender) {
            this.Patient_gender = patient_gender;
        }

        public void SetPatientEmail(string patient_email) { 
            this.Patient_email = patient_email;
        }

        public void SetPatientMobile(string patient_phone) { 
            this.Patient_mobile = patient_phone;
        }

        public void SetPatientDob(Object patient_dob) {
            this.Patient_dob = ((DateTime)patient_dob).ToString("yyyy-MM-dd");
        }

        public void SetPatientStreetAddress(string patient_street_address)
        {
            this.Patient_street_address = patient_street_address;
        }

        public void SetPatientCity(string patient_city) {
            this.Patient_city = patient_city;
        }

        public void SetPatientMedicalHistory(string patient_medical_history) {
            this.Patient_medical_history = patient_medical_history;
        }
    }
}
