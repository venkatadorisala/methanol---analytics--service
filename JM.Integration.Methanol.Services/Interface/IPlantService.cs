using System.Collections.Generic;
using System.Threading.Tasks;
using JM.Integration.Methanol.DB.Models;


namespace JM.Integration.Methanol.Services.Interface
{
    public interface IPlantService
    {
        Plant GetPlantDetails(string plantName);
    }
}
