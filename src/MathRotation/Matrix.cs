using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathRotation
{
    public enum VectorDirection { Row, Column};

    class Matrix
    {
        private const int DIM = 3;
        private double[,] mat;

        public Matrix()
        {
            //create identity matrix
            mat = new double[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };
        }
        public Matrix(Vector a, Vector b, Vector c, VectorDirection vd = VectorDirection.Column)
        {
            mat = new double[3, 3];
            if (vd == VectorDirection.Column)
            {
                mat[0, 0] = a.X;
                mat[1, 0] = a.Y;
                mat[2, 0] = a.Z;

                mat[0, 1] = b.X;
                mat[1, 1] = b.Y;
                mat[2, 1] = b.Z;

                mat[0, 2] = c.X;
                mat[1, 2] = c.Y;
                mat[2, 2] = c.Z;
            }
            else
            {
                mat[0, 0] = a.X;
                mat[0, 1] = a.Y;
                mat[0, 2] = a.Z;

                mat[1, 0] = b.X;
                mat[1, 1] = b.Y;
                mat[1, 2] = b.Z;

                mat[2, 0] = c.X;
                mat[2, 1] = c.Y;
                mat[2, 2] = c.Z;
            }
        }

        public double this[int row, int col]
        {
            get { return mat[row, col]; }
            set { mat[row, col] = value; }
        }

        public static Matrix operator +(Matrix a, Matrix b)
        {
            int row, col;
            Matrix r = new Matrix();
            for (row = 0; row < DIM; row++)
            {
                for (col = 0; col < DIM; col++)
                {
                    r[row, col] = a[row, col] + b[row, col];
                }
            }
            return r;
        }
        public static Matrix operator -(Matrix a, Matrix b)
        {
            int row, col;
            Matrix r = new Matrix();
            for (row = 0; row < DIM; row++)
            {
                for (col = 0; col < DIM; col++)
                {
                    r[row, col] = a[row, col] - b[row, col];
                }
            }
            return r;
        }
        public static Matrix operator *(Matrix a, Matrix b)
        {
            int row, col, i;
            Matrix r = new Matrix();
            for (row = 0; row < DIM; row++)
            {
                for (col = 0; col < DIM; col++)
                {
                    r[row, col] = 0;
                    for (i = 0; i < DIM; i++)
                    {
                        r[row, col] += a[row, i] * b[i, col];
                    }
                }
            }
            return r;
        }
        public static Vector operator *(Matrix a, Vector v)
        {
            Vector r = new Vector();
            r.X = v.X * a[0, 0] + v.Y * a[0, 1] + v.Z * a[0, 2];
            r.Y = v.X * a[1, 0] + v.Y * a[1, 1] + v.Z * a[1, 2];
            r.Z = v.X * a[2, 0] + v.Y * a[2, 1] + v.Z * a[2, 2];
            return r;
        }
        public static Matrix operator *(double c, Matrix a)
        {
            int row, col;
            Matrix r = new Matrix();
            for (row = 0; row < DIM; row++)
            {
                for (col = 0; col < DIM; col++)
                {
                    r[row, col] = a[row, col] * c;
                }
            }
            return r;
        }

        public Matrix Transpose()
        {
            int row, col;
            Matrix m = new Matrix();
            for (row = 0; row < DIM; row++)
            {
                for (col = 0; col < DIM; col++)
                {
                    m[row, col] = mat[col, row];
                }
            }
            return m;
        }
        //general rotation around the vector axis
        public static Matrix Rotate(Vector axis, double angle)
        {
            Matrix r = new Matrix();
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            axis.Normalize();
            r[0, 0] = (1 - cos) * axis.X * axis.X + cos;
            r[1, 0] = (1 - cos) * axis.X * axis.Y + sin * axis.Z;
            r[2, 0] = (1 - cos) * axis.X * axis.Z - sin * axis.Y;
            r[3, 0] = 0;

            r[0, 1] = (1 - cos) * axis.X * axis.Y - sin * axis.Z;
            r[1, 1] = (1 - cos) * axis.Y * axis.Y + cos;
            r[2, 1] = (1 - cos) * axis.Y * axis.Z + sin * axis.X;
            r[3, 1] = 0;

            r[0, 2] = (1 - cos) * axis.X * axis.Z + sin * axis.Y;
            r[1, 2] = (1 - cos) * axis.Y * axis.Z - sin * axis.X;
            r[2, 2] = (1 - cos) * axis.Z * axis.Z + cos;
            r[3, 2] = 0;

            r[0, 3] = 0;
            r[1, 3] = 0;
            r[2, 3] = 0;
            r[3, 3] = 1;

            return r;
        }

        public Matrix Clone()
        {
            Matrix m = new Matrix();
            int row, col;
            for (row = 0; row < DIM; row++)
            {
                for (col = 0; col < DIM; col++)
                {
                    m[row, col] = mat[row, col];
                }
            }
            return m;
        }
        public override bool Equals(object a)
        {
            Matrix m = (Matrix)a;
            int row, col;
            for (row = 0; row < DIM; row++)
            {
                for (col = 0; col < DIM; col++)
                {
                    if (m[row, col] != mat[row, col]) return false;
                }
            }
            return true;
        }
        public bool Equals(Matrix a)
        {
            int row, col;
            for (row = 0; row < DIM; row++)
            {
                for (col = 0; col < DIM; col++)
                {
                    if (a[row, col] != mat[row, col]) return false;
                }
            }
            return true;
        }
        public override int GetHashCode()
        {
            return 0;
        }

    }
}
