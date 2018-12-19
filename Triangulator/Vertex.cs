using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGLib.Graphics;
using CGLib.Math;

namespace Triangulator
{
	public class Vertex
	{
		public Vector2 Position;
		public List<PolygonEdge> Edges { get; set; } = new List<PolygonEdge>();

		public bool Selected { get; set; } = false;

		public int index = 0;

		static int TIndex = 0;

		public Vertex(Vector2 position)
		{
			index = TIndex++;

			Position = position;
		}
	}
}
