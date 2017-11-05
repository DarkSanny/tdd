using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FluentAssertions;

namespace TagCloudVisualization
{
	public class Spiral
	{
		private Point center;
		private double length = 0;
		private double angle = 0;

		public Spiral(Point center)
		{
			this.center = center;
		}

		public Point GetNextPoint()
		{
			var result = GetCartesianPoint(length, angle);
			angle += Math.PI / 180;
			length = angle / 2;
			return result;
		}

		private Point GetCartesianPoint(double length, double angle)
		{
			return new Point(center.X + (int)(length * Math.Cos(angle)), center.Y + (int)(length * Math.Sin(angle)));
		}

		public void Clear()
		{
			length = 0;
			angle = 0;
		}

	}

}
