using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TagCloudVisualization;

namespace TagCloudVisualization
{
	public class MyForm : Form
	{

		private Random random = new Random();

		public MyForm()
		{
			Size = new Size(1000, 1000);
			var cloud = new CircularCloudLayouter(new Point(500, 500));
			Paint += (sender, args) =>
			{
				for (var i = 0; i < 100; i++)
				{
					var size = new Size(20 + random.Next(80), 20 + random.Next(80));
					var rect = cloud.PutNextRectangle(size);
					var color = RandomColor();
					args.Graphics.FillRectangle(new SolidBrush(color), rect);
				}
				ImageVisualization.GetCloudImage(cloud).Save("D:\\image2.png", ImageFormat.Png);
			};
		}

		private Color RandomColor()
		{
			return Color.FromArgb(random.Next(156), random.Next(156), random.Next(156));
		}

	}
}
