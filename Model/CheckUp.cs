using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalCRM.Model
{
    public class CheckUp
    {
        private int checkup_id;
        private double height;
        private double weight;
        private double bp;
        private double cholesterol;
        private double sugar;
        private string checkup_date;
        private int patient_id;
        private int lab_officer_id;

        public CheckUp() { 
            checkup_id = -1;
            height = 0;
            weight = 0;
            bp = 0;
            cholesterol = 0;
            sugar = 0;
            checkup_date = "0001-01-01 00:00:00";
            patient_id = -1;
            lab_officer_id = -1;
        }

        public CheckUp(double height, double weight, double bp, double cholesterol, double sugar, string checkup_date, int patient_id, int lab_officer_id)
        {
            this.checkup_id = -1;
            this.height = height;
            this.weight = weight;
            this.bp = bp;
            this.cholesterol = cholesterol;
            this.sugar = sugar;
            this.checkup_date = checkup_date;
            this.patient_id = patient_id;
            this.lab_officer_id = lab_officer_id;
        }

        public int getCheckup_id() { return checkup_id; }
        public double getHeight() { return height; }
        public double getWeight() { return weight; }
        public double getBP() { return bp; }
        public double getCholesterol() { return cholesterol; }
        public double getSugar() { return sugar; }
        public String getCheckup_date() { return checkup_date; }
        public int getPatient_id() { return patient_id; }
        public int getLabOfficer_id() { return lab_officer_id; }


    }
}
