using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace WindowsFormsApp1
{
    abstract class Filters
    {
        protected abstract Color calculateNewPixelColor(Bitmap sourceImage, int x, int y);

        public virtual Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
                }
            }
            return resultImage;
        }


        public int Clamp(int value, int min, int max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;
        }
    }

    class GreyWorldFilter: Filters
    {
        private double R_max, G_max, B_max, Middle;

        private void Parametr(Bitmap sourceImage)
        {

            double R_sum = 0;
            double G_sum = 0;
            double B_sum = 0;
            for (int i = 0; i < sourceImage.Width; i++)
            {
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    Color pixel = sourceImage.GetPixel(i, j);
                    R_sum += pixel.R;
                    G_sum += pixel.G;
                    B_sum += pixel.B;
                }
            }
            double N = sourceImage.Width * sourceImage.Height;
            R_max = R_sum / N;
            G_max = G_sum / N;
            B_max = B_sum / N;
            Middle = (R_max + G_max + B_max) / 3;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {           
            Color sourceColor = sourceImage.GetPixel(x, y);
            Color resultColor = Color.FromArgb(Clamp((int)(sourceColor.R * (Middle / R_max)), 0, 255), Clamp((int)(sourceColor.G * (Middle / G_max)), 0, 255), Clamp((int)(sourceColor.B * (Middle / B_max)), 0, 255));
            return resultColor;
        }

        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            Parametr(sourceImage);
            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
                }
            }
            return resultImage;
        }
    }

    class InvertFilter: Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            Color resultColor = Color.FromArgb(255 - sourceColor.R, 255 - sourceColor.G, 255 - sourceColor.B);
            return resultColor;
        }

        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
                }
            }
            return resultImage;
        }
    }

    class SepiyaFilter: Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            double k = 7;
            double Intensity = 0.299 * sourceColor.R + 0.587 * sourceColor.G + 0.144 * sourceColor.B;
            double R = Intensity + 2 * k;
            double G = Intensity + 0.5 * k;
            double B = Intensity - 1 * k;
            Color resultColor = Color.FromArgb(Clamp((int)R, 0, 255), Clamp((int)G, 0, 255), Clamp((int)B, 0, 255));
            return resultColor;
        }

        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
                }
            }
            return resultImage;
        }
    }

    class YarkiyFilter: Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            double p = 30;
            double R = sourceColor.R + p;
            double G = sourceColor.G + p;
            double B = sourceColor.B + p;
            Color resultColor = Color.FromArgb(Clamp((int)R, 0, 255), Clamp((int)G, 0, 255), Clamp((int)B, 0, 255));
            return resultColor;
        }

        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
                }
            }
            return resultImage;
        }
    }

    class GrayScaleFilter: Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            double Intensity = 0.299 * sourceColor.R + 0.587 * sourceColor.G + 0.144 * sourceColor.B;
            Color resultColor = Color.FromArgb(Clamp((int)Intensity, 0, 255), Clamp((int)Intensity, 0, 255), Clamp((int)Intensity, 0, 255));
            return resultColor;
        }

        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
                }
            }
            return resultImage;
        }
    }

    class MatrixFilter: Filters
    {
        protected float[,] kernel = null;
        protected MatrixFilter() { }
        public MatrixFilter(float[,] kernel)
        {
            this.kernel = kernel;
        }

        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
                }
            }
            return resultImage;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;
            float resultR = 0;
            float resultG = 0;
            float resultB = 0;
            for (int l = -radiusY; l <= radiusY; l++)
            {
                for (int k = -radiusX; k <= radiusX; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    resultR += neighborColor.R * kernel[k + radiusX, l + radiusY];
                    resultG += neighborColor.G * kernel[k + radiusX, l + radiusY];
                    resultB += neighborColor.B * kernel[k + radiusX, l + radiusY];
                }
            }
            return Color.FromArgb(Clamp((int)resultR, 0, 255), Clamp((int)resultG, 0, 255), Clamp((int)resultB, 0, 255));
        }
    }

    class Morfologiya : Filters
    {
        bool[,] kernel;
        public bool flag;
        public Morfologiya(bool[,] _kernel)
        {
            //flag = _flag;
            kernel = _kernel;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int max = 0;
            int min = 10000;

            Color clr = Color.Black;

            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;

            if (flag)
            {
                for (int l = -radiusY; l <= radiusY; l++)
                    for (int k = -radiusX; k <= radiusX; k++)
                    {
                        int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                        int idY = Clamp(y + l, 0, sourceImage.Height - 1);

                        Color sourceColor = sourceImage.GetPixel(idX, idY);

                        int intensity = (int)(0.36 * sourceColor.R + 0.53 * sourceColor.G + 0.11 * sourceColor.B);

                        if ((kernel[k + radiusX, l + radiusY]) && (intensity > max))
                        {
                            max = intensity;
                            clr = sourceColor;
                        }
                    }
            }
            else
            {
                for (int l = -radiusY; l <= radiusY; l++)
                    for (int k = -radiusX; k <= radiusX; k++)
                    {
                        int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                        int idY = Clamp(y + l, 0, sourceImage.Height - 1);

                        Color sourceColor = sourceImage.GetPixel(idX, idY);

                        int intensity = (int)(0.36 * sourceColor.R + 0.53 * sourceColor.G + 0.11 * sourceColor.B);

                        if ((kernel[k + radiusX, l + radiusY]) && intensity < min)
                        {
                            min = intensity;
                            clr = sourceColor;
                        }
                    }
            }

            return clr;
        }
    }

    class Closing: Morfologiya
    {
        public Closing(bool[,] _kernel) : base(_kernel)
        { 
        }

        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap currImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            flag = true;

            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 50));

                for (int j = 0; j < sourceImage.Height; j++)
                {
                    currImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
                }
            }

            flag = false;

            for (int i = 0; i < currImage.Width; i++)
            {
                worker.ReportProgress((int)(((float)i / resultImage.Width * 50) + 50));

                for (int j = 0; j < currImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixelColor(currImage, i, j));
                }
            }

            return resultImage;
        }
    }


    class Opening : Morfologiya
    {
        public Opening(bool[,] _kernel): base(_kernel)
        {
        }

        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap currImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            flag = false;

            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 50));

                for (int j = 0; j < sourceImage.Height; j++)
                {
                    currImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
                }
            }

            flag = true;

            for (int i = 0; i < currImage.Width; i++)
            {
                worker.ReportProgress((int)(((float)i / resultImage.Width * 50) + 50));

                for (int j = 0; j < currImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixelColor(currImage, i, j));
                }
            }

            return resultImage;
        }
    }

    class Dilation: Morfologiya
    {
        public Dilation(bool[,] _kernel) : base(_kernel)
        {
        }

        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap currImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            //Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            flag = true;

            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / currImage.Width * 100));

                for (int j = 0; j < sourceImage.Height; j++)
                {
                    currImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
                }
            }
            return currImage;
        }
    }

    class Erozin: Morfologiya
    {
        public Erozin(bool[,] _kernel) : base(_kernel)
        {
        }

        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap currImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            //Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);

            flag = false;

            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / currImage.Width * 100));

                for (int j = 0; j < sourceImage.Height; j++)
                {
                    currImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
                }
            }
            return currImage;
        }
    }

    class BlurFilter : MatrixFilter
    {
        public BlurFilter()
        {
            int sizeX = 3;
            int sizeY = 3;
            kernel = new float[sizeX, sizeY];
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    kernel[i, j] = 1.0f / (float)(sizeX * sizeY);
                }
            }
        }
    }



    class RezkostFilter: MatrixFilter
    {
        public RezkostFilter()
        {
            kernel = new float[3, 3] { { 0f, -1.0f, 0f }, { -1.0f, 5.0f, -1.0f }, { 0f, -1.0f, 0f } };
        }
    }

    class SobelFilter : MatrixFilter
    {
        public SobelFilter()
        {

            kernel = new float[3, 3]{{ -1.0f, 0f, 1.0f }, { -2.0f, 0f, 2.0f}, { -1.0f, 0f, 1.0f}};
            //float[,] kernel_y = new float[3, 3]{{ -1, -2, 1 }, { 0, 0, 0 }, { 1, 2, 1 }};
        }

        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage1 = new Bitmap(sourceImage.Width, sourceImage.Height);
            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage1.Width * 30));

                for (int j = 0; j < sourceImage.Height; j++)
                {
                    resultImage1.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
                }
            }

            kernel = new float[3, 3] { { -1.0f, -2.0f, 1.0f }, { 0f, 0f, 0f }, { 1.0f, 2.0f, 1.0f } };

            Bitmap resultImage2 = new Bitmap(sourceImage.Width, sourceImage.Height);
            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage1.Width * 30) + 30);

                for (int j = 0; j < sourceImage.Height; j++)
                {
                    resultImage2.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
                }
            }
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage1.Width * 30) + 30 + 40);
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    Color color1 = resultImage1.GetPixel(i, j);
                    Color color2 = resultImage2.GetPixel(i, j);
                    double R = Math.Sqrt(color1.R * color1.R + color2.R * color2.R);
                    double G = Math.Sqrt(color1.G * color1.G + color2.G * color2.G);
                    double B = Math.Sqrt(color1.B * color1.B + color2.B * color2.B);
                    Color result = Color.FromArgb(Clamp((int)R, 0, 255), Clamp((int)G, 0, 255), Clamp((int)B, 0, 255));
                    resultImage.SetPixel(i, j, result);
                }
            }
            return resultImage;
        }
    }

    class GaussianFilter: MatrixFilter
    {
        public void createGaussianKernel(int radius, float sigma)
        {
            int size = 2 * radius + 1;
            kernel = new float[size, size];
            float norm = 0;
            for(int i = -radius; i <= radius; i++)
            {
                for(int j = -radius; j <= radius; j++)
                {
                    kernel[i + radius, j + radius] = (float)(Math.Exp(-(i * i + j * j) / (2 * sigma * sigma)));
                    norm += kernel[i + radius, j + radius];
                }
            }
            for (int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    kernel[i, j] /= norm;
                }
            }
        }

        public GaussianFilter()
        {
            createGaussianKernel(3, 2);
        }
    }

    class MedianFilter: Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int size = 5;
            int rad = size / 2;
            int s = size * size;

            int[] R = new int[s];
            int[] G = new int[s];
            int[] B = new int[s];

            int k = 0;

            for (int i = -rad; i <= rad; i++)
            {
                for (int j = -rad; j <= rad; j++)
                {
                    int idX = Clamp(x + j, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + i, 0, sourceImage.Height - 1);
                    Color sourceColor = sourceImage.GetPixel(idX, idY);
                    R[k] = sourceColor.R;
                    G[k] = sourceColor.G;
                    B[k] = sourceColor.B;
                    k++;
                }
            }
            Array.Sort(R);
            Array.Sort(G);
            Array.Sort(B);

            return Color.FromArgb(R[s / 2], G[s / 2], B[s / 2]);
        }

        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
                }
            }
            return resultImage;
        }
    }

    class SteckloFilter: Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int k, int l)
        {
            Random rnd = new Random();
            double rand = rnd.NextDouble();
            int x = (int)(k + (rand - 0.5) * 10);
            int y = (int)(l + (rand - 0.5) * 10);
            if ((x < sourceImage.Width) && (x >= 0) && (y < sourceImage.Height) && (y >= 0))
            {
                Color sourceColor = sourceImage.GetPixel(x, y);
                Color resultColor = sourceColor;
                return resultColor;
            }
            else
            {
                Color resultColor = Color.FromArgb(0, 0, 0);
                return resultColor;
            }
        }
    }

    class VolnaFilter: Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int k, int l)
        {
            int x = (int)(k + 20 * Math.Sin(2 * Math.PI * l / 60));
            int y = l;
            if ((x < sourceImage.Width) && (x >= 0) && (y < sourceImage.Height) && (y >= 0))
            {
                Color sourceColor = sourceImage.GetPixel(x, y);
                Color resultColor = sourceColor;
                return resultColor;
            }
            else
            {
                Color resultColor = Color.FromArgb(0, 0, 0);
                return resultColor;
            }
        }
    }

    class TisnenFilter : Filters
    {
        //kernel = new float[3, 3] { { 0f, 1.0f, 0f }, { 1.0f, 0f, -1.0f }, { 0f, -1.0f, 0f } };

        public Color PixelColor(Bitmap Image, int x, int y)
        {
            Color sourceColor = Image.GetPixel(x, y);
            double Intensity = 0.299 * sourceColor.R + 0.587 * sourceColor.G + 0.144 * sourceColor.B;
            Color resultColor = Color.FromArgb(Clamp((int)Intensity, 0, 255), Clamp((int)Intensity, 0, 255), Clamp((int)Intensity, 0, 255));

            return resultColor;
        }
        public Bitmap Grey(Bitmap Image)
        {
            Bitmap resultImage = new Bitmap(Image.Width, Image.Height);
            for (int i = 0; i < Image.Width; i++)
            {
                for (int j = 0; j < Image.Height; j++)
                {
                    resultImage.SetPixel(i, j, PixelColor(Image, i, j));
                }
            }
            return resultImage;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            float[,] kernel = new float[3, 3] { { 0f, 1.0f, 0f }, { 1.0f, 0f, -1.0f }, { 0f, -1.0f, 0f } };
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;
            float resultR = 0;
            float resultG = 0;
            float resultB = 0;
            for (int l = -radiusY; l <= radiusY; l++)
            {
                for (int k = -radiusX; k <= radiusX; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    resultR += neighborColor.R * kernel[k + radiusX, l + radiusY];
                    resultG += neighborColor.G * kernel[k + radiusX, l + radiusY];
                    resultB += neighborColor.B * kernel[k + radiusX, l + radiusY];
                }
            }
            return Color.FromArgb(Clamp((int)resultR, 0, 255), Clamp((int)resultG, 0, 255), Clamp((int)resultB, 0, 255));
        }

        public Bitmap Norm(Bitmap image)
        {
            Bitmap result = new Bitmap(image.Width, image.Height);
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color pixel = image.GetPixel(i, j);
                    pixel = Color.FromArgb((pixel.R + 255) / 2, (pixel.G + 255) / 2, (pixel.B + 255) / 2);
                    result.SetPixel(i, j, pixel);
                }
            }
            return result;
        }

        public override Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            sourceImage = Grey(sourceImage);
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
                }
            }
            resultImage = Norm(resultImage);
            return resultImage;
        }
    }
}
