using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMath = System.Math;

namespace CGLib.Math
{
	public class Geometry
	{
		public static bool IsPointInsideCircle(Vector2 point, Vector2 circleCenter, float circleRadius)
		{
			return point.DistanceTo(circleCenter) <= circleRadius;
		}

		public static bool IsPointOnSegment(Vector2 q, Vector2 p, Vector2 r)
		{
			if (q.X <= CMath.Max(p.X, r.X) && q.X >= CMath.Min(p.X, r.X) &&
				q.Y <= CMath.Max(p.Y, r.Y) && q.Y >= CMath.Min(p.Y, r.Y))
				return true;

			return false;
		}

		public static float GetPointToSegmentDistance(Vector2 A, Vector2 B, Vector2 C)
		{
			float dx = B.X - A.X;
			float dy = B.Y - A.Y;

			float sq = dx * dx + dy * dy;
			float t = ((C.X - A.X) * dx + (C.Y - A.Y) * dy) / sq;

			if (t > 1)
				t = 1;
			else if (t < 0)
				t = 0;

			float x = A.X + t * dx;
			float y = A.Y + t * dy;

			return (float)System.Math.Sqrt(System.Math.Pow(x - C.X, 2) + System.Math.Pow(y - C.Y, 2));
		}


		public static int GetTripletOrientation(Vector2 p, Vector2 q, Vector2 r)
		{
			float val = (q.Y - p.Y) * (r.X - q.X) -
					  (q.X - p.X) * (r.Y - q.Y);

			if (CMath.Abs(val) <= 1e-5)
				return 0; 

			return (val > 0) ? 1 : 2;
		}

		public static bool AreSegmentsInterseting(Vector2 p1, Vector2 q1, Vector2 p2, Vector2 q2)
		{
			int o1 = GetTripletOrientation(p1, q1, p2);
			int o2 = GetTripletOrientation(p1, q1, q2);
			int o3 = GetTripletOrientation(p2, q2, p1);
			int o4 = GetTripletOrientation(p2, q2, q1);

			if (o1 != o2 && o3 != o4)
				return true;

			if (o1 == 0 && IsPointOnSegment(p1, p2, q1)) return true;
			if (o2 == 0 && IsPointOnSegment(p1, q2, q1)) return true;
			if (o3 == 0 && IsPointOnSegment(p2, p1, q2)) return true;
			if (o4 == 0 && IsPointOnSegment(p2, q1, q2)) return true;

			return false;
		}

		public static float CalculateTiangleArea(Vector2 v1, Vector2 v2, Vector2 v3)
		{
			float a = v1.Length;
			float b = v2.Length;
			float c = v3.Length;

			float p = (a + b + c) / 2;

			return (float)CMath.Sqrt(p * (p - a) * (p - b) * (p - c));
		}


		public static bool IsPointInsideTriangle(Vector2 pt, Vector2 v0, Vector2 v1, Vector2 v2)
		{
			float s1 = CalculateTiangleArea(pt, v0, v1);
			float s2 = CalculateTiangleArea(pt, v1, v2);
			float s3 = CalculateTiangleArea(pt, v0, v2);

			return CMath.Abs(s1 + s2 + s3 - CalculateTiangleArea(v0, v1, v2)) <= 1e-6;
		}

		public static bool IsTripleLeftRotated(Vector2 a, Vector2 b, Vector2 c)
		{
			Vector2 ab = b - a;
			Vector2 bc = c - b;

			return (ab.X * bc.Y - ab.Y * bc.X) >= 0;
		}
	}
}
