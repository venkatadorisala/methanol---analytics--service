using JM.Integration.Methanol.DB.Models;
using JM.Integration.Methanol.Services.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace JM.Methanol.UnitTests.Services
{
    /// <summary>
    /// Mocking and testing process details service methods
    /// </summary>
    public class ProcessDetailServiceTests
    {

        private readonly ProcessDetailService _processDetails;
        private readonly Mock<LevoMethanolDBContext> _levodbContext;

        public ProcessDetailServiceTests()
        {

            var options = new DbContextOptionsBuilder<LevoMethanolDBContext>().Options;
            _levodbContext = new Mock<LevoMethanolDBContext>(options);

             var processDetailsMock = CreateDbSetMock(GetFakeListOfProcessDetails());
            _levodbContext.Setup(m => m.ProcessDetails).Returns(processDetailsMock.Object);
            _processDetails = new ProcessDetailService(_levodbContext.Object);

        }

        [Theory]
        [InlineData("JM-LEVO_Upload_v5.1_plantname_12032020.xlsm")]
        [InlineData("jm-LEVO_Upload_v5.1_plantname_12032020.xlsm")]
        [InlineData("JM-LEVO_UPLOAD_v5.1_PLANTNAME_12032020.XLSM")]
        public void Should_Return_ProcessDetail_When_Searching_With_CurrentProcess_File(string blobName)
        {
            // Act
            var processDetail = _processDetails.GetByFileName(blobName);

            // Assert
            Assert.NotNull(processDetail);
            Assert.Equal(blobName.ToUpper(), processDetail.UploadFileName.ToUpper());
        }

        [Theory]
        [InlineData("JM-LEVO_Upload_v5.1_plantname_12032000.xlsm")]
        [InlineData(".xlsm")]
        [InlineData(null)]
        [InlineData("")]
        public void Should_Return_Null_When_Searching_With_NotCurrentProcess_File(string blobName)
        {
            // Act
            var processDetail = _processDetails.GetByFileName(blobName);

            // Assert
            Assert.Null(processDetail);
        }

        [Theory]
        [InlineData("Success in AV Scan",4)]
        public void Should_Return_UpdatedCount_When_Updating_CurrentProcess_File(string avScanStatus, int sid)
        {
            //Arrange
            var updatedProcessDetail = new ProcessDetail { Sid = sid,PlantPlantSid="1" ,UploadFileName = "JM-LEVO_Upload_v5.1_plantname_14032020.xlsm", HistoryRevFlag = "Y", AvScanStatus = avScanStatus };
            _levodbContext.Setup(m => m.SaveChanges()).Returns(1);

            // Act
            var isRecordUpdated = _processDetails.Update(updatedProcessDetail);

            // Assert
            Assert.True(isRecordUpdated);
        }

        [Theory]
        [InlineData("Success in AV Scan",-1)]
        public void Should_Return_UpdatedCount_Zero_When_Updating_CurrentProcess_File(string avScanStatus,int sid)
        {
            //Arrange
            var updatedProcessDetail = new ProcessDetail {Sid=sid, UploadFileName = "JM-LEVO_Upload_v5.1_plantname_14032020.xlsm", HistoryRevFlag = "Y", AvScanStatus = avScanStatus };
            _levodbContext.Setup(m => m.SaveChanges()).Returns(0);

            // Act
            var isRecordUpdated = _processDetails.Update(updatedProcessDetail);

            // Assert
            Assert.False(isRecordUpdated);
        }


        private static Mock<DbSet<T>> CreateDbSetMock<T>(IEnumerable<T> elements) where T : class
        {
            var elementsAsQueryable = elements.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());

            return dbSetMock;
        }

        private IEnumerable<ProcessDetail> GetFakeListOfProcessDetails()
        {
            var processDetails = new List<ProcessDetail>()
            {
                new ProcessDetail {Sid = 1, UploadFileName = "JM-LEVO_Upload_v5.1_plantname_12032020.xlsm",HistoryRevFlag="Y"},
                new ProcessDetail {Sid = 2, UploadFileName = "JM-LEVO_Upload_v5.1_plantname_12032020.xlsm",HistoryRevFlag="Y"},
                new ProcessDetail {Sid = 3, UploadFileName = "JM-LEVO_Upload_v5.1_plantname_13032020.xlsm",HistoryRevFlag="Y"},
                new ProcessDetail {Sid = 4, UploadFileName = "JM-LEVO_Upload_v5.1_plantname_14032020.xlsm",HistoryRevFlag="Y"}
            };

            return processDetails;
        }

    }
}
