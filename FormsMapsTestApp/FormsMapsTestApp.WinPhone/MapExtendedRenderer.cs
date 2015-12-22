using FormsMapsTestApp;
using FormsMapsTestApp.WinPhone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps.WP8;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Platform.WinPhone;
using Microsoft.Phone.Maps.Controls;
using System.Device.Location;
using System.Windows.Media;

[assembly:ExportRenderer(typeof(MapExtended), typeof(MapExtendedRenderer))]
namespace FormsMapsTestApp.WinPhone
{
	public class MapExtendedRenderer : MapRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Maps.Map> e)
		{
			base.OnElementChanged(e);

			if(e.OldElement != null)
			{
				// Unsubscribe
			}

			if(e.NewElement != null)
			{
				var formsMap = (MapExtended)e.NewElement;

				if(formsMap.ContentType == MapContentType.Circle && formsMap.Positions.Count > 0)
				{
					var position = formsMap.Positions[0];
					var location = new GeoCoordinate(position.Latitude, position.Longitude);
					var circle = new MapPolygon
					{
						FillColor = new System.Windows.Media.Color { A = 60, R = 255, G = 0, B = 0 },
					};

					// Build a polyline that represents a circle
					foreach (var point in location.GetCirclePoints(formsMap.CircleRadius))
					{
						circle.Path.Add(point);
					}

					((Microsoft.Phone.Maps.Controls.Map)Control).MapElements.Add(circle);
				}
				if(formsMap.ContentType == MapContentType.Region)
				{
					MapPolygon polygon = new MapPolygon
					{
						FillColor = new System.Windows.Media.Color { A = 60, R = 255, G = 0, B = 0 },
						StrokeColor = Colors.Blue,
						StrokeThickness = 3
					};

					foreach (var position in formsMap.Positions)
					{
						polygon.Path.Add(new GeoCoordinate(position.Latitude, position.Longitude));
					}

					((Microsoft.Phone.Maps.Controls.Map)Control).MapElements.Add(polygon);
				}

				if (formsMap.ContentType == MapContentType.Route)
				{
					MapPolyline polyline = new MapPolyline
					{
						StrokeColor = Colors.Red,
						StrokeThickness = 3
					};

					foreach (var position in formsMap.Positions)
					{
						polyline.Path.Add(new GeoCoordinate(position.Latitude, position.Longitude));
					}

					((Microsoft.Phone.Maps.Controls.Map)Control).MapElements.Add(polyline);
				}
			}
		}
	}
}
