using BE.DTO;
using BE.Response;
using BL.StudentsBL;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
                response.statusCode = 200;
                response.TotalRowCount = response.data != null ? response.data.Count : 0;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }
    }
}
