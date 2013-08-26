using System;
using System.Drawing;
using System.Linq;
using EkranPaylas.Extensions;
using EkranPaylas.Tasks.StartupTasks;
using Ploeh.AutoFixture;

namespace EkranPaylas.UnitTests
{
    public abstract class FixtureTestBase
    {
        protected FixtureTestBase()
        {
            Random = new Random();
            Fixture = new Fixture();

            Fixture.Register(() => GenerateNoise(200, 200));

            typeof (IStartupTask).GetInstances<IStartupTask>()
                .OrderBy(q => q.Priority)
                .ToList()
                .ForEach(q => q.Execute());
        }

        protected Fixture Fixture { get; set; }
        protected Random Random { get; set; }

        protected Bitmap GenerateNoise(int width, int height)
        {
            var finalBmp = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    int num = Random.Next(0, 256);
                    finalBmp.SetPixel(x, y, Color.FromArgb(255, num, num, num));
                }

            return finalBmp;
        }
    }
}
