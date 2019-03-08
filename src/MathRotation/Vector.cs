using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathRotation
{
    class Vector
    {
        private double x, y, z;

        public Vector()
        {
            x = y = z = 0;
        }
        public Vector(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public Vector(int x, int y, int z)
        {
            this.x = (double)x;
            this.y = (double)y;
            this.z = (double)z;
        }

        public double X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }
        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }
        public double Z
        {
            get
            {
                return z;
            }
            set
            {
                z = value;
            }
        }
        public double Norm
        {
            get
            {
                return Math.Sqrt(x * x + y * y + z * z);
            }
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.x + b.x, a.y + b.y, a.z + b.z);
        }
        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        //dot product
        public static double operator *(Vector a, Vector b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        //cross product
        public static Vector operator ^(Vector a, Vector b)
        {
            Vector r = new Vector();
            r.x = a.y * b.z - a.z * b.y;
            r.y = a.z * b.x - a.x * b.z;
            r.z = a.x * b.y - a.y * b.x;
            return r;
        }

        public static Vector operator *(double c, Vector a)
        {
            return new Vector(c * a.x, c * a.y, c * a.z);
        }

        //unary minus
        public static Vector operator -(Vector a)
        {
            return new Vector(-a.x, -a.y, -a.z);
        }

        public void Normalize()
        {
            double norm = this.Norm;
            if (norm != 0.0)
            {
                x /= norm;
                y /= norm;
                z /= norm;
            }
        }

        public Vector Clone()
        {
            return new Vector(x, y, z);
        }
        public override bool Equals(object b)
        {
            return (((Vector)b).x == x && ((Vector)b).y == y && ((Vector)b).z == z) ? true : false;
        }
        public bool Equals(Vector b)
        {
            return (b.x == x && b.y == y && b.z == z) ? true : false;
        }
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
