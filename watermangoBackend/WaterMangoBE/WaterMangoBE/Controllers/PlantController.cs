using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WaterMangoBe.ServiceLayer.Interfaces;
using WaterMangoBe.ServiceLayer.Services;
using WaterMangoBE.Models;
using WaterMangoBE.Repository.Interfaces.IRepositories;
using WaterMangoBE.Repository.Repositories;

namespace WaterMangoBE.Controllers
{
    public class PlantController : ApiController
    {
        private readonly IPlantService _plantService;
        private const int AlertSpanMilliseconds = 6 * 60 * 60 * 1000;

        public PlantController()
        {
            IPlantRepository plantRepository = new PlantRepository();
            _plantService = new PlantService(plantRepository);
        }

        public IEnumerable<Plant> Get()
        {
            return _plantService.GetPlants();
        }

        //[HttpPost]
        [HttpPost]
        [Route("api/Plant/UpdatePlant")]
        public void UpdatePlant([FromBody] Plant plant)
        {
            _plantService.UpdatePlant(plant);
        }

        [HttpGet]
        [Route("api/Plant/GetEarliestCheckOnPlantsTime")]
        public double GetEarliestCheckOnPlantsTime()
        {
            return _plantService.GetEarliestCheckUpOnPlants();
        }
    }
}
