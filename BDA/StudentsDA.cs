using BE.DTO;
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

                    //con.Close();
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
    }
}
