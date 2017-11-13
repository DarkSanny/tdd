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
	public class PolarSpiral : PolarFunction
	{

		public PolarSpiral(Point center) : base(center) {}

		public override Point GetNextPoint()
		{
			Angle += Math.PI / 180;
			Length = Angle / 2;
			return GetCartesianPoint(Length, Angle);
		}
	}

}
