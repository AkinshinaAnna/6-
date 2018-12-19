using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPoint = System.Drawing.Point;
using CPointF = System.Drawing.PointF;

namespace CGLib.Math
{
	public struct Vector2
	{
		public float[] _data;
		public float this[int index] { get => _data[index]; set => _data[index] = value; }

		public float X { get => _data[0]; set => _data[0] = value; }
		public float Y { get => _data[1]; set => _data[1] = value; }

		public float Length
		{
			get
			{
				return (float)System.Math.Sqrt(X * X + Y * Y);
			}
		}

		public Vector2(float x = 0.0f, float y = 0.0f)
		{
			_data = new float[] { x, y };
		}

		public Vector2(CPoint point)
		{
			_data = new float[] { point.X, point.Y };
		}

		public Vector2(CPointF point)
		{
			_data = new float[] { point.X, point.Y };
		}

		public Vector2(Vector2 vec)
		{
			_data = new float[] { vec.X, vec.Y };
		}

		public float DistanceTo(Vector2 vec)
		{
			return (this - vec).Length;
		}

		public CPoint ToPoint()
		{
			return new CPoint((int)this.X, (int)this.Y);
		}

		public CPointF ToPointF()
		{
			return new CPointF(this.X, this.Y);
		}

		public float Cross(Vector2 vec)
		{
			return X * vec.Y - Y * vec.X;
		}

		public static float Dot(Vector2 a, Vector2 b) {
			return a.X * b.X + a.Y * b.Y;
		}

		public static float Distance(Vector2 a, Vector2 b)
		{
			return a.DistanceTo(b);
		}

		public static float CosAngle(Vector2 a, Vector2 b)
		{
			return Dot(a, b) / (a.Length * b.Length);
		}

		public static Vector2 operator+(Vector2 a, Vector2 b)
		{
			return new Vector2(a.X + b.X, a.Y + b.Y);
		}

		public static Vector2 operator-(Vector2 a, Vector2 b)
		{
			return new Vector2(a.X - b.X, a.Y - b.Y);
		}

		public static Vector2 operator *(Vector2 a, Vector2 b)
		{
			return new Vector2(a.X * b.X, a.Y * b.Y);
		}

		public static Vector2 operator *(Vector2 a, float v)
		{
			return new Vector2(a.X * v, a.Y * v);
		}

		public static Vector2 operator /(Vector2 a, float v)
		{
			return new Vector2(a.X / v, a.Y / v);
		}

		public static Vector2 operator /(Vector2 a, Vector2 b)
		{
			return new Vector2(a.X / b.X, a.Y / b.Y);
		}

		public override string ToString()
		{
			return "(" + X + ", " + Y + ")";
		}
	}
}
