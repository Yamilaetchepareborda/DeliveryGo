using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryGo.Core.Utils
{
    public class Validador
    {
        public static string PedirTexto(string mensaje)
        {
            string input;
            do
            {
                Console.Write(mensaje);
                input = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(input))
                    Console.WriteLine("Este campo es obligatorio, intente nuevamente");

            }while(string.IsNullOrEmpty(input));
            return input;
        }

        public static decimal PedirDecimal(string mensaje)
        {
            decimal valor;
            string input;
            do
            {
                Console.Write(mensaje);
                input = Console.ReadLine();

                if (!decimal.TryParse(input, out valor) || valor <= 0)
                    Console.WriteLine("Ingrese un valor valido, mayor a 0 \n");

            } while (valor<=0);

            return valor;   
        }

        public static int PedirEntero(string mensaje)
        {
            int valor;
            string input;
            do
            {
                Console.Write(mensaje);
                input = Console.ReadLine();
                if(!int.TryParse(input, out valor) && valor <= 0)
                    Console.WriteLine("Ingrese un valor valido, debe ser mayo a 0\n");
            }while (valor<=0);

            return valor;
        }


        //  Pregunta sí/no
        public static bool PedirConfirmacion(string mensaje)
        {
            string input;
            do
            {
                Console.Write(mensaje + " (s/n): ");
                input = Console.ReadLine()?.Trim().ToLower();

                if (input != "s" && input != "n")
                    Console.WriteLine(" Ingrese 's' o 'n'.\n");

            } while (input != "s" && input != "n");

            return input == "s";
        }
    }
}
