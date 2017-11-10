using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloudVisualization
{
	public abstract class PolarFunction
	{

		public Point Center { get; protected set; }
		public double Length { get; protected set; }
		public double Angle { get; protected set; }

		public PolarFunction(Point center)
		{
			Center = center;
		}

		public abstract Point GetNextPoint();

		protected Point GetCartesianPoint(double length, double angle)
		{
			return new Point(Center.X + (int)(length * Math.Cos(angle)), Center.Y + (int)(length * Math.Sin(angle)));
		}

	}
}
