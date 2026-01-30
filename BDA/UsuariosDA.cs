using BE.DTO;
using BE.Request;
using BE.Response;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDA.Estructura;
using System.Text.RegularExpressions;

namespace BDA
{
    public class UsuariosDA
    {
        public Response<List<Usuarios>> GetUser(Usuarios usuariosRequest)
        {
            Response<List<Usuarios>> response = new();
            List<Usuarios>? usuarios;
            AesCryptoService aesCryptoService = new();

            using (SqlConnection con = new(DB.ConnectionString))
            using (SqlCommand cmd = new("sp_LoginUser", con) { CommandType = CommandType.StoredProcedure })
            {
                con.Open();
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Username", string.IsNullOrEmpty(usuariosRequest.username)?  DBNull.Value:usuariosRequest.username);
                cmd.Parameters.AddWithValue("@PasswordHash", string.IsNullOrEmpty(usuariosRequest.passwordHash) ? DBNull.Value : aesCryptoService.MD5Encrypt(usuariosRequest.passwordHash));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    ParseHelper parseHelper = new();
                    usuarios = parseHelper.DataReaderMapToList<Usuarios>(reader);
                }
                con.Close();
                response.message = usuarios.Count > 0 ? "Successful Consultation. " : "Sin registros";
            }

            response.data = usuarios;
            return response;
        }

        public Response<List<Usuarios>> InsertUsuarios(Usuarios usuariosRequest)
        {
            Response<List<Usuarios>> response = new();
            List<Usuarios>? students;
            AesCryptoService aesCryptoService = new();

            using (SqlConnection con = new(DB.ConnectionString))
            using (SqlCommand cmd = new("sp_InsertUser", con) { CommandType = CommandType.StoredProcedure })
            {
                con.Open();
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Username", usuariosRequest.username);
                cmd.Parameters.AddWithValue("@Email", usuariosRequest.email);
                cmd.Parameters.AddWithValue("@PasswordHash", aesCryptoService.MD5Encrypt(usuariosRequest.passwordHash));

                // Extraer la dirección de correo
                // Extraer la dirección de correo
                string emailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                if (!Regex.IsMatch(usuariosRequest.email, emailRegex))
                    throw new ArgumentException("Falta de formato del correo electrónico", nameof(usuariosRequest.email));


                // Validar contraseña (tu regex solo permite mayúsculas A-Z)
                string passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
                if (!Regex.IsMatch(usuariosRequest.passwordHash, passwordRegex))
                    throw new ArgumentException("La contraseña debe tener al menos 8 caracteres, mayúsculas, minúsculas, número y carácter especial");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    ParseHelper parseHelper = new();
                    students = parseHelper.DataReaderMapToList<Usuarios>(reader);
                }
                con.Close();
                response.message = students.Count > 0 ? "Insert Successful. " : "Sin registros";
            }

            response.data = students;
            return response;
        }
    }
}
