using System.Drawing;


namespace Utils
{
    /// <summary>
    /// Utility functions for working with bitmaps.
    /// </summary>
    class Utils
    {
        /// <summary>
        /// Converts bitmap to double vector of zeros and ones.
        /// </summary>
        /// <param name="bmap">Input bitmap</param>
        /// <returns>Vector of doubles</returns>
        public static double[] BitmapToVector(Bitmap bmap)
        {
            var vector = new double[bmap.Height * bmap.Width];
            for (int y = 0; y < bmap.Height; ++y)
                for (int x = 0; x < bmap.Width; ++x)
                {
                    Color c = bmap.GetPixel(x, y);
                    vector[y * bmap.Width + x] = c.R == 255 ? 0.0 : 1.0;
                }

            return vector;
        }

        /// <summary>
        /// Structure holding information about bitmap content distances from edges.
        /// </summary>
        struct Distances
        {
            public int _distanceFromLeft;
            public int _distanceFromRight;
            public int _distanceFromTop;
            public int _distanceFromBottom;
        }

        /// <summary>
        /// Returns distances of black bitmap content from edges.
        /// </summary>
        /// <param name="input">Input bitmap (black and white)</param>
        /// <returns>Distances from edges</returns>
        static Distances GetDistancesToEdge(Bitmap input)
        {
            Distances distances;

            distances._distanceFromLeft = int.MaxValue;
            distances._distanceFromRight = int.MaxValue;
            distances._distanceFromTop = int.MaxValue;
            distances._distanceFromBottom = int.MaxValue;

            for (int y = 0; y < input.Height; ++y)
            {
                bool blackDetectedFromLeft = false;
                int lastBlackX = 0;

                for (int x = 0; x < input.Width; ++x)
                {
                    Color pixelColor = input.GetPixel(x, y);

                    // updating left distance
                    if (!blackDetectedFromLeft && pixelColor == Color.FromArgb(255, 0, 0, 0))
                    {
                        distances._distanceFromLeft = x < distances._distanceFromLeft ? x : distances._distanceFromLeft;
                        blackDetectedFromLeft = true;
                    }

                    // updating right distance
                    if (pixelColor == Color.FromArgb(255, 0, 0, 0))
                    {
                        lastBlackX = x;
                    }
                    if (x == input.Width - 1)
                    {
                        int dRight = input.Width - lastBlackX - 1;
                        distances._distanceFromRight = dRight < distances._distanceFromRight
                            ? dRight
                            : distances._distanceFromRight;
                    }
                }
            }

            for (int x = 0; x < input.Width; ++x)
            {
                bool blackDetectedFromTop = false;
                int lastBlackY = 0;

                for (int y = 0; y < input.Height; ++y)
                {
                    Color pixelColor = input.GetPixel(x, y);

                    // updating top distance
                    if (!blackDetectedFromTop && pixelColor == Color.FromArgb(255, 0, 0, 0))
                    {
                        distances._distanceFromTop = y < distances._distanceFromTop ? y : distances._distanceFromTop;
                        blackDetectedFromTop = true;
                    }

                    // updating bottom distance
                    if (pixelColor == Color.FromArgb(255, 0, 0, 0))
                    {
                        lastBlackY = y;
                    }
                    if (y == input.Height - 1)
                    {
                        int dBOttom = input.Height - lastBlackY - 1;
                        distances._distanceFromBottom = dBOttom < distances._distanceFromBottom
                            ? dBOttom
                            : distances._distanceFromBottom;
                    }
                }
            }

            return distances;
        }


        /// <summary>
        /// Shifts content of a bitmap. New pixels are automatically white.
        /// </summary>
        /// <param name="input">Input bitmap</param>
        /// <param name="right">How many pixels to shift to right. Negative value shifts to left.</param>
        /// <param name="down">How many pixels to shift down. Negative value shifts up.</param>
        /// <returns></returns>
        static Bitmap ShiftBitmap(Bitmap input, int right, int down)
        {
            Bitmap shifted = input.Clone() as Bitmap;

            for (int x = 0; x < shifted.Width; ++x)
            {
                for (int y = 0; y < shifted.Height; ++y)
                {
                    int oldX = x - right;
                    int oldY = y - down;

                    if (oldX < 0 || oldX >= input.Width || oldY < 0 || oldY >= input.Height)
                    {
                        shifted.SetPixel(x, y, Color.White);
                    }
                    else
                    {
                        shifted.SetPixel(x, y, input.GetPixel(oldX, oldY));
                    }
                }
            }

            return shifted;
        }

        /// <summary>
        /// Centrifies black content of a bitmap.
        /// </summary>
        /// <param name="input">Input bitmap</param>
        /// <returns>Centrified bitmap</returns>
        public static Bitmap Centrify(Bitmap input)
        {
            Distances dist = GetDistancesToEdge(input);

            int xShift = (dist._distanceFromRight - dist._distanceFromLeft) / 2;
            int yShift = (dist._distanceFromBottom - dist._distanceFromTop) / 2;

            return ShiftBitmap(input, xShift, yShift);
        }
    }
}

