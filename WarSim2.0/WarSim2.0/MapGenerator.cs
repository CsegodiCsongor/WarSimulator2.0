using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarSim2._0
{
    public class MapGenerator
    {
        public double[,] CreateRandomNoiseMap(int width, int height)
        {
            double[,] map = new double[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    map[i, j] = Engine.random.NextDouble();
                }
            }

            return map;
        }


        public double[,] KernelBlurMap(double[,] map, double[,] kernel)
        {
            double[,] blurredMap = new double[map.GetLength(0), map.GetLength(1)];

            for (int i = 0; i < blurredMap.GetLength(0); i++)
            {
                for (int j = 0; j < blurredMap.GetLength(1); j++)
                {
                    blurredMap[i, j] = GetAppliedKernel(map, i, j, kernel);
                }
            }

            return blurredMap;
        }

        public double GetAppliedKernel(double[,] map, int i, int j, double[,] kernel)
        {
            int kernelWidth = kernel.GetLength(0);

            double currentValue = 0;
            double usedValues = 0;

            for (int q = i - kernelWidth / 2; q <= i + kernelWidth / 2; q++)
            {
                if (q >= 0 && q < map.GetLength(0))
                {
                    for (int w = j - kernelWidth / 2; w <= j + kernelWidth / 2; w++)
                    {
                        if (w >= 0 && w < map.GetLength(1))
                        {
                            currentValue += kernel[q - (i - kernelWidth / 2), w - (j - kernelWidth / 2)] * map[q, w];
                            usedValues += kernel[q - (i - kernelWidth / 2), w - (j - kernelWidth / 2)];
                        }
                    }
                }
            }

            return currentValue / usedValues;
        }

        #region Kernels
        public double[,] CreateGuassianKernel(int kernelWidth)
        {
            double[,] kernel = new double[kernelWidth, kernelWidth];

            Point currentPoint = new Point(kernelWidth / 2, kernelWidth / 2);
            kernel[currentPoint.Y, currentPoint.X] = Math.Pow(2, kernelWidth - 1);

            List<Point> points = new List<Point>();
            points.Add(currentPoint);

            while (points.Count > 0)
            {
                currentPoint = points[0];
                points.RemoveAt(0);

                if (Ok(currentPoint, -1, 0, kernel))
                {
                    kernel[currentPoint.Y, currentPoint.X - 1] = kernel[currentPoint.Y, currentPoint.X] / 2;
                    points.Add(new Point(currentPoint.X - 1, currentPoint.Y));
                }
                if (Ok(currentPoint, 1, 0, kernel))
                {
                    kernel[currentPoint.Y, currentPoint.X + 1] = kernel[currentPoint.Y, currentPoint.X] / 2;
                    points.Add(new Point(currentPoint.X + 1, currentPoint.Y));
                }
                if (Ok(currentPoint, 0, -1, kernel))
                {
                    kernel[currentPoint.Y - 1, currentPoint.X] = kernel[currentPoint.Y, currentPoint.X] / 2;
                    points.Add(new Point(currentPoint.X, currentPoint.Y - 1));
                }
                if (Ok(currentPoint, 0, 1, kernel))
                {
                    kernel[currentPoint.Y + 1, currentPoint.X] = kernel[currentPoint.Y, currentPoint.X] / 2;
                    points.Add(new Point(currentPoint.X, currentPoint.Y + 1));
                }
            }

            return kernel;
        }

        public double[,] CreateMeanKernel(int kernelWidth)
        {
            double[,] kernel = new double[kernelWidth, kernelWidth];
            for (int i = 0; i < kernelWidth; i++)
            {
                for (int j = 0; j < kernelWidth; j++)
                {
                    kernel[i, j] = 1;
                }
            }
            return kernel;
        }

        #region LeeTypeChecks
        private bool Ok(Point currentPoint, int xOffset, int yOffset, double[,] kernel)
        {
            if (CouldGo(currentPoint, xOffset, yOffset, kernel.GetLength(0)) && ShouldGo(currentPoint, xOffset, yOffset, kernel))
            {
                return true;
            }
            return false;
        }

        private bool ShouldGo(Point currentPoint, int xOffset, int yOffset, double[,] kernel)
        {
            if (kernel[currentPoint.Y + yOffset, currentPoint.X + xOffset] == 0)
            {
                return true;
            }
            return false;
        }

        private bool CouldGo(Point currentPoint, int xOffset, int yOffset, int kernelWidth)
        {
            if (currentPoint.X + xOffset < kernelWidth && currentPoint.X + xOffset >= 0 &&
                currentPoint.Y + yOffset < kernelWidth && currentPoint.Y + yOffset >= 0)
            {
                return true;
            }
            return false;
        }
        #endregion
        #endregion
    }
}
