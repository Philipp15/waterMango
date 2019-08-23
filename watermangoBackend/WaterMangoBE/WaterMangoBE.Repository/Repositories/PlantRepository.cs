using System;
using System.Collections.Generic;
using System.Text;
using WaterMangoBE.Models;
using WaterMangoBE.Repository.Interfaces.IRepositories;

namespace WaterMangoBE.Repository.Repositories
{
    public class PlantRepository : IPlantRepository
    {
        // !!! DB was not required
        private static List<Plant> _myPlants = new List<Plant>()
        {
            new Plant {ImagePath = "plant1.jpg", State = "Watered", WateredTime = DateTime.UtcNow.AddHours(-6).AddMinutes(-23).AddSeconds(-25)},
            new Plant {ImagePath = "plant2.jpg", State = "Watered", WateredTime = DateTime.UtcNow.AddHours(-6)},
            new Plant {ImagePath = "plant3.jpg", State = "Watered", WateredTime = DateTime.UtcNow.AddHours(-6)},
            new Plant {ImagePath = "plant4.jpg", State = "Watered", WateredTime = DateTime.UtcNow.AddHours(-5)},
            new Plant {ImagePath = "plant5.jpg", State = "Watered", WateredTime = DateTime.UtcNow.AddHours(-5) },
        };

        public List<Plant> GetPlants()
        {
            return _myPlants;  
        }

        public void UpdatePlant(Plant plant)
        {
            var plantToUpdate = _myPlants.Find(p => p.Id == plant.Id);
            if (plantToUpdate != null)
            {
                plantToUpdate.WateredTime = plant.WateredTime;
                plantToUpdate.State = plant.State;

            }
        }


    }
}
