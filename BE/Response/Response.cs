using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace BE.Response
{
    public class Response<T>
    {
        public string? message { get; set; }
        public T? data { get; set; }
        public int? TotalRowCount { get; set; }
        public int? status { get; set; }

    }

    public class SuccessResponse<T> : Response<T> 
    {
        public string VariableBase1 { get; set; }
        public string VariableBase2 { get; set; }
        public bool IsSuccessful { get; set; } = true;
        public SuccessResponse(T data)
        {
            this.data = data;
        }
    }

    public class terceraHerencia
    {
        public string Propiedad1 { get; set; }
        public int Propiedad2 { get; set; }
    }

    public class segundaHerencia : terceraHerencia 
    {
        public string Propiedad3 { get; set; }
        public int Propiedad4 { get; set; }
    }

    public class primeraHerancia : segundaHerencia 
    {
        public string Propiedad5 { get; set; }
        public int Propiedad6 { get; set; }
    }


    public abstract class Notificacion
    {
        public string Destinatario { get; set; }

        public abstract void Enviar();
    }

    public class NotificacionEmail : Notificacion
    {
        public override void Enviar()
        {
            Console.WriteLine($"Enviando email a {Destinatario}...");
        }
    }

    public class NotificacionSMS : Notificacion
    {
        public override void Enviar()
        {
            Console.WriteLine($"Enviando SMS a {Destinatario}...");
        }
    }



    public class Autenticador
    {
        public virtual void Autenticar(int tipo, string textoEncode)
        {
            Console.WriteLine("Autenticando usuario de forma genérica...");
        }
    }

    public class AutenticacionPassword : Autenticador
    {
        public override void Autenticar(int tipo,string textoEncode)
        {
            Console.WriteLine("Autenticando mediante contraseña.");
            return;
        }
    }

    public class AutenticacionHuella : Autenticador
    {
        public override void Autenticar(int tipo,string textoEncode)
        {
            Console.WriteLine("Autenticando mediante huella digital.");
        }
    }

    public class AutenticacionFaceID : Autenticador
    {
        public override void Autenticar(int tipo, string textoEncode)
        {
            Console.WriteLine("Autenticando mediante reconocimiento facial.");
        }
    }


    public class CLaseEjecucionMetodosAbstractos
    {
        public void implementarNotificaciones()
        {
            Notificacion n1 = new NotificacionEmail { Destinatario = "cliente@mail.com" };
            Notificacion n2 = new NotificacionSMS { Destinatario = "3001234567" };

            List<Autenticador> metodos = new List<Autenticador>()
            {
                new AutenticacionPassword(),
                new AutenticacionHuella(),
                new AutenticacionFaceID()
            };

            n1.Enviar();
            n2.Enviar();

            foreach (var metodo in metodos)
            {
                metodo.Autenticar(3,"Texto codigficado");
            }
        }
    }
}
