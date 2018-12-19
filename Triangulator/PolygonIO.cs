using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CGLib.Math;

namespace Triangulator
{
	public class PolygonIO
	{
		public static CustomPolygon LoadFromFile(string fileName)
		{
			CustomPolygon polygon = new CustomPolygon();

			string[] data = File.ReadAllLines(fileName);

			foreach (string row in data)
			{
				if (string.IsNullOrEmpty(row))
					continue;

				string[] parts = row.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

				Vector2 position = new Vector2(float.Parse(parts[1]), float.Parse(parts[2]));

				if (parts[0] == "v")
					polygon.AddVertex(new Vertex(position));
				else if (parts[0] == "e")
					polygon.AddEdge(int.Parse(parts[1]), int.Parse(parts[2]));
			}

			return polygon;
		}

		public static void WriteToFile(string fileName, CustomPolygon polygon)
		{
			List<string> rows = new List<string>();

			foreach (Vertex vertex in polygon.Vertices)
				rows.Add("v " + vertex.Position.X + " " + vertex.Position.Y);

			foreach (Vertex vertex in polygon.Vertices)
				foreach (PolygonEdge edge in vertex.Edges)
				{
					int startIndex = polygon.Vertices.IndexOf(edge.StartVertex);
					int endIndex = polygon.Vertices.IndexOf(edge.EndVertex);

					rows.Add("e " + startIndex + " " + endIndex);
				}

			File.WriteAllLines(fileName, rows);
		}
	}
}