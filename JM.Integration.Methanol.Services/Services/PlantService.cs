using JM.Integration.Methanol.Services.Interface;
using JM.Integration.Methanol.DB.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JM.Integration.Methanol.Services.Services
{
    public class PlantService : IPlantService
    {

        private readonly LevoMethanolDBContext _levoMethanolDBContext;

        public PlantService(LevoMethanolDBContext lEVOTSContext)
        {
            _levoMethanolDBContext = lEVOTSContext;
        }

        public Plant GetPlantDetails(string plantName)
        {
            var plant = _levoMethanolDBContext.Plants.FirstOrDefault(x => x.PlantName == plantName);
            return plant;



        }

        public async Task<Plant> GetPlantsAsync(string plantSid, string plantName)
        {
            var plant = _levoMethanolDBContext.Plants.FirstOrDefaultAsync(x => x.PlantSid == plantSid && x.PlantName == plantName);
            return await plant.ConfigureAwait(false);
        }

       

    }
}
