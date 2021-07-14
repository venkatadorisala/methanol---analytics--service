using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using JM.Integration.Methanol.Services.Interface;
using JM.Integration.Methanol.Common.Enums;

namespace JM.Integration.Methanol.Services.Services
{
   public class FileExtensionValidationService : IFileExtensionValidationService
    {
        public bool IsValidExtension(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath);

            if (fileExtension.Equals($".{SupportedFileTypes.XLSX}", StringComparison.InvariantCultureIgnoreCase)
                ||
                fileExtension.Equals($".{SupportedFileTypes.XLSM}", StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }

            return false;
        }
        public bool IsValidFileSize(long fileSizeInBytes)
        {

            long fileSizeInMB = fileSizeInBytes / (1024 * 1024);
            if (fileSizeInMB < 5)
            {
                return true;
            }
            return false;
        }
    }
}
