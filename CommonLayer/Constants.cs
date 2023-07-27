using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLayer
{
    public class Constants
    {
        # region Code File - Constants
        public const string InlineQuery = "InlineQuery";
        public const string StoredProcedure = "StoredProcedure";
        public const string PRC_GetHospitalDet = "PRC_GetHospitalDet";

        //ledgermaster

        public const string InsertLedger_Master = "PRC_Insert_Ledger_Master";
        public const string LedgerID = "LedgerID";
        public const string Account_Name = "Account_Name";
        public const string Account_Group = "Account_Group";
        public const string Address = "Address";

        //UpdateCancelSlot
        public const string PRC_Update_CancelSlot = "PRC_Update_CancelSlot";

        public const string AppDate = "AppDate";
        public const string AppHospitalID = "HospitalID";
        public const string AppDoctorID = "DoctorID";
        public const string AppTime = "AppTime";


        //Appointment Details

        public const string PRC_Insert_Appointment = "PRC_Insert_Appointment";

        public const string PatientName = "PatientName";
        public const string EndDate = "EndDate";
        public const string Time = "Time";

        public const string MobileNumber = "MobileNumber";
        public const string PersonAppointment = "PersonAppointment";
        public const string PhoneNumber = "PhoneNumber";
        public const string Relation = "Relation";

        public const string EmailID = "EmailID";
        public const string Gender = "Gender";
        public const string Area = "Area";
        public const string Query = "Query";


        public const string PRC_Insert_DoctorTime = "PRC_Insert_DoctorTime";
        public const string PRC_Delete_DoctorTime = "PRC_Delete_DoctorTime";
        public const string PRC_GetDoctorTimingDet = "PRC_GetDoctorTimingDet";
        public const string PRC_GetDoctorPersonalDet = "PRC_GetDoctorPersonalDet";
        public const string PRC_GetDoctorScheduleDet = "usp_doctorappointmentWeeklyreport";
        

        public const string Timing = "Timing";
        public const string Weekday = "Weekday";

        public const string PRC_GetDoctorDet = "PRC_GetDoctorDet";

        public const string UserName = "UserName";
        public const string Password = "Password";

        public const string PRC_GetDoctorLogin = "PRC_GetDoctorLogin";

   

        public const string PRC_GetPateintTimingDet = "PRC_GetPateintTimingDet";

        public const string PRC_Get_WeekdaysTime = "PRC_Get_WeekdaysTime";
        public const string PRC_GET_CompareDoctorAppWeeklyRep = "PRC_GET_CompareDoctorAppWeeklyRep";
        # endregion
    }
}

