using BDA;
using BE.DTO;
using BE.Request;
using BE.Response;
using BL.Estructura;
using Microsoft.Extensions.Configuration;

namespace BL.StudentsBL
{
    public class StudentsBL
    {
        public Response<List<Students>> GetStudents()
        {
            StudentsDA studentsDA = new();
            var ListStudents = new Response<List<Students>>();
            ListStudents = studentsDA.GetStudents();
            return ListStudents;
        }

        public Response<List<Students>> GetStudents(StudentsRequest students)
        {
            StudentsDA studentsDA = new();
            Log log = new();
            GeneracionExcel generacionExcel = new();
            GenerarPdf generarPdf = new();
             //ListStudents = new Response<List<Students>>();
            var ListStudents = studentsDA.GetStudents(students);
            //Task.Run(() =>log.WriteLog("Ejecución de servicio de lesctura de estudiantes filtrados"));
            generacionExcel.GenerarExcel(ListStudents.data);
            generarPdf.GenerarDocumento(ListStudents.data);
            return ListStudents;
        }

        public Response<List<Students>> InsertStudents(StudentsInsertRequest students)
        {
            StudentsDA studentsDA = new();
            Response<List<Students>>  ListStudents = studentsDA.InsertStudents(students);
            return ListStudents;
        }
        public Response<List<Students>> UpdateStudent (Students students)
        {
            StudentsDA studentsDA = new();
            Response<List<Students>> ListStudents = studentsDA.UpdateStudent(students);
            return ListStudents;
        }
        public Response<List<Students>> DeleteStudents(Students students)
        {
            StudentsDA studentsDA = new();
            Response<List<Students>> ListStudents = studentsDA.DeleteStudent(students);
            return ListStudents;
        }
        public Response<List<Students>> InsertStudentsBulk(List<StudentsInsertRequest> students)
        {
            StudentsDA studentsDA = new();
            Response<List<Students>> ListStudents = studentsDA.InsertStudentsBulk(students);
            return ListStudents;
        }
    }
}
