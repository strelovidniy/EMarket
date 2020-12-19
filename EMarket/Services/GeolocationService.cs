using System.Collections.Generic;

namespace EMarket.Services
{
    public class GeolocationService : IGeolocationService
    {
        private List<string> geolocations = new List<string>()
        {
            "49.62649777359683, 24.009087497652555",
            "49.727541205451956, 23.98638096697053",
            "49.76497041043116, 23.3575534111516",
            "50.11052987263466, 24.28960437069082",
            "49.914901236713106, 24.446608239994553",
            "49.833151, 24.220795"
        };

        public List<string> GetGeolocations()
        {
            return geolocations;
        }

        public void SetGeolocation(string geolocation)
        {
            geolocations.Add(geolocation);
        }

        public void RemoveGeolocation(string geolocation)
        {
            geolocations.Remove(geolocation);
        }
    }
}