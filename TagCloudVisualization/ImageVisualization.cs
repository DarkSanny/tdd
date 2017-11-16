using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloudVisualization
{
	public class ImageVisualization
	{
		public static Random random = new Random();

		public static Bitmap GetCloudImage(CircularCloudLayouter cloud)
		{
			var bitmap = new Bitmap(cloud.Center.X * 2, cloud.Center.Y * 2);
			var graphics = Graphics.FromImage(bitmap);
			foreach (var item in cloud.CloudItems)
			{
				graphics.FillRectangle(new SolidBrush(RandomColor()), item);
			}
			return bitmap;
		}

		public static Color RandomColor()
		{
			return Color.FromArgb(random.Next(156), random.Next(156), random.Next(156));
		}
	}
}