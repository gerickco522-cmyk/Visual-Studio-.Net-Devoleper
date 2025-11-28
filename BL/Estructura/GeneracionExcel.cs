using BE.DTO;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Estructura
{
    public class GeneracionExcel
    {
        private static string? _folderPath;
        private static string? _excelFolder;
        public static void Initialize(IConfiguration configuration)
        {
            _folderPath = configuration["LogSettings:FolderPath"];
            _excelFolder = configuration["LogSettings:ExcelFolderPath"];

            if (string.IsNullOrWhiteSpace(_folderPath))
                throw new Exception("LogSettings:FolderPath is missing in appsettings.json");

        }
        public byte[] GenerarExcel(List<Students> lista)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var package = new ExcelPackage();
            var ws = package.Workbook.Worksheets.Add("Estudiantes");

            ws.Cells["A1"].Value = "StudentId";
            ws.Cells["B1"].Value = "FirstName";
            ws.Cells["C1"].Value = "LastName";
            ws.Cells["D1"].Value = "Email";
            ws.Cells["E1"].Value = "BirthDate";
            ws.Cells["F1"].Value = "CreatedAt";

            using (var range = ws.Cells["A1:F1"])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(80, 245, 39)); // azul
                range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }

            int row = 2;
            foreach (var s in lista)
            {
                ws.Cells[row, 1].Value = s.StudentId;
                ws.Cells[row, 2].Value = s.FirstName;
                ws.Cells[row, 3].Value = s.LastName;
                ws.Cells[row, 4].Value = s.Email;
                ws.Cells[row, 5].Value = s.BirthDate?.ToString("yyyy-MM-dd");
                ws.Cells[row, 5].Style.Numberformat.Format = "yyyy-mm-dd";
                ws.Cells[row, 6].Value = s.CreatedAt?.ToString("yyyy-MM-dd HH:mm:ss");
                ws.Cells[row, 6].Style.Numberformat.Format = "yyyy-mm-dd HH:mm:ss";
                row++;

            }

            // ===========================
            // BORDES DE LA TABLA
            // ===========================
            var dataRange = ws.Cells[$"A1:F{row - 1}"];
            dataRange.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            dataRange.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            dataRange.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            dataRange.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            // ===========================
            // AUTOFIT Y ESTÉTICA
            // ===========================
            ws.Cells[ws.Dimension.Address].AutoFitColumns();
            ws.View.FreezePanes(2, 1); // congela la fila de encabezados
            ws.Cells["A1:F1"].AutoFilter = true; // filtros en encabezados


            if (!Directory.Exists(_excelFolder))
                Directory.CreateDirectory(_excelFolder);

            var bytes = package.GetAsByteArray();
            string fileName = $"Estudiantes-{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            string fullPath = Path.Combine(_excelFolder!, fileName);
            File.WriteAllBytes(fullPath, bytes);



            string base64Excel = Convert.ToBase64String(bytes);
            return bytes;
        }


    }
}
