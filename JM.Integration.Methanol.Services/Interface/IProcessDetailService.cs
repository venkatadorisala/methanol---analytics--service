using JM.Integration.Methanol.DB.Models;

namespace JM.Integration.Methanol.Services.Interface
{

    /// <summary>
    /// Contains methods for upload process details services implementation 
    /// </summary>
    public interface IProcessDetailService
    {
        /// <summary>
        /// Fetching from current process details 
        /// Based on searching filename
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>Returns current upload file process details</returns>
        ProcessDetail GetByFileName(string fileName);

        /// <summary>
        /// Updates the current upload file processing details
        /// </summary>
        /// <param name="processDetail"></param>
        /// <returns>Returns process details updated status</returns>
        bool Update(ProcessDetail processDetail);

        /// <summary>
        /// Getting current uploaded file process details based on Plant id
        /// </summary>
        /// <param name="plantSid"></param>
        /// <returns>Returns process details </returns>
        ProcessDetail GetProcessDetailsByPlantName(string plantName, string fileName);

        /// <summary>
        /// Get Master template by plantSid
        /// </summary>
        /// <param name="plantSid"></param>
        /// <returns></returns>
        MasterTemplate GetMasterTemplateByPlantSid(string plantSid);

        /// <summary>
        /// Get ProcessDetail by plantSid
        /// </summary>
        /// <param name="plantSid"></param>
        /// <returns></returns>
        ProcessDetail GetProcessDetailsByPlantSid(string plantSid);

    }
}
