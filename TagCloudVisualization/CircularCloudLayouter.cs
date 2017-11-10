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

	    private PolarSpiral spiral;
		private List<Rectangle> cloudItems = new List<Rectangle>();

	    public IEnumerable<Rectangle> CloudItems => cloudItems;


	    private double sumOfWidth = 0;
	    private double sumOfHeight = 0;

		public CircularCloudLayouter(Point center)
		{
			this.Center = center;
			spiral = new PolarSpiral(center);
		}

	    public Rectangle PutNextRectangle(Size rectangleSize)
	    {
			if (IsShouldThrowArgumentException(rectangleSize)) throw new ArgumentException();
		    var currentSpiral = IsShouldRunNewSpiral(rectangleSize) ? new PolarSpiral(Center) : spiral;
		    var intersectRect = default(Rectangle);
			while (true)
			{
				var point = currentSpiral.GetNextPoint();
				var rectangle = CreateRectangle(point, rectangleSize);
				if (intersectRect != default(Rectangle) && rectangle.IntersectsWith(intersectRect)) continue;
				intersectRect = GetIntersect(rectangle);
				if (intersectRect != default(Rectangle)) continue;
				rectangle = TryMoveToCenter(rectangle, currentSpiral);
				return AddRectangleInCloud(rectangle);
			}
	    }

	    private Rectangle TryMoveToCenter(Rectangle rectangle, PolarSpiral spiral)
	    {
		    var correctRectangle = rectangle;
			var line = new PolarDecreasingLine(spiral.Center, spiral.Length, spiral.Angle);
		    while (true)
		    {
			    var point = line.GetNextPoint();
				var rect = CreateRectangle(point, rectangle.Size);
			    var intersectRect = GetIntersect(rect);
			    if (intersectRect == default(Rectangle) && line.Length != 0) correctRectangle = rect;
			    else return correctRectangle;
		    }
	    }

	    private Rectangle GetIntersect(Rectangle rectangle) => cloudItems
		    .FirstOrDefault(rect => rect.IntersectsWith(rectangle));

		private Rectangle CreateRectangle(Point point, Size size) => 
			new Rectangle(point.X - size.Width/2, point.Y-size.Height/2, size.Width, size.Height);

	    private Rectangle AddRectangleInCloud(Rectangle rectangle)
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

	    private bool IsShouldThrowArgumentException(Size rectangleSize)
	    {
		    return rectangleSize == default(Size) || rectangleSize.Width < 0 || rectangleSize.Height < 0;
	    }

	}
}
