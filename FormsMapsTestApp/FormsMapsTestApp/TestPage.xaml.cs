using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace FormsMapsTestApp
{
	public partial class TestPage : ContentPage
	{
		public TestPage()
		{
			InitializeComponent();

			var stack = new StackLayout
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand
			};

			var map = new MapExtended()
			{
				ContentType = MapContentType.Route,
				CircleRadius = 100,
				HeightRequest = 800,
				WidthRequest = 600,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			// Add pins
			map.Pins.Add(new Pin { Label = "Point of Interest 1", Position = new Position(37.797513, -122.402058) });
			map.Pins.Add(new Pin { Label = "Point of Interest 2", Position = new Position(37.798433, -122.402256) });
			map.Pins.Add(new Pin { Label = "Point of Interest 3", Position = new Position(37.798582, -122.401071) });

			// Add map positions (used for optional overlay like Circle, Region or Route)
			map.Positions.Add(new Position(37.797513, -122.402058));
			map.Positions.Add(new Position(37.798433, -122.402256));
			map.Positions.Add(new Position(37.798582, -122.401071));

			// Define the position of the map
			map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(37.79752, -122.40183), Distance.FromMeters(200)));

			stack.Children.Add(map);

			Content = stack;
		}
	}
}
