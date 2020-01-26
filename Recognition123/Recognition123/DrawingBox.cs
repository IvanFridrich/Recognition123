using System;
using System.Drawing;
using System.Windows.Forms;

namespace Recognition123
{
    /// <summary>
    /// Box for drawing in back and white.
    /// Hold left mouse button for drawing black and right mouse button for drawing white.
    /// </summary>
    public class DrawingBox : PictureBox
    {
        /// <summary>
        /// Delegete for signaling that drawing has ended (mouse left up or mouse left)
        /// </summary>
        public delegate void DrawingDoneDelegate();

        /// <summary>
        /// Event signalizing thath drawing has ended (mouse left up or mouse left)
        /// </summary>
        public event DrawingDoneDelegate DrawingDone;

        /// <summary>
        /// Contructor
        /// </summary>
        public DrawingBox()
        {
            MouseMove += DrawingBox_MouseMove;
            MouseClick += DrawingBox_MouseClick;
            MouseLeave += DrawingBox_MouseLeave;
            MouseUp += DrawingBox_MouseUp;
            Width = 15 * 10;
            Height = 20 * 10;

            Clear();
        }

        /// <summary>
        /// Handler of mouse button up event
        /// </summary>
        private void DrawingBox_MouseUp(object sender, MouseEventArgs e)
        {
            DrawingDone?.Invoke();
        }

        /// <summary>
        /// Handler of mouse left event
        /// </summary>
        private void DrawingBox_MouseLeave(object sender, EventArgs e)
        {
            DrawingDone?.Invoke();
        }

        /// <summary>
        /// Handler of mouse click event
        /// </summary>
        private void DrawingBox_MouseClick(object sender, MouseEventArgs e)
        {
            DrawingBox_MouseMove(sender, e);
        }

        /// <summary>
        /// Handler of mouse move event
        /// </summary>
        private void DrawingBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Bitmap bitmap = Image as Bitmap;

                int x = e.X / 15 * 15;
                int y = e.Y / 20 * 20;

                Graphics gr = Graphics.FromImage(bitmap);
                gr.FillRectangle(new SolidBrush(Color.Black), x, y, 15, 20);

                Image = bitmap;
            }
            else if (e.Button == MouseButtons.Right)
            {
                Bitmap bitmap = Image as Bitmap;

                int x = e.X / 15 * 15;
                int y = e.Y / 20 * 20;

                Graphics gr = Graphics.FromImage(bitmap);
                gr.FillRectangle(new SolidBrush(Color.White), x, y, 15, 20);

                Image = bitmap;
            }
        }

        /// <summary>
        /// Return s current drawing canvas in size of 15x20 pixels. 
        /// </summary>
        /// <returns>15x20 pixel black and white bitmap</returns>
        public Bitmap GetBitmap()
        {
            Bitmap bitmap = new Bitmap(15, 20);
            Bitmap image = Image as Bitmap;

            for (int x = 0; x < bitmap.Width; ++x)
            {
                for (int y = 0; y < bitmap.Height; ++y)
                {
                    if (image.GetPixel(x * 10, y * 10) == Color.FromArgb(255, 0, 0, 0))
                    {
                        bitmap.SetPixel(x, y, Color.Black);
                    }
                    else 
                    {
                        bitmap.SetPixel(x, y, Color.White);
                    }
                }
            }

            return bitmap;
        }

        /// <summary>
        /// Clears current canvas. All pixels will be white.
        /// </summary>
        public void Clear()
        {
            Bitmap background = new Bitmap(Width, Height);

            for (int x = 0; x < background.Width; ++x)
            {
                for (int y = 0; y < background.Height; ++y)
                {
                    background.SetPixel(x, y, Color.White);
                }
            }

            Image = background;
        }
    }
}
