using BDA;
using BE.DTO;
using BE.Request;
using BE.Response;

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
            var ListStudents = new Response<List<Students>>();
            ListStudents = studentsDA.GetStudents(students);
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
