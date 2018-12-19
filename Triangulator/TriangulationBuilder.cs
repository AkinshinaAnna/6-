using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGLib.Math;
using System.Windows.Forms;

namespace Triangulator
{
	public class TriangulationBuilder
	{
		

		public static float get_area(List<Vertex> contour) {
			int n = contour.Count;

			float A = 0.0f;

			for (int p = n - 1, q = 0; q<n; p = q++) {
				A += contour[p].Position.Cross(contour[q].Position);
			}

			return A* 0.5f;
}


public static void MonotoneCutting(CustomPolygon polygon)
		{
			List<Vertex> contour = polygon.GetVerticesStrip();

			int n = contour.Count;
			if (n < 3) return;

			List<int> V = new List<int>();

			for (int i = 0; i < n; i++)
				V.Add(0);
			
		
			if (0.0f < get_area(contour))
				for (int v = 0; v < n; v++)
					V[v] = v;
			else
				for (int v = 0; v < n; v++)
					V[v] = (n - 1) - v;

			int nv = n;

			/*  remove nv-2 Vertices, creating 1 triangle every time */
			int count = 2 * nv; /* error detection */

			for (int v = nv - 1; nv > 2;)
			{
				/* if we loop, it is probably a non-simple polygon */
				if (0 >= (count--))
				{
					return;
				}

				/* three consecutive vertices in current polygon, <u,v,w> */
				int u = v;
				if (nv <= u) u = 0; /* previous */
				v = u + 1;
				if (nv <= v) v = 0; /* new v    */
				int w = v + 1;
				if (nv <= w) w = 0; /* next     */

				if (snip(contour, u, v, w, nv, V))
				{
					int a, b, c, s, t;

					/* true names of the vertices */
					a = V[u];
					b = V[v];
					c = V[w];

					/* output Triangle */
					polygon.AddEdge(a, b);
					polygon.AddEdge(b, c);
					polygon.AddEdge(a, c);

					/* remove v from remaining polygon */
					for (s = v, t = v + 1; t < nv; s++, t++)
						V[s] = V[t];

					nv--;

					/* resest error detection counter */
					count = 2 * nv;
				}
			}
		}

		public static bool snip(List<Vertex> contour, int u, int v, int w, int n, List<int> V) {
			int p;
			float Ax, Ay, Bx, By, Cx, Cy, Px, Py;

			Ax = contour[V[u]].Position.X;
			Ay = contour[V[u]].Position.Y;

			Bx = contour[V[v]].Position.X;
			By = contour[V[v]].Position.Y;

			Cx = contour[V[w]].Position.X;
			Cy = contour[V[w]].Position.Y;

			if (1e-5 > (((Bx - Ax) * (Cy - Ay)) - ((By - Ay) * (Cx - Ax)))) return false;

	for (p = 0; p<n; p++) {
		if ((p == u) || (p == v) || (p == w)) continue;
		Px = contour[V[p]].Position.X;
				Py = contour[V[p]].Position.Y;
		if (is_inside_triangle(Ax, Ay, Bx, By, Cx, Cy, Px, Py)) return false;
	}



	return true;
}

		public static bool is_inside_triangle(float Ax, float Ay,
		float Bx, float By,
		float Cx, float Cy,
		float Px, float Py)

		{
			float ax, ay, bx, by, cx, cy, apx, apy, bpx, bpy, cpx, cpy;
			float cCROSSap, bCROSScp, aCROSSbp;

			ax = Cx - Bx;
			ay = Cy - By;
			bx = Ax - Cx;
			by = Ay - Cy;
			cx = Bx - Ax;
			cy = By - Ay;
			apx = Px - Ax;
			apy = Py - Ay;
			bpx = Px - Bx;
			bpy = Py - By;
			cpx = Px - Cx;
			cpy = Py - Cy;

			aCROSSbp = ax * bpy - ay * bpx;
			cCROSSap = cx * apy - cy * apx;
			bCROSScp = bx * cpy - by * cpx;

			return ((aCROSSbp >= 0.0) && (bCROSScp >= 0.0) && (cCROSSap >= 0.0));
		}

        /*
		public static void EarCuttingTriangulate(CustomPolygon polygon)
		{
			List<Vertex> verticesStrip = polygon.GetVerticesStrip();
			verticesStrip.Remove(verticesStrip.Last());

			List<int> map = new List<int>(verticesStrip.Count);

			for (int pointIndex = 0; pointIndex < verticesStrip.Count; pointIndex++)
				map.Add(pointIndex);

			while (verticesStrip.Count > 3)
			{
				bool processed = false;

				for (int pointIndex = 1; pointIndex < verticesStrip.Count - 1; pointIndex++)
				{
					Vector2 a = verticesStrip[pointIndex - 1].Position;
					Vector2 b = verticesStrip[pointIndex].Position;
					Vector2 c = verticesStrip[pointIndex + 1].Position;

					if (!Geometry.IsTripleLeftRotated(a, b, c))
					{
						//MessageBox.Show("Невыпуклая: " + verticesStrip[pointIndex].index);

						continue;
					}

					if (Geometry.CalculateTiangleArea(a, b, c) <= 1e-6)
						continue;

					bool result = true;

					for (int trianglePointIndex = 0; trianglePointIndex < verticesStrip.Count; trianglePointIndex++)
					{
						if (Math.Abs(trianglePointIndex - pointIndex) <= 1)
							continue;

						if (Geometry.IsPointInsideTriangle(verticesStrip[trianglePointIndex].Position, a, b, c))
						{
							result = false;

							//MessageBox.Show("Не ухо: " + verticesStrip[pointIndex].index);


							break;
						}
					}

					if (!result)
						continue;

					try {
						polygon.AddEdge(verticesStrip[map[pointIndex - 1]], verticesStrip[map[pointIndex]]);
						polygon.AddEdge(verticesStrip[map[pointIndex]], verticesStrip[map[pointIndex + 1]]);
						polygon.AddEdge(verticesStrip[map[pointIndex - 1]], verticesStrip[map[pointIndex + 1]]);
					}
					catch (Exception error) { }

					//MessageBox.Show("Ухо: " + verticesStrip[pointIndex].index);

					verticesStrip.RemoveAt(pointIndex);
					map.RemoveAt(pointIndex);

					processed = true;

					break;
				}

				if (!processed)
					break;
			}
		}

        public static void NaiveTriangulate(CustomPolygon polygon)
        {
            List<Vertex> verticesStrip = polygon.GetVerticesStrip();

            for (int i = 2; i < polygon.Vertices.Count; i++)
            {
                polygon.AddEdge(verticesStrip[0], verticesStrip[i - 1]);
                polygon.AddEdge(verticesStrip[0], verticesStrip[i - 2]);
                polygon.AddEdge(verticesStrip[i - 1], verticesStrip[i - 2]);
            }
        }
        */



    }
}
