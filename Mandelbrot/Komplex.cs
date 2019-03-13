using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandelbrot
{
    class Komplex
    {
        public double A { get; private set; }
        public double B { get; private set; }
        public Komplex(double a, double b)
        {
            this.A = a;
            this.B = b;
        }
        public static Komplex operator +(Komplex z, Komplex u)
        {
            return new Komplex(z.A + u.A, z.B + u.B);
        }
        public static Komplex operator *(Komplex z, Komplex u)
        {
            return new Komplex(z.A * u.A - z.B * u.B, z.A * u.B + z.B * u.A);
        }
        public double Abs()
        {
            return Math.Sqrt(A * A + B * B);
        }
        public Komplex Im()
        {
            return new Komplex(0, B);
        }
        public Komplex Re()
        {
            return new Komplex(A, 0);   
        }
    }
}
