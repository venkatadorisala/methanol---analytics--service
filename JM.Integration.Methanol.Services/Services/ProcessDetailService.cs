using JM.Integration.Methanol.Services.Interface;
using JM.Integration.Methanol.DB.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace JM.Integration.Methanol.Services.Services
{
    /// <inheritdoc cref="IProcessDetailService"/>
    public class ProcessDetailService : IProcessDetailService
    {
        private readonly LevoMethanolDBContext _lEVOTSContext;

        public ProcessDetailService(LevoMethanolDBContext lEVOTSContext)
        {
            _lEVOTSContext = lEVOTSContext;
        }

        public ProcessDetail GetByFileName(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                var processDetail = _lEVOTSContext.ProcessDetails.OrderByDescending(x => x.Sid).FirstOrDefault(x => x.UploadFileName.ToUpper().Equals(fileName.ToUpper()));
                return processDetail;
            }
            return null;
        }

        public MasterTemplate GetMasterTemplateByPlantSid(string plantSid)
        {
            var masterTemplate = _lEVOTSContext.MasterTemplates.Include(x => x.DataTemplates).OrderByDescending(x => x.Sid).FirstOrDefault(x => x.PlantPlantSid == plantSid);
            return masterTemplate;
        }

        public ProcessDetail GetProcessDetailsByPlantName(string plantName, string fileName)
        {
            var processDetail = _lEVOTSContext.ProcessDetails.Include(x=>x.PlantPlantS).OrderByDescending(x => x.Sid).FirstOrDefault(x => x.PlantPlantS.PlantName == plantName && x.UploadFileName == fileName);
            return processDetail;
        }

        public bool Update(ProcessDetail processDetail)
        {
            _lEVOTSContext.ProcessDetails.Update(processDetail);
            var savedCount = _lEVOTSContext.SaveChanges();
            return savedCount > 0;
        }


        public ProcessDetail GetProcessDetailsByPlantSid(string plantSid)
        {
            if (!string.IsNullOrEmpty(plantSid))
            {
                var processDetail = _lEVOTSContext.ProcessDetails.OrderByDescending(x => x.Sid).FirstOrDefault(x => x.PlantPlantSid == plantSid);
                return processDetail;
            }
            return null;
        }

    }
}
