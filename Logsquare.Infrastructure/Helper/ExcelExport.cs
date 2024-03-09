using Logsquare.Application.Common.Interfaces;
using Logsquare.Domain;
using Logsquare.Dto;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Logsquare.Infrastructure.Helper
{
    public class ExcelExport : IExcelExport
    {
        public async Task<byte[]> ExportFileAsync(List<User> data, string fileName, ExportUserDto exportUserDto)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                // Add a new worksheet
                var worksheet = package.Workbook.Worksheets.Add(fileName);

                // Add column headers
                int dataCount = 0;
                dataCount = data.Count();

                Type type = typeof(ExportUserDto);

                for (int i = 0; i < type.GetProperties().Count(); i++)
                {
                    worksheet.Cells[1, i + 1].Value = type.GetProperties()[i].Name;
                }

                // Add user data to the worksheet
                int row = 2;
                for (int i = 0; i < data.Count(); i++)
                {
                    for (int j = 0; j < type.GetProperties().Count(); j++)
                    {
                        worksheet.Cells[row, j + 1].Value = data[i].GetType().GetProperty(type.GetProperties()[j].Name).GetValue(data[i]);
                    }
                    row++;
                }

                // Auto-fit columns
                worksheet.Cells.AutoFitColumns();

                // Convert Excel package to a byte array
                byte[] excelFileContents = package.GetAsByteArray();

                // Return the Excel file as a downloadable file
                return excelFileContents;
            }
        }
    }
}
