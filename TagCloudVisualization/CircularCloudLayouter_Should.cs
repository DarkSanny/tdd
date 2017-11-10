using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace TagCloudVisualization
{
	[TestFixture]
	public class CircularCloudLayouter_Should
	{

		private Point center;
		private CircularCloudLayouter cloud;

		[SetUp]
		public void SetUp()
		{
			center = new Point(500, 500);
			cloud = new CircularCloudLayouter(center);
		}


		[Test]
		public void RectShouldHaveSameSize()
		{
			var size = new Size(5, 4);
			var rect = cloud.PutNextRectangle(size);

			rect.Size.Should().Be(size);
		}

		[Test]
		public void Cloud_ShouldThrowWhenDefaultSize()
		{
			Action act = () => cloud.PutNextRectangle(default(Size));

			act.ShouldThrow<ArgumentException>();
		}

		[Test]
		public void Cloud_ShouldThrowWhenNegativeSize()
		{
			Size size = new Size(-5, -1);
			Action act = () => cloud.PutNextRectangle(size);

			act.ShouldThrow<ArgumentException>();

		}

		[Test]
		public void FirstRectShouldIntersectCenter()
		{
			var firstRect = cloud.PutNextRectangle(new Size(5, 4));

			firstRect.IntersectsWith(new Rectangle(center.X, center.Y, 1, 1)).Should().BeTrue();
		}

		[Test]
		public void SecondRectShouldNotIntersectsFirstRect()
		{
			var firstRect = cloud.PutNextRectangle(new Size(5, 4));
			var secondRect = cloud.PutNextRectangle(new Size(5, 4));

			firstRect.IntersectsWith(secondRect).Should().BeFalse();
		}

		[Test]
		public void RectsShouldBeNearToCenter()
		{
			var size = new Size(1, 1);
			for (var i = 0; i < 9; i++)
				cloud.PutNextRectangle(size);

			DistanceToCenter(cloud.PutNextRectangle(size), center).Should().BeLessOrEqualTo(5);
		}

		public static double DistanceToCenter(Rectangle rect, Point center)
		{
			var rectCenter = GetRectCenter(rect);
			return Math.Sqrt((center.X - rectCenter.X) * (center.X - rectCenter.X) +
			                 (center.Y - rectCenter.Y) * (center.Y - rectCenter.Y));
		}

		public static Point GetRectCenter(Rectangle rect) => new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);

		[Test]
		public void TestOfDensity()
		{
			var size = new Size(10, 10);
			for (var i = 0; i < 50; i++)
				cloud.PutNextRectangle(size);
			var lastRect = cloud.PutNextRectangle(size);
			var smallRect = cloud.PutNextRectangle(new Size(1, 1));

			DistanceToCenter(lastRect, center).Should()
				.BeGreaterThan(DistanceToCenter(smallRect, center));
		}

		[Test, Timeout(100)]
		public void TestOfOptimization()
		{
			var size = new Size(50, 50);
			for (var i = 0; i < 100; i++)
				cloud.PutNextRectangle(size);
		}

		[TearDown]
		public void TearDown()
		{
			var testresult = TestContext.CurrentContext.Result.Outcome;

			if (!testresult.Equals(ResultState.Success))
			{
				var path = Environment.CurrentDirectory + "\\" + TestContext.CurrentContext.Test.FullName + ".png";
				Console.WriteLine("Tag cloud visualization saved to file " + path);
				ImageVisualization.GetCloudImage(cloud).Save(path, ImageFormat.Png);
			}
		}


	}

}
