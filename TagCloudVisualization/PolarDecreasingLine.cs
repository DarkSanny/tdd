using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloudVisualization
{
	public class Line : PolarFunction
	{
		public Line(Point center, double length, double angle) : base(center)
		{
			Length = length;
			Angle = angle;
		}

		public override Point GetNextPoint()
		{
			Length = Math.Max(0, Length - 1);
			return GetCartesianPoint(Length, Angle);
		}
	}
}
