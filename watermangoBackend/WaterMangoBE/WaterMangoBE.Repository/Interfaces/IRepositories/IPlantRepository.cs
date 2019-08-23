using System;
using System.Collections.Generic;
using System.Text;
using WaterMangoBE.Models;

namespace WaterMangoBE.Repository.Interfaces.IRepositories
{
    public interface IPlantRepository : IRepository
    {
        List<Plant> GetPlants();

        void UpdatePlant(Plant plantId);

    }
}
