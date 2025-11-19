using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Funtion
{
    public class EjemploFunciones
    {
        public string Funcion1(int num1, int num2, ref int num3)
        {
            num3 = num1 * num2;
            return (num1 + num2).ToString();
        }

        // ✅ Método estático: pertenece a la clase, no al objeto
        public static void SaludarEstatico(string nombre)
        {
            Console.WriteLine($"[Método estático] Hola {nombre}, bienvenido al curso de C#!");
        }

        // ✅ Método de instancia: pertenece al objeto, necesita ser instanciado
        public void SaludarInstancia(string nombre, out int variableSalida)
        {
            if (nombre.Length == 0)
            {
                variableSalida = nombre.Length;
                return;
            }
            variableSalida = nombre.Length;
            Console.WriteLine($"[Método de instancia] Hola {nombre}, gracias por participar en clase.");
        }
        public void EjemploMetodoGenericos<T>(T j, T k)
        {
            Console.WriteLine("Este es un ejemplo de método en C#. " + j.ToString(), k.ToString());
        }
    }
}
