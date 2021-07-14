using System;
namespace JM.Integration.Methanol.Services.Interface
{
    public interface IFileExtensionValidationService
    {
        bool IsValidExtension(string filePath);
        bool IsValidFileSize(long ffileSizeInBytes);
    }
}
