using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using JM.Integration.Methanol.Services.Extensions;
using JM.Integration.Methanol.Services.Interface;
using JM.Integration.Methanol.Services.Models;
using NPOI.SS.UserModel;


namespace JM.Integration.Methanol.Services.Services
{
    public class ExcelFileContentExtractorService : IExcelFileContentExtractorService
    {
        public PreCheckExcelDataModel GetFileContent(Stream fileStream)
        {
            IDictionary<string, string> cellValues = new Dictionary<string, string>();
            IWorkbook workbook = WorkbookFactory.Create(fileStream);
            ISheet reportSheet = workbook.GetSheetAt(0);

            if (reportSheet != null)
            {
                int rowCount = reportSheet.LastRowNum + 1;
                for (int i = 0; i < rowCount; i++)
                {
                    IRow row = reportSheet.GetRow(i);
                    if (row != null)
                    {
                        foreach (var cell in row.Cells)
                        {
                            var cellValue = cell.GetFormattedCellValue();
                            if (!string.IsNullOrEmpty(cellValue))
                            {
                                cellValues.Add(cell.Address.FormatAsString(), cellValue);
                            }
                        }
                    }
                }
            }

            return new PreCheckExcelDataModel()
            {
                PlantName = cellValues["C5"]??"",
                 TemplateVersion= cellValues["C6"]??"",
                //PlantSid = cellValues["C7"]??""
            };
        }

        
    }
}
