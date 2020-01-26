using System.Drawing;


namespace Utils
{
    class Utils
    {
        /// <summary>
        /// Converts bitmap to double vector of zeroes nad ones.
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
        /// Moves the content of the image to the upper left corner
        /// </summary>
        /// <param name="input">Input bitmap</param>
        /// <returns>Shifted input bitmap</returns>
        public static Bitmap MovePictureContentToUpperLeftCorner(Bitmap input)
        {
            int x1 = 0;
            int y1 = 0;

            // x1 finding
            for (int x = 0; x < input.Width; ++x)
            {
                bool found = false;
                for (int y = 0; y < input.Height; ++y)
                {
                    if (input.GetPixel(x, y).R == 0)
                    {
                        x1 = x;
                        found = true;
                        break;
                    }
                }

                if (found) break;
            }

            // y1 finding
            for (int y = 0; y < input.Height; ++y)
            {
                bool found = false;
                for (int x = 0; x < input.Width; ++x)
                {
                    if (input.GetPixel(x, y).R == 0)
                    {
                        y1 = y;
                        found = true;
                        break;
                    }
                }

                if (found) break;
            }

            Bitmap output = new Bitmap(input.Width, input.Height);

            for (int x = 0; x < output.Width; ++x)
                for (int y = 0; y < output.Height; ++y)
                    output.SetPixel(x, y, Color.FromArgb(255, 255, 255));

            for (int x = x1; x < input.Width; ++x)
                for (int y = y1; y < input.Height; ++y)
                    output.SetPixel(x - x1, y - y1, input.GetPixel(x, y));

            return output;
        }

    }
}

