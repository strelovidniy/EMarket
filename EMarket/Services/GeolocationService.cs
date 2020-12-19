using System.Collections.Generic;

namespace EMarket.Services
{
    public class GeolocationService : IGeolocationService
    {
        private List<string> geolocations = new List<string>()
        {
            "49.62649777359683, 24.009087497652555"
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