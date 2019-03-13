using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mandelbrot
{
    public partial class Form1 : Form
    {
        List<Color> colorList = new List<Color>();
        double balX = -2, balY = 1, jobbX = 1, jobbY = -1;
        double transzA1, transzA2, transzB1, transzB2;
        int iter = 300;
        int kilep = 0;
        Bitmap bitmap;



        int egerLeX, egerLeY;

       

        public Form1()
        {
            InitializeComponent();
            Szinatmenet();
            SzamolRajzol();
            textBox1.Text = iter.ToString();
        }

        private void Szinatmenet()
        {
            colorList = new List<Color>();
            int size = iter;

            int rMax = 255;
            int gMax = 255;
            int bMax = 255;
            int rMin = 66;
            int gMin = 100;
            int bMin = 147;
            for (int i = 1; i < size; i++)
            {
                var rAverage = rMin + (int)((rMax - rMin) * (-Math.Log10(i * 1.0 / size)/Math.Log10(size)));
                var gAverage = gMin + (int)((gMax - gMin) * (-Math.Log10(i * 1.0 / size) / Math.Log10(size)));
                var bAverage = bMin + (int)((bMax - bMin) * (-Math.Log10(i * 1.0 / size) / Math.Log10(size)));
                colorList.Add(Color.FromArgb(rAverage, gAverage, bAverage));
            }
        }
        private void SzamolRajzol()
        {
            int width = 1500;
            int height = 1000;

            if (Math.Abs(jobbX - balX) / Math.Abs(jobbY - balY) > 1.5)
            {
                height = (int)(width / (Math.Abs(jobbX - balX) / Math.Abs(jobbY - balY)));
            }
            else
            {
                width = (int)(height * (Math.Abs(jobbX - balX) / Math.Abs(jobbY - balY)));
            }

            int[,] pixelek = new int[width, height];
            bitmap = new Bitmap(width, height);
            transzA1 = (jobbX - balX) / width;
            transzA2 = balX;
            transzB1 = (jobbY - balY) / height;
            transzB2 = balY;

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    double a = transzA1 * i + transzA2;
                    double b = transzB1 * j + transzB2;
                    //Komplex c = new Komplex(-0.8, 0.156);
                    Komplex c = new Komplex(a, b);
                    Komplex z = new Komplex(a, b);
                    //Komplex u = new Komplex(a, b);
                    //Komplex w = new Komplex(a, b);
                    kilep = 0;
                    for (int k = 0; k < iter; k++)
                    {
                        z = z * z + c; // Mandelbrot
                        //z = z * z * z * z + c;
                        //w = new Komplex(u.A, u.B);
                        //u = u * u + c.Re() + c.Im() * z;
                        //z = new Komplex(w.A, w.B);
                        if (z.Abs() > 2)
                        {
                            kilep = k;
                            break;
                        }
                        kilep = k;
                    }
                    pixelek[i, j] = kilep;
                }
            }
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    Color color;
                    color = colorList[(int)(colorList.Count * pixelek[i, j] * 1.0 / iter)];
                    //if (pixelek[i, j] != iter )
                    //{
                    //    color = colorList[(int)(colorList.Count * pixelek[i, j] * 1.0 / iter)];
                    //}
                    //else
                    //{
                    //    color = Color.Black;
                    //}
                    bitmap.SetPixel(i, j, color);
                }
            }
            pictureBox1.Image = bitmap;
        }
        

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            double leX = transzA1 * egerLeX + transzA2, leY = transzB1 * egerLeY + transzB2;
            double felX = transzA1 * e.X + transzA2, felY = transzB1 * e.Y + transzB2;

            if (leX < felX)
            {
                balX = leX;
                balY = leY;
                jobbX = felX;
                jobbY = felY;
            }
            else
            {
                balX = felX;
                balY = felY;
                jobbX = leX;
                jobbY = leY;
            }
            SzamolRajzol();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            egerLeX = e.X;
            egerLeY = e.Y;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            iter = int.Parse(textBox1.Text);
            Szinatmenet();
            SzamolRajzol();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            iter = int.Parse(textBox1.Text);
            balX = -2;
            balY = 1;
            jobbX = 1;
            jobbY = -1;
            Szinatmenet();
            SzamolRajzol();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bitmap.Save(@"C:\Users\Samu\Desktop\sajt.png");
        }

    }
}
