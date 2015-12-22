using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsMapsTestApp.WinPhone
{
	/// <summary>
	/// Extension for GeoCoordinate (source: http://dotnetbyexample.blogspot.ch/2014/02/drawing-circles-shapes-on-windows-phone.html)
	/// </summary>
	public static class GeoCoordinateExtensions
	{
		public static List<GeoCoordinate> GetCirclePoints(this GeoCoordinate center, double radius, int nbPoints = 50)
		{
			var angle = 360.0 / nbPoints;
			var locations = new List<GeoCoordinate>();

			for (int i = 0; i <= nbPoints; i++)
			{
				locations.Add(center.GetAtDistanceBearing(radius, angle * i));
			}

			return locations;
		}

		public static GeoCoordinate GetAtDistanceBearing(this GeoCoordinate point, double distance, double bearing)
		{
			double degreesToRadian = Math.PI / 180.0;
			double radianToDegrees = 180.0 / Math.PI;
			double earthRadius = 6378137.0;

			var latA = point.Latitude * degreesToRadian;
			var lonA = point.Longitude * degreesToRadian;
			var angularDistance = distance / earthRadius;
			var trueCourse = bearing * degreesToRadian;

			var lat = Math.Asin(
				Math.Sin(latA) * Math.Cos(angularDistance) +
				Math.Cos(latA) * Math.Sin(angularDistance) * Math.Cos(trueCourse));

			var dlon = Math.Atan2(
				Math.Sin(trueCourse) * Math.Sin(angularDistance) * Math.Cos(latA),
				Math.Cos(angularDistance) - Math.Sin(latA) * Math.Sin(lat));

			var lon = ((lonA + dlon + Math.PI) % (Math.PI * 2)) - Math.PI;

			var result = new GeoCoordinate(lat * radianToDegrees, lon * radianToDegrees);

			return result;
		}
	}
}
