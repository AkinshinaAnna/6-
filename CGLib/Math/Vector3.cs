using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGLib.Math
{
    public struct Vector3
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3(float X = 0, float Y = 0, float Z = 0)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        public Vector3(Vector2 v, float z)
        {
            this.X = v.X;
            this.Y = v.Y;
            this.Z = z;
        }

        public Vector3(Vector3 v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }

        public float Length()
        {
            return (float)System.Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public float Distance(Vector3 v)
        {
            return (float)System.Math.Sqrt(System.Math.Pow(X - v.X, 2) 
				+ System.Math.Pow(Y - v.Y, 2) + System.Math.Pow(Z - v.Z, 2));
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vector3 operator *(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }

        public static Vector3 operator *(Vector3 a, float b)
        {
            return new Vector3(a.X * b, a.Y * b, a.Z * b);
        }
    }
}
