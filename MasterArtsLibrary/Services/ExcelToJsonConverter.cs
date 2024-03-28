
using OfficeOpenXml;
using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using LicenseContext = OfficeOpenXml.LicenseContext;

public  class ExcelToJsonConverter
{
    public  string ConvertExcelToJson(string filePath)
    {
        // Säkerställ att EPPlus använder inte-kommersiell licens
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        var sb = new StringBuilder();
        using (var package = new ExcelPackage(new FileInfo(filePath)))
        {
            // Öppna bladet med namnet "Destinations"
            var worksheet = package.Workbook.Worksheets["Destinations"];
            if (worksheet == null)
            {
                throw new Exception("Kunde inte hitta ett arbetsblad med namnet 'Destinations'");
            }

            // Starta JavaScript-objektet
            sb.AppendLine("const locodes = {");

            // Anta att bladet "Destinations" har UN/LOCODE i kolumn B och stadsnamn i kolumn C
            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                var city = worksheet.Cells[row, 3].Text; // Stad i kolumn C
                var locode = worksheet.Cells[row, 2].Text; // UN/LOCODE i kolumn B
                sb.AppendLine($"    \"{city}\": \"{locode}\",");
            }

            // Avsluta JavaScript-objektet
            sb.AppendLine("};");
        }

        return sb.ToString();
    }
}

