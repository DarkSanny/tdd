using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

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
			center = new Point(5, 5);
			cloud = new CircularCloudLayouter(center);
		}


		[Test]
		public void RectShouldHaveSameSize()
		{
			var size = new Size(5, 4);

			cloud.PutNextRectangle(size).Size.Should().Be(size);
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

			cloud.PutNextRectangle(new Size(5, 4)).IntersectsWith(firstRect).Should().BeFalse();
		}

		[Test]
		public void RectsShouldBeNearToCenter()
		{
			var size = new Size(1, 1);

			for (var i = 0; i < 9; i++)
				cloud.PutNextRectangle(size);

			DistanceToCenter(cloud.PutNextRectangle(size), center).Should().BeLessOrEqualTo(10);

		}

		public static double DistanceToCenter(Rectangle rect, Point center)
		{
			var rectCenter = GetRectCenter(rect);
			return Math.Sqrt((center.X - rectCenter.X) * (center.X - rectCenter.X) +
			                 (center.Y - rectCenter.Y) * (center.Y - rectCenter.Y));
		}

		public static Point GetRectCenter(Rectangle rect) => new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);

		[Test]
		public void Density()
		{
			var size = new Size(10, 10);

			for (var i = 0; i < 50; i++)
				cloud.PutNextRectangle(size);
			var lastRect = cloud.PutNextRectangle(size);

			DistanceToCenter(lastRect, center).Should()
				.BeGreaterThan(DistanceToCenter(cloud.PutNextRectangle(new Size(1, 1)), center));
		}

		//TODO: нормальные названия тестов
		[Test, Timeout(100)]
		public void Optimization()
		{
			var size = new Size(50, 50);

			for (var i = 0; i < 100; i++)
				cloud.PutNextRectangle(size);


		}

	}

}
