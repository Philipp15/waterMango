using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WaterMangoBE.Interfaces;

namespace WaterMangoBE.Models
{
    public class Plant : BaseModel, IPlant
    {
      
        public string State { get; set; }
        public string ImagePath { get; set; }
        public DateTime WateredTime { get; set; }

    }
}