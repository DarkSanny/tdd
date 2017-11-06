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

	    public Point Center { get; }

	    private Spiral spiral;

		private List<Rectangle?> cloudItems = new List<Rectangle?>();

	    public IEnumerable<Rectangle> CloudItems => 
			cloudItems.Where(rect => rect.HasValue).Select(rectangle => rectangle.Value);


	    private double sumOfWidth = 0;
	    private double sumOfHeight = 0;

		public CircularCloudLayouter(Point center)
		{
			this.Center = center;
			spiral = new Spiral(center);
		}

	    public Rectangle PutNextRectangle(Size rectangleSize)
	    {
		    var currenSpiral = IsShouldRunNewSpiral(rectangleSize) ? new Spiral(Center) : spiral;
		    Rectangle? intersectRect = null;
			while (true)
			{
				var point = currenSpiral.GetNextPoint();
				var rectangle = new Rectangle(point.X-rectangleSize.Width/2, point.Y - rectangleSize.Height/2, 
					rectangleSize.Width, rectangleSize.Height);
				if (intersectRect != null && rectangle.IntersectsWith(intersectRect.Value)) continue;
				intersectRect = cloudItems
					.Where(rect => rect != null)
					.FirstOrDefault(rect => rect.Value.IntersectsWith(rectangle));
				if (intersectRect != null) continue;
				return CreateRectangle(rectangle);
			}
	    }

	    private Rectangle CreateRectangle(Rectangle rectangle)
	    {
		    sumOfWidth += rectangle.Width;
		    sumOfHeight += rectangle.Height;
			cloudItems.Add(rectangle);
		    return rectangle;
	    }

	    private bool IsShouldRunNewSpiral(Size rectangleSize)
	    {
		    return rectangleSize.Width < (sumOfWidth / cloudItems.Count) / 2 &&
		           rectangleSize.Height < (sumOfHeight/ cloudItems.Count) / 2;

	    }

	}
}
