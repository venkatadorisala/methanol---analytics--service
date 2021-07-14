using JM.Integration.Methanol.Services.Interface;
using JM.Integration.Methanol.DB.Models;
using JM.Integration.Methanol.Services.Models;
using JM.Integration.Methanol.DB.Models;
using JM.Integration.Methanol.Services;
using System.Threading.Tasks;
using Moq;
using Xunit;
using JM.Integration.Common.Interfaces;
using NPOI.SS.Formula.Functions;
using System.IO;
using System.Text;

namespace JM.Integration.Methanol.Services.UnitTests
{
    public class PreliminaryCheckUploadedFileTest:FunctionTest
    {
        private readonly Mock<IExcelFileContentExtractorService> _excelFileContentExtractorService;
        private readonly Mock<IFileExtensionValidationService> _fileExtensionValidationService;
        private readonly Mock<IProcessDetailService> _processDetailService;
        private readonly Mock<IBlobService> _blobService;
        private readonly Mock<IPlantService> _plantService;
        private readonly PreliminaryCheckUploadedFileV1 _preliminaryCheckUploadedFileV1;
       
        public PreliminaryCheckUploadedFileTest()
        {
            _excelFileContentExtractorService = new Mock<IExcelFileContentExtractorService>();
            _fileExtensionValidationService = new Mock<IFileExtensionValidationService>();
            _processDetailService = new Mock<IProcessDetailService>();
            _blobService = new Mock<IBlobService>();
            _plantService = new Mock<IPlantService>();
            _preliminaryCheckUploadedFileV1 = new PreliminaryCheckUploadedFileV1(_fileExtensionValidationService.Object, _excelFileContentExtractorService.Object, _processDetailService.Object, _blobService.Object,_plantService.Object);
        }



        [Fact]
        public async Task Test_Asyn_PreCheck_Queue()
        {
            //Arrange
            
            var blobName = "testBlob.xlsm";
            PreCheckExcelDataModel objPreCheckExcelDataModel = new PreCheckExcelDataModel { PlantName= "YanChang", TemplateVersion="1" };
            
            MasterTemplate masterTemplate = new MasterTemplate { Sid = 1, PlantPlantSid = "1" };
            DataTemplate dataTemplate = new DataTemplate { MasterTemplateSid = 1, TemplateVersion = "1.0" };
            masterTemplate.DataTemplates.Add(dataTemplate);

            Plant objplant = new Plant { PlantSid = "1", PlantName = "YanChang"  };
            objplant.MasterTemplates.Add(masterTemplate);

            ProcessDetail objProcessDetail = new ProcessDetail { Sid = 1, PlantPlantSid = "1",PlantPlantS=objplant };
          
            string fileContent = "upload sample data";
            var fileStream = new MemoryStream(Encoding.Default.GetBytes(fileContent));

            var mockService = new Mock<IFileExtensionValidationService>();
            _fileExtensionValidationService.Setup(x => x.IsValidExtension(blobName)).Returns(true);

         
            _blobService.Setup(x => x.GetBlobStream(It.IsAny<string>())).Returns(fileStream);
         
            _excelFileContentExtractorService.Setup(x => x.GetFileContent(fileStream)).Returns(objPreCheckExcelDataModel);
            _plantService.Setup(x => x.GetPlantDetails(It.IsAny<string>())).Returns(objplant);
            _processDetailService.Setup(x => x.GetProcessDetailsByPlantName(objPreCheckExcelDataModel.PlantName, blobName)).Returns(objProcessDetail);
            _processDetailService.Setup(x => x.GetMasterTemplateByPlantSid(It.IsAny<string>())).Returns(masterTemplate);
            _processDetailService.Setup(x => x.GetProcessDetailsByPlantSid(It.IsAny<string>())).Returns(objProcessDetail);
            //act

            await _preliminaryCheckUploadedFileV1.Run(blobName, log);

            //assert at lease once process details are updated.
            
            _processDetailService.Verify(pd => pd.Update(It.IsAny<ProcessDetail>()), Times.AtLeastOnce());
            



        }

       


    }

}
    
