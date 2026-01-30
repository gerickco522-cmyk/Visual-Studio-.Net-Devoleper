using BDA;
using BE.DTO;
using BE.Request;
using BE.Response;
using BL.Estructura;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.UsuariosBL
{
    public class UsuarioBL
    {
        public Response<List<Usuarios>> InsertStudents(Usuarios request)
        {
            UsuariosDA usuariosDA = new();
            var ListUsuarios = usuariosDA.InsertUsuarios(request);
            return ListUsuarios;
        }

        public Response<List<Usuarios>> GetUsuarios(Usuarios request)
        {
            UsuariosDA usuariosDA = new();
            var ListUsuarios = usuariosDA.GetUser(request);
            if ((ListUsuarios.data?[0]?.id??0) > 0)
            {
                ListUsuarios.data[0].token = new TokenService().GenerateJWT(ListUsuarios.data[0]);
            } 
            return ListUsuarios;
        }
    }
}
