// See https://aka.ms/new-console-template for more information

using BL.Funtion;

Console.Title = "Laboratorio 2 - C# Developer (.NET 6)";
int opcion = 0;

do
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("\n===== MENÚ DE LABORATORIOS =====");
    Console.ResetColor();
    Console.WriteLine("1. Lab 2.1 - Multiplicar dos números");
    Console.WriteLine("2. Lab 2.2 - Mostrar el mayor número ingresado");
    Console.WriteLine("3. Lab 2.3 - Repetir hasta ingresar el número 5");
    Console.WriteLine("4. Salir");
    Console.Write("\nSeleccione una opción: ");

    if (int.TryParse(Console.ReadLine(), out opcion))
    {
        Console.Clear();
        switch (opcion)
        {
            case 1:
                Lab_2_1_Multiplicar();
                break;
            case 2:
                Lab_2_2_MayorNumero();
                break;
            case 3:
                Lab_2_3_HastaCinco();
                break;
            case 4:
                Console.WriteLine("👋 Saliendo del programa...");
                break;
            default:
                Console.WriteLine("⚠️  Opción no válida, intenta nuevamente.");
                break;
        }
    }
    else
    {
        Console.WriteLine("⚠️  Ingresa un número válido.");
    }

} while (opcion != 4);


// 🔹 LAB 2.1 – Multiplicar dos números
void Lab_2_1_Multiplicar()
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("=== LAB 2.1 - Multiplicar dos números ===\n");
    Console.ResetColor();

    Console.Write("Ingrese el primer número: ");
    double num1 = Convert.ToDouble(Console.ReadLine());

    Console.Write("Ingrese el segundo número: ");
    double num2 = Convert.ToDouble(Console.ReadLine());

    double resultado = num1 * num2;
    Console.WriteLine($"\nEl resultado de {num1} x {num2} = {resultado}");
}


// 🔹 LAB 2.2 – Mostrar el mayor número ingresado
void Lab_2_2_MayorNumero()
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("=== LAB 2.2 - Mostrar el mayor número ===\n");
    Console.ResetColor();

    Console.Write("Ingrese el primer número: ");
    double a = Convert.ToDouble(Console.ReadLine());

    Console.Write("Ingrese el segundo número: ");
    double b = Convert.ToDouble(Console.ReadLine());

    double mayor = (a > b) ? a : b; // Operador ternario
    Console.WriteLine($"\nEl número mayor es: {mayor}");
}


// 🔹 LAB 2.3 – Programa que no finaliza hasta ingresar "5"
void Lab_2_3_HastaCinco()
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("=== LAB 2.3 - No finalizar hasta ingresar 5 ===\n");
    Console.ResetColor();

    int numero;
    do
    {
        Console.Write("Ingrese un número (5 para salir): ");
        numero = Convert.ToInt32(Console.ReadLine());
    } while (numero != 5);

    Console.WriteLine("\n✅ Has ingresado 5. Programa finalizado.");

    EjemploFunciones ejemplo = new EjemploFunciones();
}