using BE.DTO;
using BE.Request;
using BE.Response;
using BL.Estructura;
using BL.StudentsBL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class StudentsController : ControllerBase
    {
        [HttpGet("StudentsList")]
        public IActionResult StudentsList()
        {
            Response<List<Students>> response = new();
            try
            {
                StudentsBL studentsBL = new();
                response = studentsBL.GetStudents();
                response.status = 200;
                response.TotalRowCount = response.data != null ? response.data.Count : 0;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }

        [HttpPost("StudentsListFilter")]
        public IActionResult StudentsListFilter([FromBody] StudentsRequest students)
        {
            Response<List<Students>> response = new();
            try
            {
                StudentsBL studentsBL = new();
                response = studentsBL.GetStudents(students);
                response.status = 200;
                response.TotalRowCount = response.data != null ? response.data.Count : 0;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }

        [HttpPost("InsertStudents")]
        public IActionResult InsertStudents([FromBody] StudentsInsertRequest students)
        {
            Response<List<Students>> response = new();
            try
            {
                StudentsBL studentsBL = new();
                response = studentsBL.InsertStudents(students);
                response.status = 200;
                response.TotalRowCount = response.data != null ? response.data.Count : 0;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }
        [HttpPut("UpdateStudent")]
        public IActionResult UpdateStudent([FromBody] Students students)
        {
            Response<List<Students>> response = new();
            try
            {
                StudentsBL studentsBL = new();
                response = studentsBL.UpdateStudent(students);
                response.status = 200;
                response.TotalRowCount = response.data != null ? response.data.Count : 0;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }

        [HttpDelete("DeleteStudents")]
        public IActionResult DeleteStudents([FromBody] Students students)
        {
            Response<List<Students>> response = new();
            try
            {
                StudentsBL studentsBL = new();
                response = studentsBL.DeleteStudents(students);
                response.status = 200;
                response.TotalRowCount = response.data != null ? response.data.Count : 0;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }

        [HttpPost("InsertStudentsBulk")]
        public IActionResult InsertStudentsBulk([FromBody] List<StudentsInsertRequest> students)
        {
            Response<List<Students>> response = new();
            try
            {
                StudentsBL studentsBL = new();
                response = studentsBL.InsertStudentsBulk(students);
                response.status = 200;
                response.TotalRowCount = response.data != null ? response.data.Count : 0;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }

        [HttpPost("ImpresionPDf")]
        [AllowAnonymous]
        public IActionResult ImpresionPDf([FromBody] StudentsRequest students)
        {
            Response<string> response = new();
            try
            {
                StudentsBL studentsBL = new();
                var studentsResponse = studentsBL.GetStudents(students);
                GenerarPdf generarPdf = new();
                response.data = generarPdf.GenerarDocumentoBase64(studentsResponse.data);
                response.status = 200;
                response.TotalRowCount = studentsResponse.data != null ? studentsResponse.data.Count : 0;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }
    }
}
