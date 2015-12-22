using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace FormsMapsTestApp
{
	public class MapExtended : Map
	{
		public MapContentType ContentType { get; set; }
		public double CircleRadius { get; set; }
		public List<Position> Positions { get; set; }

		public MapExtended()
		{
			this.ContentType = MapContentType.Normal;
			this.CircleRadius = 500;
			this.Positions = new List<Position>();
		}
	}
}
