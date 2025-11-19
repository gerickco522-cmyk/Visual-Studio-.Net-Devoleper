using BE.DTO;
using BL.Funtion;
using Microsoft.AspNetCore.Mvc;
using BE.Request;
using BE.Response;
using BL.Services;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetMessage([FromQuery] RequestInfo datas)
        {
            var message = new
            {
                Message = "Bienvenido a mi API 😎",
                Fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Version = "1.0"
            };

            var mensaje2 = new ApiInfo
            {
                Message1 = "Bienvenido a mi API nuevo texto 😎",
                Fecha1 = DateTime.Now.AddDays(10).ToString("MM-dd HH:mm:ss"),
                Version1 = "5.1"
            };

            return Ok(mensaje2);
        }

        public class ApiInfo
        {
            public string Message1 { get; set; } = "Bienvenido a mi API 😎";
            public string Fecha1 { get; set; }
            public string Version1 { get; set; }
        }

        //[HttpPost]
        //public IActionResult pruebaNuevaClase([FromBody] Models.ApiResulInfo datos)
        //{
        //    datos.fechaActaul = DateTime.Now;
        //    return Ok(datos);
        //}

        [HttpPost("EjmplosNuevasClases")]
        public IActionResult pruebaNuevaClase([FromBody] RequestInfo request)
        {
            #region ejemplo switch
            var dia = DateTime.Now.AddDays(1).DayOfWeek;

            switch (dia)
            {
                case DayOfWeek.Monday:
                    Console.WriteLine("Inicio de semana.");
                    break;

                case DayOfWeek.Friday:
                    Console.WriteLine("Casi fin de semana.");
                    break;

                case DayOfWeek.Saturday:
                case DayOfWeek.Sunday:
                    Console.WriteLine("Fin de semana 😎");
                    break;

                default:
                    Console.WriteLine("Día normal.");
                    break;
            }
            #endregion

            #region ejemplo if, else, if else

            int nota = (int)request.NotaUsuario;

            if (nota >= 90)
            {
                Console.WriteLine("Excelente 👏");
            }
            else if (nota >= 75)
            {
                Console.WriteLine("Muy bien 👍");
            }
            else if (nota >= 60)
            {
                Console.WriteLine("Aprobado 😌");
            }
            else
            {
                Console.WriteLine("Reprobado 😞");
            }
            #endregion

            #region ejemplo while y do while
            int contador = 1;

            while (contador <= 5)
            {
                Console.WriteLine($"Contador: {contador}");
                contador++; // importante: evitar bucle infinito
            }
            #endregion

            #region do while 
            int numero;

            Console.WriteLine("Ejemplo de DO...WHILE:");
            do
            {
                Console.Write("Ingrese un número (0 para salir): ");
                numero = request.Edad;
                Console.WriteLine($"Ingresaste: {numero}\n");
                numero = 0;

            } while (numero != 0); // Se repite mientras NO sea 0
            #endregion

            try
            {
                var nuevaClase = new ApiResulInfo
                {
                    Message1 = "Bienvenido a mi API 😎",
                    Fecha1 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    Version1 = "1.0",
                    Codigo = 200,

                    Etiquetas = new List<string> { "API", "C#", ".NET", "Ejemplo" },

                    InfoExtra = new List<DatosExtra> {
                        new DatosExtra
                        {
                            Autor = request.Usuario,
                            Descripcion = request.MostrarFechaFutura.ToString(),
                            Libros = request.Edad.ToString(),
                            Nivel = 10
                        },
                        new DatosExtra{
                            Autor = "Erick Gonzalez",
                            Descripcion = "Esta es una API de ejemplo creada en ASP.NET Core.",
                            Libros = "C# Avanzado",
                            Nivel = 1
                        },
                        new DatosExtra{
                            Autor = "Pedro Perez",
                            Descripcion = "Prueba 2",
                            Libros = "ASP.NET Core",
                            Nivel = 8
                        },
                        new DatosExtra
                        {
                            Autor = "Maria Lopez",
                            Descripcion = "Prueba 3",
                            Libros = "Entity Framework Core",
                            Nivel = 5
                        }
                    },
                };

                #region ejemplo de for 
                for (int i = 0; i <= 3; i++)
                {
                    Console.WriteLine("Mi variable del for vale" + i.ToString());
                }
                #endregion

                #region ejemplo foreach
                List<DatosExtra> ejeForeach = nuevaClase.InfoExtra;
                foreach (var item in ejeForeach)
                {
                    Console.WriteLine("El autor es: " + item.Autor);
                }


                #endregion

                #region ejemplo uso de la funcion
                EjemploFunciones BL = new EjemploFunciones();
                int variableRefencia = 0;
                string resultadoFuncion = BL.Funcion1(request.Edad, (int)request.NotaUsuario, ref variableRefencia);
                Console.WriteLine("El resultado de la funcion es: " + resultadoFuncion);
                Console.WriteLine("El resultado de la variable de refencia es: " + variableRefencia.ToString());
                #endregion

                #region ejemplo uso de metodo estatico
                EjemploFunciones.SaludarEstatico(request.Usuario);
                #endregion

                #region ejemplo uso de metodo de instancia
                //EjemploFunciones instancia = new EjemploFunciones();
                var resultadoSalida = 0;
                BL.SaludarInstancia(request.Usuario, out resultadoSalida);
                #endregion

                #region ejemploMetodoGenetiro
                BL.EjemploMetodoGenericos<string>(request.Edad.ToString(), request.Usuario);
                #endregion

                return Ok(nuevaClase);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al procesar la solicitud.", Details = ex.Message });
            }
        }

        [HttpPost("CrearPersona")]
        public IActionResult CrearPersona([FromBody] RequestInfo request)
        {
            
            try
            {
                throw new NotImplementedException("Mensaje", new Exception("Mas mensaje"));
                // Se usa la clase con encapsulación
                var persona = new Persona(request.Usuario, request.Edad);

                return Ok(new
                {
                    mensaje = "Persona creada correctamente",
                    descripcion = persona.ObtenerDescripcion()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.InnerException.Message});
            }
        }

        [HttpPost("CrearOrden")]
        public IActionResult crearOrden([FromBody] CrearOrdenRequestDTO request)
        {
            SuccessResponse<CrearOrdenResponseDTO> response = null;
            CLaseEjecucionMetodosAbstractos cLaseEjecucionMetodosAbstractos = new CLaseEjecucionMetodosAbstractos();
            cLaseEjecucionMetodosAbstractos.implementarNotificaciones();
            try
            {
                //throw new NotImplementedException("Error en llamada a servicio");
                var service = new WebApplication1Services();
                CrearOrdenResponseDTO respuesta = service.CrearOrden(request);
                response = new SuccessResponse<CrearOrdenResponseDTO>(respuesta)
                {
                    data = respuesta,
                    message = "Orden creada exitosamente",
                    statusCode = 200,
                    TotalRowCount = 1,
                    VariableBase1 = "ValorBase1",
                    VariableBase2 = "ValorBase2"
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }
    }
}
