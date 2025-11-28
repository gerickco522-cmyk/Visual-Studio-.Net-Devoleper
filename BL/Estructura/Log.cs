using BE.DTO;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;

namespace BL.Estructura
{
    public class Log
    {
        private static string? _folderPath;
        private static string? _filePath;

        public static void Initialize(IConfiguration configuration)
        {
            _folderPath = configuration["LogSettings:FolderPath"];

            if (string.IsNullOrWhiteSpace(_folderPath))
                throw new Exception("LogSettings:FolderPath is missing in appsettings.json");

            _filePath = Path.Combine(_folderPath, "log");
        }
        public void WriteLog(string mensaje)
        {
            //var logGuid = _filePath+ Guid.NewGuid().ToString() +".txt";
            var logFecha = _filePath + "-" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
            // Crear carpeta si no existe
            if (!Directory.Exists(_folderPath))
                Directory.CreateDirectory(_folderPath);

            // Crear archivo si no existe
            //if (!File.Exists(logGuid))
            //    File.Create(logGuid).Close();

            if (!File.Exists(logFecha))
                File.Create(logFecha).Close();

            // Escribir mensaje
            string linea = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {mensaje}{Environment.NewLine}";
            //File.AppendAllText(logGuid, linea);
            File.AppendAllText(logFecha, linea);
        }

        
    }
}


