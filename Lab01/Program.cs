
using Lab01.EjmplosOperadoresYvariables;
using System;

namespace Lab01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.Title = "Curso de C# Developer 🚀";
            //Console.ForegroundColor = ConsoleColor.Cyan;

            //Console.WriteLine("==========================================");
            //Console.WriteLine("     👋 Bienvenido al Curso de C# Developer");
            //Console.WriteLine("==========================================\n");

            //Console.ForegroundColor = ConsoleColor.Yellow;
            //Console.Write("Por favor, ingresa tu nombre: ");
            //Console.ForegroundColor = ConsoleColor.White;

            //var nombre = Console.ReadLine();

            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine($"\nHola {nombre}! Preparando tu entorno de aprendizaje...\n");

            //// Simular carga (pequeña animación)
            //Console.ForegroundColor = ConsoleColor.DarkCyan;
            //for (int i = 0; i < 5; i++)
            //{
            //    Console.Write(".");
            //    Thread.Sleep(400); // pausa de 0.4 segundos
            //}

            //Console.ForegroundColor = ConsoleColor.Magenta;
            //Console.WriteLine($"\n\n✅ Listo {nombre}! Comienza tu aventura en C#\n");

            //// Nueva sección: Interpolación de cadenas y formato de fecha/hora
            //Console.ForegroundColor = ConsoleColor.Blue;
            //var ahora = DateTime.Now;
            //Console.WriteLine($"Hola {nombre}, hoy es {ahora:dddd, dd MMMM yyyy} y la hora actual es {ahora:HH:mm:ss}.");
            //Console.WriteLine($"Ejemplo de interpolación: Tu nombre tiene {nombre.Length} caracteres.");

            //Console.ForegroundColor = ConsoleColor.Gray;
            //Console.WriteLine("Presiona cualquier tecla para salir...");
            //Console.ReadKey(true);
            string Mivariable = "1";

            Persona p = new Persona();
            
            p.Nombre = "Ana María";

            Persona q = new Persona();

            var cualLlamo = 2;
            //p.MostrarInfo();
            //p.MostrarOperadores();
            //p.MostrarOperadoresLogicos();
            //p.MostrarOperadoresDesplazamiento();+
            p.MetodoOperadorTerciaro(6);

            Console.WriteLine("\nPresiona una tecla para salir...");
            Console.ReadKey();
        }
    }

    namespace EjmplosOperadoresYvariables
    {
        #region Mi region de enumeradores 
        // Enumeraciones definidas fuera de la clase principal
        enum DiasSemana
        {
            Lunes, Martes, Miercoles, Jueves, Viernes, Sabado, Domingo
        }

        enum Colores
        {
            Negro, Blanco, Rojo, Verde, Azul
        }

        enum Meses
        {
            Enero, Febrero, Marzo, Abril, Mayo, Junio, Julio, Agosto,
            Septiembre, Octubre, Noviembre, Diciembre
        }

        enum Estado
        {
            Activo = 1,
            Inactivo = 2,
            Suspendido = 4
        }
        #endregion

        class Persona
        {
            // Campos y propiedades de distintos tipos
            public string Nombre = "Pedro";     // público → accesible desde fuera
            private int edad = 20;              // privado → solo dentro de la clase
            const int AñoActual = 2025;         // constante → no cambia nunca

            #region funciones de ejemplo
            public void MostrarInfo()
            {
                // variable implícita con 'var'
                var pais = "Perú";              // el compilador asume que es string

                // variable explícita con tipo
                string ciudad = "Lima";

                // cálculo usando constante y campo privado
                int anioNacimiento = AñoActual - edad;

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("===== Información Personal =====");
                Console.ResetColor();

                Console.WriteLine($"Nombre: {Nombre}");
                Console.WriteLine($"Edad: {edad}");
                Console.WriteLine($"Nació en: {anioNacimiento}");
                Console.WriteLine($"País: {pais}");
                Console.WriteLine($"Ciudad: {ciudad}");

                // Uso de enumeraciones
                DiasSemana diaFavorito = DiasSemana.Viernes;
                Colores colorFavorito = Colores.Azul;
                Meses mesFavorito = Meses.Julio;

                Console.WriteLine($"\nDía favorito: {diaFavorito}");
                Console.WriteLine($"Color favorito: {colorFavorito}");
                Console.WriteLine($"Mes favorito: {mesFavorito}");
                Estado estadoActual = Estado.Activo;
                Console.WriteLine($"Estado actual: {estadoActual} ({(int)estadoActual})");
            }

            public void MostrarOperadores()
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("===== DEMOSTRACIÓN DE OPERADORES EN C# =====");
                Console.ResetColor();

                // 🔹 Asignación (=)
                int a = 10;
                int b = 3;
                Console.WriteLine($"Asignación: a = {a}, b = {b}");

                // 🔹 Adición (+)
                int suma = a + b;
                Console.WriteLine($"Adición (+): {a} + {b} = {suma}");

                // 🔹 Sustracción (-)
                int resta = a - b;
                Console.WriteLine($"Sustracción (-): {a} - {b} = {resta}");

                // 🔹 Multiplicación (*)
                int multiplicacion = a * b;
                Console.WriteLine($"Multiplicación (*): {a} * {b} = {multiplicacion}");

                // 🔹 División (/)
                double division = (double)a / b;
                Console.WriteLine($"División (/): {a} / {b} = {division}");

                // 🔹 Módulo (%)
                int modulo = a % b;
                Console.WriteLine($"Módulo (%): {a} % {b} = {modulo}");

                // 🔹 Negación (!)
                bool esVerdadero = true;
                Console.WriteLine($"Negación (!): !{esVerdadero} = {!esVerdadero}");

                // 🔹 Operadores relacionales
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n-- Operadores relacionales --");
                Console.ResetColor();

                Console.WriteLine($"{a} > {b}  → {a > b}");
                Console.WriteLine($"{a} < {b}  → {a < b}");
                Console.WriteLine($"{a} >= {b} → {a >= b}");
                Console.WriteLine($"{a} <= {b} → {a <= b}");

                // 🔹 Operadores combinados (+=, -=, etc.)
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n-- Operadores combinados --");
                Console.ResetColor();

                int c = 5;
                Console.WriteLine($"Valor inicial de c: {c}");
                c += 2;
                Console.WriteLine($"c += 2 → {c}");
                c -= 1;
                Console.WriteLine($"c -= 1 → {c}");
                c *= 3;
                Console.WriteLine($"c *= 3 → {c}");
                c /= 2;
                Console.WriteLine($"c /= 2 → {c}");

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\nDemostración completada ✅");
                Console.ResetColor();
            }

            public void MostrarOperadoresLogicos()
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("===== DEMOSTRACIÓN DE OPERADORES LÓGICOS =====\n");
                Console.ResetColor();

                bool tieneCuenta = true;
                bool tieneSaldo = false;

                Console.WriteLine($"tieneCuenta = {tieneCuenta}");
                Console.WriteLine($"tieneSaldo = {tieneSaldo}\n");

                // 🔹 AND lógico (&&)
                bool puedeRetirar = tieneCuenta && tieneSaldo;
                Console.WriteLine($"AND lógico (&&): {tieneCuenta} && {tieneSaldo} = {puedeRetirar}");

                // 🔹 OR lógico (||)
                bool puedeVerSaldo = tieneCuenta || tieneSaldo;
                Console.WriteLine($"OR lógico (||): {tieneCuenta} || {tieneSaldo} = {puedeVerSaldo}");

                // 🔹 Negación lógica (!)
                bool noTieneSaldo = !tieneSaldo;
                Console.WriteLine($"Negación lógica (!): !{tieneSaldo} = {noTieneSaldo}");

                // 🔹 Ejemplo práctico 1: uso con if
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n-- Ejemplo práctico 1 --");
                Console.ResetColor();

                int edad = 25;
                bool tieneLicencia = true;

                if (edad != 18 && tieneLicencia)
                {
                    Console.WriteLine("✅ Puedes conducir un vehículo.");
                }
                else
                {
                    Console.WriteLine("❌ No cumples con los requisitos para conducir.");
                }

                // 🔹 Ejemplo práctico 2: OR lógico
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n-- Ejemplo práctico 2 --");
                Console.ResetColor();

                bool esAdmin = false;
                bool esInvitado = true;

                if (esAdmin || esInvitado)
                {
                    Console.WriteLine("🔓 Acceso permitido al sistema.");
                }
                else
                {
                    Console.WriteLine("🔒 Acceso denegado.");
                }

                // 🔹 Ejemplo práctico 3: Negación
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n-- Ejemplo práctico 3 --");
                Console.ResetColor();

                bool sistemaActivo = false;
                if (!sistemaActivo)
                {
                    Console.WriteLine("⚠️  El sistema está apagado. Enciéndelo para continuar.");
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nDemostración completada ✅");
                Console.ResetColor();
            }

            public void MostrarOperadoresDesplazamiento()
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("===== DEMOSTRACIÓN DE OPERADORES DE DESPLAZAMIENTO =====\n");
                Console.ResetColor();

                int numero = 5; // En binario: 0000 0101
                Console.WriteLine($"Número base: {numero} (binario: {Convert.ToString(numero, 2).PadLeft(8, '0')})\n");

                // 🔹 Desplazamiento a la izquierda <<
                int izquierda1 = numero << 1; // Multiplica por 2
                int izquierda2 = numero << 2; // Multiplica por 4

                Console.WriteLine($"Desplazamiento a la izquierda << 1 → {izquierda1} (binario: {Convert.ToString(izquierda1, 2).PadLeft(8, '0')})");
                Console.WriteLine($"Desplazamiento a la izquierda << 2 → {izquierda2} (binario: {Convert.ToString(izquierda2, 2).PadLeft(8, '0')})\n");

                // 🔹 Desplazamiento a la derecha >>
                int derecha1 = numero >> 1; // Divide entre 2
                int derecha2 = numero >> 2; // Divide entre 4

                Console.WriteLine($"Desplazamiento a la derecha >> 1 → {derecha1} (binario: {Convert.ToString(derecha1, 2).PadLeft(8, '0')})");
                Console.WriteLine($"Desplazamiento a la derecha >> 2 → {derecha2} (binario: {Convert.ToString(derecha2, 2).PadLeft(8, '0')})");

                // 🔹 Ejemplo visual de cómo los bits se mueven
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n-- Ejemplo visual --");
                Console.ResetColor();

                int valor = 1;
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine($"{valor,-2} → {Convert.ToString(valor, 2).PadLeft(8, '0')}");
                    valor = valor << 1;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nDemostración completada ✅");
                Console.ResetColor();
            }

            public void MetodoOperadorTerciaro(int datoEnteroParaComparar)
            {
                string mensaje = (edad >= 18)? "Eres mayor de edad ✅": "Eres menor de edad ❌";

                Console.WriteLine($"\n-- mensaje -- {mensaje} mas texto sin concatenar" );
                Console.WriteLine("\n-- mensaje --" + mensaje + " mas texto");
            }
            #endregion
        }
    }
}
