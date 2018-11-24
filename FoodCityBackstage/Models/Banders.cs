using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodCityBackstage.Models
{
    public class Banders
    {
        public int ID { get; set; }
        public string BannerTypeName { get; set; }
        public string BannderName { get; set; }
        public string Image { get; set; }
        public bool Isdefault { get; set; }
    }
}