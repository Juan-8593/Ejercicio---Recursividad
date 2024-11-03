using System;

namespace Recursividad
{
    internal class Programa
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese la ruta de la carpeta:");
            string rutaBase = Console.ReadLine();

            if (!Directory.Exists(rutaBase))
            {
                Console.WriteLine($"La ruta '{rutaBase}' no es válida.");
                return;
            }

            int totalElementos = ContarElementos(rutaBase);
            Console.WriteLine($"Total de archivos y carpetas en '{rutaBase}': {totalElementos}");

            Console.WriteLine("Especifique la extensión de archivo a buscar:");
            string Buscarextension = Console.ReadLine();
            Console.WriteLine($"Archivos con la extensión '{Buscarextension}':");
            BuscarArchivosTipo(rutaBase, Buscarextension);
        }

        static int ContarElementos(string ruta)
        {
            int total = 0;

            try
            {
                string[] archivos = Directory.GetFiles(ruta);
                total += archivos.Length;

                string[] subdirectorios = Directory.GetDirectories(ruta);
                total += subdirectorios.Length;

                foreach (string subdirectorio in subdirectorios)
                {
                    total += ContarElementos(subdirectorio);
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"Acceso restringido: {ruta}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al acceder a '{ruta}': {ex.Message}");
            }

            return total;
        }

        static void BuscarArchivosTipo(string ruta, string extension)
        {
            try
            {
                string[] archivos = Directory.GetFiles(ruta, "*" + extension);
                foreach (var archivo in archivos)
                {
                    Console.WriteLine(archivo);
                }

                string[] subdirectorios = Directory.GetDirectories(ruta);
                foreach (string subdirectorio in subdirectorios)
                {
                    BuscarArchivosTipo(subdirectorio, extension);
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"Acceso restringido: {ruta}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al acceder a '{ruta}': {ex.Message}");
            }
        }
    }
}
