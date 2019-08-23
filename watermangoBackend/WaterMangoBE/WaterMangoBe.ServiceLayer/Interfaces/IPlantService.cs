using System;
using System.Collections.Generic;
using System.Text;
using WaterMangoBE.Models;

namespace WaterMangoBe.ServiceLayer.Interfaces
{
    public interface IPlantService : IService
    {
        List<Plant> GetPlants();

        void UpdatePlant(Plant plant);

        int GetEarliestCheckUpOnPlants();
    }
}
