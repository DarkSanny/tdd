using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloudVisualization
{
    public class CircularCloudLayouter
    {

	    private Spiral spiral;

		private List<Rectangle?> cloudItems = new List<Rectangle?>();

		public CircularCloudLayouter(Point center)
		{
			spiral = new Spiral(center);
		}

	    public Rectangle PutNextRectangle(Size rectangleSize)
	    {
		    Rectangle? intersectRect = null;
			while (true)
			{
				var point = spiral.GetNextPoint();
				var rectangle = new Rectangle(point.X-rectangleSize.Width/2, point.Y - rectangleSize.Height/2, 
					rectangleSize.Width, rectangleSize.Height);
				if (intersectRect != null && rectangle.IntersectsWith(intersectRect.Value)) continue;
				intersectRect = cloudItems
					.Where(rect => rect != null)
					.FirstOrDefault(rect => rect.Value.IntersectsWith(rectangle));
				if (intersectRect != null) continue;
				cloudItems.Add(rectangle);
				return rectangle;
			}
	    }

	}
}
