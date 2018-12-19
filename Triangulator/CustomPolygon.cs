using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGLib.Graphics;
using CGLib.Math;
using CGraphics = System.Drawing.Graphics;
using CColor = System.Drawing.Color;
using CPen = System.Drawing.Pen;
using CBrush = System.Drawing.Brush;
using CBrushes = System.Drawing.Brushes;
using CRectF = System.Drawing.RectangleF;

namespace Triangulator
{
	public class CustomPolygon : IDrawable
	{
		public CColor Color { get; set; } = CColor.Black;

		public List<Vertex> Vertices { get; set; } = new List<Vertex>();

		private const int POINT_CIRCLE_RADIUS = 10;

		public CustomPolygon()
		{

		}

		public void Clear()
		{
			Vertices.Clear();
		}

		public void AddVertex(Vertex vertex)
		{
			Vertices.Add(vertex);
		}

		public void AddEdge(int startIndex, int endIndex)
		{
			Vertices[startIndex].Edges.Add(new PolygonEdge(Vertices[startIndex], Vertices[endIndex]));
			Vertices[endIndex].Edges.Add(new PolygonEdge(Vertices[endIndex], Vertices[startIndex]));
		}

		public void AddEdge(Vertex start, Vertex end)
		{
			start.Edges.Add(new PolygonEdge(start, end));
			end.Edges.Add(new PolygonEdge(start, end));
		}

		public Vertex FindVertexByIndex(int index)
		{
			return Vertices[index];
		}

		public int GetMaxVertexDegree()
		{
			int maxDegree = 0;

			Vertices.ForEach((v) => maxDegree = Math.Max(maxDegree, v.Edges.Count));

			return maxDegree;
		}

		public void RemoveEdge(PolygonEdge edge)
		{
			edge.StartVertex.Edges.RemoveAll((e) => e.StartVertex == edge.StartVertex && e.EndVertex == edge.EndVertex
				|| e.EndVertex == edge.StartVertex && e.StartVertex == edge.EndVertex);

			edge.EndVertex.Edges.RemoveAll((e) => e.StartVertex == edge.StartVertex && e.EndVertex == edge.EndVertex
				|| e.EndVertex == edge.StartVertex && e.StartVertex == edge.EndVertex);
		}

		public List<Vertex> GetVerticesStrip()
		{
			List<Vertex> strip = new List<Vertex>();

			Vertex startVertex = Vertices[0];
			Vertex currentVertex = startVertex.Edges[0].EndVertex;

			PolygonEdge prevEdge = startVertex.Edges[0];

			strip.Add(startVertex);
			strip.Add(currentVertex);

			while (currentVertex != startVertex)
			{
				PolygonEdge currentEdge = currentVertex.Edges.Find((e) => !(e.EndVertex == prevEdge.StartVertex && e.StartVertex == prevEdge.EndVertex));
				currentVertex = currentEdge.EndVertex;
				strip.Add(currentVertex);

				prevEdge = currentEdge;
			}

			return strip;
		}

		public void FindNearestEdgeIndex(Vector2 point)
		{
			
		}

		public bool IsClosed()
		{
			return false;
		}

		public bool IsSelfIntersecting()
		{
			return false;
		}

		public void DFSTraverse(int index, bool[] visited)
		{
			Stack<int> dfsStack = new Stack<int>();
			dfsStack.Push(index);
			visited[index] = true;

			for (int i = 0; i < Vertices[index].Edges.Count; ++i)
			{
				PolygonEdge edge = Vertices[index].Edges[i];

				int targetIndex = Vertices.IndexOf(edge.EndVertex);

				if (!visited[targetIndex])
					DFSTraverse(targetIndex, visited);
			}
		}

		public int GetConnectedComponentsCount()
		{
			List<int> components = new List<int>();

			bool[] visited = new bool[Vertices.Count];

			for (int i = 0; i < Vertices.Count; ++i)
				visited[i] = false;

			int count = 0;

			for (int i = 0; i < Vertices.Count; ++i)
				if (!visited[i]) {
					DFSTraverse(i, visited);
					count++;
				}

			return count;
		}

		public void RemoveVertex(int index)
		{
			foreach (PolygonEdge edge in Vertices[index].Edges) {
				Vertex ajacement = (edge.StartVertex == Vertices[index]) ?
					edge.EndVertex : edge.StartVertex;

				ajacement.Edges.
					RemoveAll((e) => e.StartVertex == Vertices[index] || e.EndVertex == Vertices[index]);
			}

			Vertices.RemoveAt(index);
		}

		public void PickVertex(int index)
		{
			ResetPickState();
			Vertices[index].Selected = true;
		}

		public void ResetPickState()
		{
			Vertices.ForEach((v) => {
				v.Selected = false;

				v.Edges.ForEach((e) => e.Selected = false);
			});
		}

		public void PickEdge(PolygonEdge edge)
		{
			ResetPickState();

			edge.StartVertex.Edges.FindAll((e) => e.StartVertex == edge.StartVertex && e.EndVertex == edge.EndVertex
				|| e.EndVertex == edge.StartVertex && e.StartVertex == edge.EndVertex).ForEach((e) => e.Selected = true);

			edge.EndVertex.Edges.FindAll((e) => e.StartVertex == edge.StartVertex && e.EndVertex == edge.EndVertex
				|| e.EndVertex == edge.StartVertex && e.StartVertex == edge.EndVertex).ForEach((e) => e.Selected = true);
		}

		public int FindNearestVertex(Vector2 point, Camera camera)
		{
			for (int i = 0; i < Vertices.Count; i++)
			{
				if (Geometry.IsPointInsideCircle(point, camera.LocalToScreen(Vertices[i].Position), (POINT_CIRCLE_RADIUS + 5)))
					return i;
			}

			return -1;
		}

		public PolygonEdge FindNearestEdge(Vector2 point, Camera camera)
		{
			foreach (Vertex vertex in Vertices)
				foreach (PolygonEdge edge in vertex.Edges) {
					Vector2 a = camera.LocalToScreen(edge.StartVertex.Position);
					Vector2 b = camera.LocalToScreen(edge.EndVertex.Position);

					float distance = Geometry.GetPointToSegmentDistance(a, b, point);


					if (distance <= 10)
					{
						return edge;
					}
				}

			return null;
		}

		public void Draw(DrawState state)
		{
			foreach (Vertex vertex in Vertices)
			{
				CBrush color = CBrushes.Black;

				if (vertex.Selected)
					color = CBrushes.Green;

				DrawPoint(state.Graphics, color, state.Camera.LocalToScreen(vertex.Position));
			}

			foreach (Vertex vertex in Vertices)
			{
				foreach (PolygonEdge edge in vertex.Edges)
				{
					CColor color = CColor.Black;

					if (edge.Selected)
						color = CColor.Red;

					DrawLine(state.Graphics, color,
						state.Camera.LocalToScreen(edge.StartVertex.Position),
						state.Camera.LocalToScreen(edge.EndVertex.Position));
				}
			}
		}

		private void DrawLine(CGraphics g, CColor color, Vector2 a, Vector2 b)
		{
			g.DrawLine(new CPen(color), a.ToPoint(), b.ToPoint());
		}

		private void DrawPoint(CGraphics g, CBrush color, Vector2 center)
		{
			g.FillEllipse(color, new CRectF(center.X - POINT_CIRCLE_RADIUS, center.Y - POINT_CIRCLE_RADIUS,
				POINT_CIRCLE_RADIUS * 2, POINT_CIRCLE_RADIUS * 2));
		}
	}
}
