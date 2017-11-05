using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TagCloudVisualization;

namespace TagCloud
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
				for (int i = 0; i < 100; i++)
				{
					var size = new Size(20 + random.Next(80), 20 + random.Next(80));
					var rect = cloud.PutNextRectangle(size);
					var color = RandomColor();
					args.Graphics.FillRectangle(new SolidBrush(color), rect);
				}
			};
		}

		private Color RandomColor()
		{
			return Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
		}

	}
}
