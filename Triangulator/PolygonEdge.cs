using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangulator
{
	public class PolygonEdge
	{
		public Vertex StartVertex { get; set; }
		public Vertex EndVertex { get; set; }

		public bool Selected { get; set; }

		public PolygonEdge(Vertex startIndex, Vertex endIndex)
		{
			StartVertex = startIndex;
			EndVertex = endIndex;
		}
	}
}
