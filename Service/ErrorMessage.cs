using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalCRM.Service
{
    public enum ErrorMessage
    {
        OK,
        EMAIL_NOT_FOUND,
        INCORRECT_PASSWORD,
        SQL_FAILED,
        INVALID_USER,
        NOT_LOGGED_IN,
        USER_NOT_FOUND,
        PATIENT_NOT_FOUND
    }
}
