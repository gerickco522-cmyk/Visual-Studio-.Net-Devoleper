using BE.DTO;
using Microsoft.Extensions.Configuration;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Estructura
{
    public class GenerarPdf
    {
        private static string? _pdffolder;
        public static void Initialize(IConfiguration configuration)
        {
            _pdffolder = configuration["LogSettings:PdfFolderPath"];

            if (string.IsNullOrWhiteSpace(_pdffolder))
                throw new Exception("LogSettings:FolderPath is missing in appsettings.json");

        }
        public void GenerarDocumento(List<Students> students)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Lista de estudiantes";

            PdfPage page = document.AddPage();          
            page.Orientation = PdfSharp.PageOrientation.Landscape;

            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont font = new XFont("Arial", 16, XFontStyle.Regular);
            XFont fontTitle = new XFont("Arial", 20, XFontStyle.Bold);
            XFont fontRow = new XFont("Arial", 11, XFontStyle.Regular);

            gfx.DrawString("Lista de estudiantes", fontTitle, XBrushes.Black, new XRect(0, 30, page.Width, 40), XStringFormats.TopCenter);
            gfx.DrawString("Este es un ejemplo de generación de un documento PDF utilizando PDFsharp en C#.", 
                font, XBrushes.Black, new XRect(40, 100, page.Width - 80, page.Height - 100), XStringFormats.TopLeft);

            int startY = 120;
            int startX = 40;
            int rowHeight = 25;

            int colId = 20;
            int colFirstName = 100;
            int colLastName = 100;
            int colEmail = 200;
            int colBirthDate = 150;
            int colCreatedAt = 150;

            gfx.DrawString("ID", fontRow, XBrushes.Red, new XRect(startX, startY, colId, rowHeight), XStringFormats.Center);
            gfx.DrawString("First Name", fontRow, XBrushes.Red, new XRect(startX + colId, startY, colFirstName, rowHeight), XStringFormats.Center); 
            gfx.DrawString("Last Name", fontRow, XBrushes.Red, new XRect(startX + colId + colFirstName, startY, colLastName, rowHeight), XStringFormats.Center);
            gfx.DrawString("Email", fontRow, XBrushes.Red, new XRect(startX + colId + colFirstName + colLastName, startY, colEmail, rowHeight), XStringFormats.Center);
            gfx.DrawString("Birth Date", fontRow, XBrushes.Red, new XRect(startX + colId + colFirstName + colLastName + colEmail, startY, colBirthDate, rowHeight), XStringFormats.Center);
            gfx.DrawString("Created At", fontRow, XBrushes.Red, new XRect(startX + colId + colFirstName + colLastName + colEmail + colBirthDate, startY, colCreatedAt, rowHeight), XStringFormats.Center);

            startY += rowHeight;

            foreach (var student in students)
            {
                gfx.DrawString(student.StudentId.ToString(), fontRow, XBrushes.Black, new XRect(startX, startY, colId, rowHeight), XStringFormats.Center);
                gfx.DrawString(student.FirstName ?? "", fontRow, XBrushes.Black, new XRect(startX + colId, startY, colFirstName, rowHeight), XStringFormats.Center);
                gfx.DrawString(student.LastName ?? "", fontRow, XBrushes.Black, new XRect(startX + colId + colFirstName, startY, colLastName, rowHeight), XStringFormats.Center);
                gfx.DrawString(student.Email ?? "", fontRow, XBrushes.Black, new XRect(startX + colId + colFirstName + colLastName, startY, colEmail, rowHeight), XStringFormats.Center);
                gfx.DrawString(student.BirthDate?.ToString("yyyy-MM-dd") ?? "", fontRow, XBrushes.Black, new XRect(startX + colId + colFirstName + colLastName + colEmail, startY, colBirthDate, rowHeight), XStringFormats.Center);
                gfx.DrawString(student.CreatedAt?.ToString("yyyy-MM-dd HH:mm:ss") ?? "", fontRow, XBrushes.Black, new XRect(startX + colId + colFirstName + colLastName + colEmail + colBirthDate, startY, colCreatedAt, rowHeight), XStringFormats.Center);

                gfx.DrawLine(XPens.Black, startX, startY, page.Width - startX, startY);
                startY += rowHeight;

                if (startY + rowHeight > page.Height - 40)
                {
                    page = document.AddPage();
                    page.Orientation = PdfSharp.PageOrientation.Landscape;
                    gfx = XGraphics.FromPdfPage(page);
                    startY = 40;
                }
            }

            startY += 20;
            gfx.DrawLine(XPens.Black, startX, startY, page.Width - startX, startY);
            startY += 10;
            gfx.DrawString($"Total de estudiantes: {students.Count}", font, XBrushes.Black, new XRect(startX, startY, page.Width - 2 * startX, rowHeight), XStringFormats.TopLeft);


            if (!Directory.Exists(_pdffolder))
                Directory.CreateDirectory(_pdffolder);

            string fileName = $"Estudiantes-{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            string fullPath = Path.Combine(_pdffolder!, fileName);

            document.Save(fullPath);
            document.Close();
        }

        public string GenerarDocumentoBase64(List<Students> students)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Lista de estudiantes";

            PdfPage page = document.AddPage();
            page.Orientation = PdfSharp.PageOrientation.Landscape;

            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont font = new XFont("Arial", 16, XFontStyle.Regular);
            XFont fontTitle = new XFont("Arial", 20, XFontStyle.Bold);
            XFont fontRow = new XFont("Arial", 11, XFontStyle.Regular);

            gfx.DrawString("Lista de estudiantes", fontTitle, XBrushes.Black, new XRect(0, 30, page.Width, 40), XStringFormats.TopCenter);
            gfx.DrawString("Este es un ejemplo de generación de un documento PDF utilizando PDFsharp en C#.",
                font, XBrushes.Black, new XRect(40, 100, page.Width - 80, page.Height - 100), XStringFormats.TopLeft);

            int startY = 120;
            int startX = 40;
            int rowHeight = 25;

            int colId = 20;
            int colFirstName = 100;
            int colLastName = 100;
            int colEmail = 200;
            int colBirthDate = 150;
            int colCreatedAt = 150;

            gfx.DrawString("ID", fontRow, XBrushes.Red, new XRect(startX, startY, colId, rowHeight), XStringFormats.Center);
            gfx.DrawString("First Name", fontRow, XBrushes.Red, new XRect(startX + colId, startY, colFirstName, rowHeight), XStringFormats.Center);
            gfx.DrawString("Last Name", fontRow, XBrushes.Red, new XRect(startX + colId + colFirstName, startY, colLastName, rowHeight), XStringFormats.Center);
            gfx.DrawString("Email", fontRow, XBrushes.Red, new XRect(startX + colId + colFirstName + colLastName, startY, colEmail, rowHeight), XStringFormats.Center);
            gfx.DrawString("Birth Date", fontRow, XBrushes.Red, new XRect(startX + colId + colFirstName + colLastName + colEmail, startY, colBirthDate, rowHeight), XStringFormats.Center);
            gfx.DrawString("Created At", fontRow, XBrushes.Red, new XRect(startX + colId + colFirstName + colLastName + colEmail + colBirthDate, startY, colCreatedAt, rowHeight), XStringFormats.Center);

            startY += rowHeight;

            foreach (var student in students)
            {
                gfx.DrawString(student.StudentId.ToString(), fontRow, XBrushes.Black, new XRect(startX, startY, colId, rowHeight), XStringFormats.Center);
                gfx.DrawString(student.FirstName ?? "", fontRow, XBrushes.Black, new XRect(startX + colId, startY, colFirstName, rowHeight), XStringFormats.Center);
                gfx.DrawString(student.LastName ?? "", fontRow, XBrushes.Black, new XRect(startX + colId + colFirstName, startY, colLastName, rowHeight), XStringFormats.Center);
                gfx.DrawString(student.Email ?? "", fontRow, XBrushes.Black, new XRect(startX + colId + colFirstName + colLastName, startY, colEmail, rowHeight), XStringFormats.Center);
                gfx.DrawString(student.BirthDate?.ToString("yyyy-MM-dd") ?? "", fontRow, XBrushes.Black, new XRect(startX + colId + colFirstName + colLastName + colEmail, startY, colBirthDate, rowHeight), XStringFormats.Center);
                gfx.DrawString(student.CreatedAt?.ToString("yyyy-MM-dd HH:mm:ss") ?? "", fontRow, XBrushes.Black, new XRect(startX + colId + colFirstName + colLastName + colEmail + colBirthDate, startY, colCreatedAt, rowHeight), XStringFormats.Center);

                gfx.DrawLine(XPens.Black, startX, startY, page.Width - startX, startY);
                startY += rowHeight;

                if (startY + rowHeight > page.Height - 40)
                {
                    page = document.AddPage();
                    page.Orientation = PdfSharp.PageOrientation.Landscape;
                    gfx = XGraphics.FromPdfPage(page);
                    startY = 40;
                }
            }

            startY += 20;
            gfx.DrawLine(XPens.Black, startX, startY, page.Width - startX, startY);
            startY += 10;
            gfx.DrawString($"Total de estudiantes: {students.Count}", font, XBrushes.Black, new XRect(startX, startY, page.Width - 2 * startX, rowHeight), XStringFormats.TopLeft);


            if (!Directory.Exists(_pdffolder))
                Directory.CreateDirectory(_pdffolder);

            string fileName = $"Estudiantes-{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            string fullPath = Path.Combine(_pdffolder!, fileName);

            using (var stream = new MemoryStream())
            {
                document.Save(fullPath);
                document.Close();
                byte[] pdfBytes = File.ReadAllBytes(fullPath);
                return Convert.ToBase64String(pdfBytes);
            }   
        }
    }
}
