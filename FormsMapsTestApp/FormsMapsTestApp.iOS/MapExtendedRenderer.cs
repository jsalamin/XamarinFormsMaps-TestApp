using CoreLocation;
using FormsMapsTestApp;
using FormsMapsTestApp.iOS;
using MapKit;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(MapExtended), typeof(MapExtendedRenderer))]
namespace FormsMapsTestApp.iOS
{
	public class MapExtendedRenderer : MapRenderer
	{
		MKCircleRenderer circleRenderer;
		MKPolygonRenderer polygonRenderer;
		MKPolylineRenderer polylineRenderer;

		protected override void OnElementChanged(ElementChangedEventArgs<View> e)
		{
			base.OnElementChanged(e);

			if(e.OldElement != null)
			{
				var nativeMap = Control as MKMapView;
				nativeMap.OverlayRenderer = null;
			}

			if (e.NewElement != null)
			{
				var formsMap = (MapExtended)e.NewElement;
				var nativeMap = Control as MKMapView;

				if(formsMap.ContentType == MapContentType.Circle && formsMap.Positions.Count > 0)
				{
					var position = formsMap.Positions[0];
					var radius = formsMap.CircleRadius;

					nativeMap.OverlayRenderer = GetCircleOverlayRenderer;

					var circleOverlay = MKCircle.Circle(new CLLocationCoordinate2D(position.Latitude, position.Longitude), radius);
					nativeMap.AddOverlay(circleOverlay);
				}

				if(formsMap.ContentType == MapContentType.Region || formsMap.ContentType == MapContentType.Route)
				{
					// Build the polygon that will be drawn on the map
					CLLocationCoordinate2D[] coords = new CLLocationCoordinate2D[formsMap.Positions.Count];

					int index = 0;
					foreach (var position in formsMap.Positions)
					{
						coords[index] = new CLLocationCoordinate2D(position.Latitude, position.Longitude);
						index++;
					}

					// Define overlay that will be added to the map (color, line...) and add it to the map
					if(formsMap.ContentType == MapContentType.Region)
					{
						nativeMap.OverlayRenderer = GetRegionOverlayRenderer;
						var blockOverlay = MKPolygon.FromCoordinates(coords);
						nativeMap.AddOverlay(blockOverlay);
					}
					else
					{
						nativeMap.OverlayRenderer = GetRouteOverlayRenderer;
						var blockOverlay = MKPolyline.FromCoordinates(coords);
						nativeMap.AddOverlay(blockOverlay);
					}
				}
			}
		}

		private MKOverlayRenderer GetCircleOverlayRenderer(MKMapView mapView, IMKOverlay overlay)
		{
			if(circleRenderer == null)
			{
				circleRenderer = new MKCircleRenderer(overlay as MKCircle);
				circleRenderer.FillColor = UIColor.Red;
				circleRenderer.Alpha = 0.4f;
			}
			return circleRenderer;
		}

		private MKOverlayRenderer GetRegionOverlayRenderer(MKMapView mapView, IMKOverlay overlay)
		{
			if(polygonRenderer == null)
			{
				polygonRenderer = new MKPolygonRenderer(overlay as MKPolygon);
				polygonRenderer.FillColor = UIColor.Red;
				polygonRenderer.StrokeColor = UIColor.Blue;
				polygonRenderer.Alpha = 0.4f;
				polygonRenderer.LineWidth = 9;
			}

			return polygonRenderer;
		}

		private MKOverlayRenderer GetRouteOverlayRenderer(MKMapView mapView, IMKOverlay overlay)
		{
			if(polylineRenderer == null)
			{
				polylineRenderer = new MKPolylineRenderer(overlay as MKPolyline);
				polylineRenderer.FillColor = UIColor.Blue;
				polylineRenderer.StrokeColor = UIColor.Red;
				polylineRenderer.LineWidth = 3;
				polylineRenderer.Alpha = 0.4f;
			}

			return polylineRenderer;
		}
	}
}
