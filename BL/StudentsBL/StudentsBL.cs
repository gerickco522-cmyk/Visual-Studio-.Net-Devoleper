using BDA;
using BE.DTO;
using BE.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
