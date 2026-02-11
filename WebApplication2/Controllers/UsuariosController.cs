using BE.DTO;
using BE.Request;
using BE.Response;
using BL.StudentsBL;
using BL.UsuariosBL;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        [HttpPost("UserListFilter")]
        public IActionResult UserListFilter([FromBody] Usuarios request)
        {
            Response<List<Usuarios>> response = new();
            try
            {
                UsuarioBL usuarioBL= new();
                response = usuarioBL.GetUsuarios(request);
                response.status = 200;
                response.TotalRowCount = response.data != null ? response.data.Count : 0;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }

        [HttpPost("InsertUser")]
        public IActionResult InsertUser([FromBody] Usuarios request)
        {
            Response<string> response = new();
            try
            {
                UsuarioBL usuarioBL = new();
                var responseBase = usuarioBL.InsertStudents(request);
                response.message = responseBase.message;
                response.status = 200;
                response.TotalRowCount = 1;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }
    }
}
