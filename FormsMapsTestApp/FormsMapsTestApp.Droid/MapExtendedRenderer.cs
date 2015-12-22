using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Maps.Android;
using FormsMapsTestApp.Droid;
using Xamarin.Forms;
using FormsMapsTestApp;
using Android.Gms.Maps;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Maps;
using Android.Gms.Maps.Model;

[assembly:ExportRenderer(typeof(MapExtended), typeof(MapExtendedRenderer))]
namespace FormsMapsTestApp.Droid
{
	public class MapExtendedRenderer : MapRenderer, IOnMapReadyCallback
	{
		private GoogleMap map;
		private MapContentType contentType;
		private double radius;
		private List<Position> positions;

		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
		{
			base.OnElementChanged(e);

			if(e.OldElement != null)
			{
				// Unsubscribe
			}

			if(e.NewElement != null)
			{
				var formsMap = (MapExtended)e.NewElement;
				contentType = formsMap.ContentType;
				radius = formsMap.CircleRadius;
				positions = formsMap.Positions;

				((MapView)Control).GetMapAsync(this);
			}
		}
		public void OnMapReady(GoogleMap googleMap)
		{
			map = googleMap;

			if(contentType == MapContentType.Circle && positions.Count > 0)
			{
				var position = positions[0];
				var circleOptions = new CircleOptions();
				circleOptions.InvokeCenter(new LatLng(position.Latitude, position.Longitude));

				circleOptions.InvokeRadius(radius);
				circleOptions.InvokeFillColor(0x66FF0000);
				circleOptions.InvokeStrokeColor(0x66FF0000);
				circleOptions.InvokeStrokeWidth(0);
				map.AddCircle(circleOptions);
			}

			if(contentType == MapContentType.Region)
			{
				var polygonOptions = new PolygonOptions();
				polygonOptions.InvokeFillColor(0x66FF0000);
				polygonOptions.InvokeStrokeColor(0x660000FF);
				polygonOptions.InvokeStrokeWidth(30.0f);

				foreach (var position in positions)
				{
					polygonOptions.Add(new LatLng(position.Latitude, position.Longitude));
				}

				map.AddPolygon(polygonOptions);
			}

			if(contentType == MapContentType.Route)
			{
				var polylineOptions = new PolylineOptions();
				polylineOptions.InvokeColor(0x66FF0000);

				foreach (var position in positions)
				{
					polylineOptions.Add(new LatLng(position.Latitude, position.Longitude));
				}

				map.AddPolyline(polylineOptions);
			}
		}
	}
}