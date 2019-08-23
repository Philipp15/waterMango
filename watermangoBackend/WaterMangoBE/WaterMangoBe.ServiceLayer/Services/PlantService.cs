using System;
using System.Collections.Generic;
using System.Linq;
using WaterMangoBe.ServiceLayer.Interfaces;
using WaterMangoBE.Models;
using WaterMangoBE.Repository.Interfaces.IRepositories;

namespace WaterMangoBe.ServiceLayer.Services
{
    public class PlantService : IPlantService
    {
        private readonly IPlantRepository _plantRepository;

        private const int AlertDyingHours = 6;
        private const int AlertDyingSeconds = 6 * 60 * 60;

        public PlantService(IPlantRepository plantRepository)
        {
            _plantRepository = plantRepository;
        }

        public List<Plant> GetPlants()
        {
            var plants = _plantRepository.GetPlants();
            CheckState(plants);
            return plants;
        }

        private void CheckState(List<Plant> plants)
        {
            foreach (var plant in plants)
            {
                //Avoid rounding problems e.g Totals Seconds 21600.989
                if ( Convert.ToInt32(Math.Ceiling((DateTime.UtcNow - plant.WateredTime).TotalSeconds)) > AlertDyingHours * 60 * 60)
                {
                    plant.State = "Dying";
                    UpdatePlant(plant);
                }
            }
        }

        public void UpdatePlant(Plant plant)
        {
            _plantRepository.UpdatePlant(plant);
        }

        public int GetEarliestCheckUpOnPlants()
        {
            
            var plants = _plantRepository.GetPlants();
            CheckState(plants);
            var weakestWateredPlant = plants.Where(p => p.State == "Watered").OrderBy(plant => plant.WateredTime).FirstOrDefault();

            if (weakestWateredPlant != null)
            {
                //milliseconds is too granular and can cause overflow
                //INT max val = 2,147,483,647 which in seconds is 68 years
                var secondsToRecheck = Convert.ToInt32(Math.Ceiling((DateTime.UtcNow - weakestWateredPlant.WateredTime).TotalSeconds));
                
                var secondsLeft = AlertDyingSeconds - secondsToRecheck;

                if (secondsLeft < 0)
                {
                    return 1; //Check now
                }
                else
                {
                    return secondsLeft;
                }
            }

            return -1; //All plants are dying -> do not recheck again until one gets watered
        }
    }
}
