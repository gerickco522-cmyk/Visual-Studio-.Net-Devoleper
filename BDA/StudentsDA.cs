using BE.DTO;
using BE.Request;
using BE.Response;
using System.Data;
using System.Data.SqlClient;

namespace BDA
{
    public class StudentsDA
    {
        public Response<List<Students>> GetStudents()
        {
            Response<List<Students>> response = new();
            List<Students>? students;
            try
            {
                using (SqlConnection con = new(DB.ConnectionString))
                using (SqlCommand cmd = new("sp_GetStudents", con) { CommandType = CommandType.StoredProcedure })
                {
                    con.Open();
                    cmd.Parameters.Clear();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ParseHelper parseHelper = new();
                        students = parseHelper.DataReaderMapToList<Students>(reader);
                    }
                    con.Close();
                    response.message = students.Count > 0 ? "Successful Consultation. " : "Error De comunicacion";
                }
            }
            catch (SqlException e)
            {
                response.message += "Excepción SQL " + e.Number + ": " + e.Message;
                Console.WriteLine("!-- Excepción SQL " + e.Number + ": " + e.Message);
                students = null;
            }
            catch (Exception e)
            {
                response.message += "Excepción NET " + e.Message;
                Console.WriteLine("!-- Excepción .NET: " + e.Message);
                students = null;
            }

            response.data = students;
            return response;
        }

        public Response<List<Students>> GetStudents(StudentsRequest studentsRequest)
        {
            Response<List<Students>> response = new();
            List<Students>? students;

            using (SqlConnection con = new(DB.ConnectionString)) 
            using (SqlCommand cmd = new("sp_GetStudentsFilteredPage", con) { CommandType = CommandType.StoredProcedure })
            {
                con.Open();
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@StudentId", studentsRequest.StudentId > 0 ? studentsRequest.StudentId : DBNull.Value);
                cmd.Parameters.AddWithValue("@Name", string.IsNullOrEmpty(studentsRequest.Name) ? DBNull.Value : studentsRequest.Name);
                cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(studentsRequest.Email) ? DBNull.Value : studentsRequest.Email);
                cmd.Parameters.AddWithValue("@FromDate", studentsRequest.FromDate.HasValue ? studentsRequest.FromDate.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@ToDate", studentsRequest.ToDate.HasValue ? studentsRequest.ToDate.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@PageSize", studentsRequest.PageSize);
                cmd.Parameters.AddWithValue("@PageNumber", studentsRequest.PageNumber);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    ParseHelper parseHelper = new();
                    students = parseHelper.DataReaderMapToList<Students>(reader);
                }
                con.Close();
                response.message = students.Count > 0 ? "Successful Consultation. " : "Sin registros";
            }

            response.data = students;
            return response;
        }

        public Response<List<Students>> InsertStudents(StudentsInsertRequest StudentsInsertRequest)
        {
            Response<List<Students>> response = new();
            List<Students>? students;

            using (SqlConnection con = new(DB.ConnectionString))
            using (SqlCommand cmd = new("sp_InsertStudent", con) { CommandType = CommandType.StoredProcedure })
            {
                con.Open();
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@FirstName", StudentsInsertRequest.FirstName);
                cmd.Parameters.AddWithValue("@LastName",string.IsNullOrEmpty(StudentsInsertRequest.LastName)? DBNull.Value: StudentsInsertRequest.LastName); // Assuming last name is not provided in StudentsRequest
                cmd.Parameters.AddWithValue("@Email", StudentsInsertRequest.Email);
                cmd.Parameters.AddWithValue("@BirthDate", StudentsInsertRequest.BirthDate.HasValue ? StudentsInsertRequest.BirthDate.Value : DBNull.Value);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    ParseHelper parseHelper = new();
                    students = parseHelper.DataReaderMapToList<Students>(reader);
                }
                con.Close();
                response.message = students.Count > 0 ? "Successful Consultation. " : "Sin registros";
            }

            response.data = students;
            return response;
        }

        public Response<List<Students>> UpdateStudent(Students studentsUpdate)
        {
            Response<List<Students>> response = new();
            List<Students>? students;

            using (SqlConnection con = new(DB.ConnectionString))
            using (SqlCommand cmd = new("sp_UpdateStudent", con) { CommandType = CommandType.StoredProcedure })
            {
                con.Open();
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@StudentId", studentsUpdate.StudentId);
                cmd.Parameters.AddWithValue("@FirstName", studentsUpdate.FirstName);
                cmd.Parameters.AddWithValue("@LastName", string.IsNullOrEmpty(studentsUpdate.LastName) ? DBNull.Value : studentsUpdate.LastName); // Assuming last name is not provided in StudentsRequest
                cmd.Parameters.AddWithValue("@Email", studentsUpdate.Email);
                cmd.Parameters.AddWithValue("@BirthDate", studentsUpdate.BirthDate.HasValue ? studentsUpdate.BirthDate.Value : DBNull.Value);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    ParseHelper parseHelper = new();
                    students = parseHelper.DataReaderMapToList<Students>(reader);
                }
                con.Close();
                response.message = students.Count > 0 ? "Successful Consultation. " : "Sin registros";
            }

            response.data = students;
            return response;
        }

        public Response<List<Students>> DeleteStudent(Students studentsUpdate)
        {
            Response<List<Students>> response = new();
            List<Students>? students;

            using (SqlConnection con = new(DB.ConnectionString))
            using (SqlCommand cmd = new("sp_DeleteStudent", con) { CommandType = CommandType.StoredProcedure })
            {
                con.Open();
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@StudentId", studentsUpdate.StudentId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    ParseHelper parseHelper = new();
                    students = parseHelper.DataReaderMapToList<Students>(reader);
                }
                con.Close();
                response.message = students.Count > 0 ? "Successful Consultation. " : "Sin registros";
            }

            response.data = students;
            return response;
        }

        public Response<List<Students>> InsertStudentsBulk(List<StudentsInsertRequest> studentsRequest)
        {
            Response<List<Students>> response = new();
            List<Students> students = new();

            using (SqlConnection con = new(DB.ConnectionString))
            using (SqlCommand cmd = new("sp_InsertStudentsBulk", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Crear DataTable para TVP
                DataTable dt = new();
                dt.Columns.Add("FirstName", typeof(string));
                dt.Columns.Add("LastName", typeof(string));
                dt.Columns.Add("Email", typeof(string));
                dt.Columns.Add("BirthDate", typeof(DateTime));

                foreach (var s in studentsRequest)
                {
                    dt.Rows.Add(
                        s.FirstName,
                        string.IsNullOrEmpty(s.LastName) ? DBNull.Value : s.LastName,
                        s.Email,
                        (object?)s.BirthDate ?? DBNull.Value
                    );
                }

                // Agregar parámetro tipo tabla
                SqlParameter tvpParam = cmd.Parameters.AddWithValue("@Students", dt);
                tvpParam.SqlDbType = SqlDbType.Structured;
                tvpParam.TypeName = "StudentsInsertTableType";

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    ParseHelper parseHelper = new();
                    students = parseHelper.DataReaderMapToList<Students>(reader);
                }
                con.Close();
            }

            response.data = students;
            response.message = "Inserción masiva completada correctamente.";
            return response;
        }
    }
}
