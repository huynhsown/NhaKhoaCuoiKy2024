using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NhaKhoaCuoiKy.dbs;

namespace NhaKhoaCuoiKy.Helpers
{
    public class PatientHelper
    {
        public static DataTable getByID(int id)
        {
            DataTable dt = new DataTable();
            Database db = null;
            try
            {
                db = new Database();
                db.openConnection();
                using (SqlCommand cmd = new SqlCommand("getByID", db.getConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaBenhNhan", id);
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    dr.Close();
                }
                db.closeConnection();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (db != null) { db.closeConnection();}
            }
            return dt;
        }

        public static bool addNewRecord(int patientID, int staffID, string dentalDisease, string otherDisease, string symptoms, string result_, string diagnosis, string treatmentMethod, string nextAppointment, DateTime recordDate)
        {
            Database db = null;
            bool success = false;
            try
            {
                db = new Database();
                db.openConnection();
                using (SqlCommand cmd = new SqlCommand("AddNewRecord", db.getConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RecordID", 0);
                    cmd.Parameters.AddWithValue("@PatientID", patientID);
                    cmd.Parameters.AddWithValue("@StaffID", staffID);
                    cmd.Parameters.AddWithValue("@DentalDisease", dentalDisease);
                    cmd.Parameters.AddWithValue("@OtherDisease", otherDisease);
                    cmd.Parameters.AddWithValue("@Symptoms", symptoms);
                    cmd.Parameters.AddWithValue("@Result", result_);
                    cmd.Parameters.AddWithValue("@Diagnosis", diagnosis);
                    cmd.Parameters.AddWithValue("@TreatmentMethod", treatmentMethod);
                    cmd.Parameters.AddWithValue("@NextAppointment", nextAppointment);
                    cmd.Parameters.AddWithValue("@RecordDate", recordDate);

                    // Thực thi procedure và kiểm tra kết quả
                    if (cmd.ExecuteNonQuery() > 0) success = true;
                }
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ
                throw ex;
            }
            finally
            {
                if (db != null) db.closeConnection();
            }
            return success;
        }

    }
}
