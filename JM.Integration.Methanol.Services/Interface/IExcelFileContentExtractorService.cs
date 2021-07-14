using System.IO;
using JM.Integration.Methanol.Services.Models;

namespace JM.Integration.Methanol.Services.Interface
{
   public interface IExcelFileContentExtractorService
    {
        PreCheckExcelDataModel GetFileContent(Stream fileStream);
    }
}
