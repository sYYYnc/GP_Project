using System;

namespace DBMProject.Models
{
    public class Coordenadas
    {


        public int CoordenadasId { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }

       
    }
}
