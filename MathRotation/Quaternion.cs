using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathRotation
{
    class Quaternion
    {
        public const double PI = Math.PI;
        public const double HALFPI = (PI / 2.0);

        private double real;
        private Vector imaginary;

        public Quaternion()
        {
            this.real = 1;
            this.imaginary = new Vector();
        }
        public Quaternion(Vector axis, double theta)
        {
            double sin, cos;
            Vector a = axis.Clone();
            a.Normalize();

            sin = Math.Sin(GradToRad(theta) / 2.0);
            cos = Math.Cos(GradToRad(theta) / 2.0);
            this.real = cos;
            double x = a.X * sin;
            double y = a.Y * sin;
            double z = a.Z * sin;
            this.imaginary = new Vector(x, y, z);
        }
        public Quaternion(Vector v)
        {
            this.real = 0.0;
            double x = v.X;
            double y = v.Y;
            double z = v.Z;
            this.imaginary = new Vector(x, y, z);
        }
        public Quaternion(double w, double x, double y, double z)
        {
            this.real = w;
            this.imaginary = new Vector(x, y, z);
        }
        public Quaternion(double real, Vector imaginary)
        {
            this.real = real;
            double x = imaginary.X;
            double y = imaginary.Y;
            double z = imaginary.Z;
            this.imaginary = new Vector(x, y, z);
        }

        public double Real
        {
            get
            {
                return this.real;
            }
            set
            {
                this.real = value;
            }
        }
        public Vector Imaginary
        {
            get
            {
                return this.imaginary;
            }
            set
            {
                this.imaginary = value;
            }
        }
        public double Norm
        {
            get
            {
                return Math.Sqrt(this.real * this.real + this.imaginary * this.imaginary);
            }
        }

        public static Quaternion operator +(Quaternion a, Quaternion b)
        {
            return new Quaternion(a.real + b.real, a.imaginary + b.imaginary);
        }
        public static Quaternion operator -(Quaternion a, Quaternion b)
        {
            return new Quaternion(a.real - b.real, a.imaginary - b.imaginary);
        }
        public static Quaternion operator *(Quaternion a, Quaternion b)
        {
            double w = a.real * b.real - a.Imaginary.X * b.Imaginary.X - a.Imaginary.Y * b.Imaginary.Y - a.Imaginary.Z * b.Imaginary.Z;
            double x = a.real * b.Imaginary.X + a.Imaginary.X * b.Real + a.Imaginary.Y + b.Imaginary.Z - a.Imaginary.Z * b.Imaginary.Y;
            double y = a.real * b.Imaginary.Y + a.imaginary.Y * b.Real + a.Imaginary.Z * b.Imaginary.X - a.Imaginary.X * b.Imaginary.Z;
            double z = a.real * b.Imaginary.Z + a.Imaginary.Z * b.Real + a.Imaginary.X * b.Imaginary.Y - a.Imaginary.Y * a.Imaginary.X;
            return new Quaternion(w, x, y, z);

            /*
             * double real = a.real * b.real - a.Imaginary * b.Imaginary;
             * Vector imag = a.real * b.Imaginary + b.real * a.Imaginary + a.Imaginary ^ b.Imaginary;
             * return new Quaternion();
             */
        }
        //unary minus
        public static Quaternion operator -(Quaternion a)
        {
            return new Quaternion(-a.real, -a.Imaginary);
        }

        public Quaternion Conjugate()
        {
            return new Quaternion(this.real, -this.imaginary);
        }
        public Quaternion Inverse()
        {
            Quaternion q = new Quaternion(this.real, this.imaginary.Clone()).Conjugate();
            q.Normalize();
            return q;
        }
        public void Normalize()
        {
            double norm = this.Norm;
            this.real /= norm;
            this.Imaginary.X /= norm;
            this.Imaginary.Y /= norm;
            this.Imaginary.Z /= norm;
        }

        public Quaternion Clone()
        {
            return new Quaternion(this.real, this.imaginary.Clone());
        }
        public override bool Equals(object q)
        {
            return (((Quaternion)q).real == this.real &&
            ((Quaternion)q).imaginary.X == this.imaginary.X &&
            ((Quaternion)q).imaginary.Y == this.imaginary.Y &&
            ((Quaternion)q).imaginary.Z == this.imaginary.Z)? true: false;
        }
        public bool Equals(Quaternion q)
        {
            return (q.real == this.real &&
            q.imaginary.X == this.imaginary.X &&
            q.imaginary.Y == this.imaginary.Y &&
            q.imaginary.Z == this.imaginary.Z) ? true : false;
        }
        public override int GetHashCode()
        {
            return 0;
        }

        private double GradToRad(double theta)
        {
            return (PI * theta / 180.0);
        }
    }
}
