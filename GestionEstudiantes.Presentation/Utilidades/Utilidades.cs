using GestionEstudiantes.Datos.Comun;

namespace GestionEstudiantes.Presentacion.Utilidades
{
    /// <summary>
    /// Clase con métodos utilitarios para validación de entradas
    /// </summary>
    public static class UtilidadesConsola
    {
        /// <summary>
        /// Lee un texto no vacío desde la consola con validación
        /// </summary>
        public static string LeerTextoNoVacio(string mensaje)
        {
            string texto;
            do
            {
                Console.Write(mensaje);
                texto = Console.ReadLine()!;

                if (string.IsNullOrWhiteSpace(texto))
                {
                    Console.WriteLine("Error: El texto no puede estar vacío. Intente nuevamente.");
                }
            } while (string.IsNullOrWhiteSpace(texto));

            return texto.Trim();
        }

        /// <summary>
        /// Lee un número entero desde la consola con validación
        /// </summary>
        public static int LeerEntero(string mensaje, int minimo = int.MinValue, int maximo = int.MaxValue)
        {
            int numero;
            bool entradaValida;

            do
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine()!;

                entradaValida = int.TryParse(entrada, out numero);

                if (!entradaValida)
                {
                    Console.WriteLine("Error: Debe ingresar un número entero válido.");
                }
                else if (numero < minimo || numero > maximo)
                {
                    Console.WriteLine($"Error: El número debe estar entre {minimo} y {maximo}.");
                    entradaValida = false;
                }
            } while (!entradaValida);

            return numero;
        }

        /// <summary>
        /// Lee un número decimal (double) desde la consola con validación
        /// </summary>
        public static double LeerDouble(string mensaje, double minimo = double.MinValue, double maximo = double.MaxValue)
        {
            double numero;
            bool entradaValida;

            do
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine()!;

                entradaValida = double.TryParse(entrada, out numero);

                if (!entradaValida)
                {
                    Console.WriteLine("Error: Debe ingresar un número válido.");
                }
                else if (numero < minimo || numero > maximo)
                {
                    Console.WriteLine($"Error: El número debe estar entre {minimo} y {maximo}.");
                    entradaValida = false;
                }
            } while (!entradaValida);

            return numero;
        }

        /// <summary>
        /// Pausa la ejecución hasta que el usuario presione una tecla
        /// </summary>
        public static void PausarConsola(string mensaje = "\nPresione cualquier tecla para continuar...")
        {
            Console.WriteLine(mensaje);
            Console.ReadKey();
        }

        /// <summary>
        /// Limpia la consola
        /// </summary>
        public static void LimpiarConsola()
        {
            Console.Clear();
        }

        /// <summary>
        /// Muestra un encabezado formateado
        /// </summary>
        public static void MostrarEncabezado(string titulo)
        {
            Console.WriteLine("\n" + new string('═', 80));
            Console.WriteLine(titulo.ToUpper().PadLeft((80 + titulo.Length) / 2));
            Console.WriteLine(new string('═', 80));
        }

        /// <summary>
        /// Muestra el resultado de una operación
        /// </summary>
        public static void MostrarResultadoOperacion(OperationResult resultado)
        {
            if (resultado.Success)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"✓ {resultado.Message}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"✗ {resultado.Message}");
                Console.ResetColor();
            }
        }
    }
}
