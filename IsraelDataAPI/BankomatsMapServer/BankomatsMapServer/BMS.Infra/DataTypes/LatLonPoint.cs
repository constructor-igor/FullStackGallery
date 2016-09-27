namespace BMS.Infra.DataTypes
{
    public class LatLonPoint
    {
        public readonly double Latitude;
        public readonly double Longitude;

        public LatLonPoint(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}