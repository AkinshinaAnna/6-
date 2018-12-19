using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CGLib.Graphics;
using CGLib.Math;

namespace Triangulator { 
	public partial class MainWindow : Form
	{
		private Scene _scene;
		private CustomPolygon _polygon;

		private Vector2 _lastMousePosition = new Vector2();

		private int _pickedVertexIndex = -1;
		private PolygonEdge _pickedEdge = null;

		private const float _mouseZoomSpeed = 0.05f;
		private const float _mouseSpeed = 0.15f;
		
		public MainWindow()
		{
			InitializeComponent();
			this.MouseWheel += MainWindow_MouseWheel;
		}

		private void _timer_Tick(object sender, EventArgs e)
		{
			ResetView();
		}

		private void ResetView()
		{
			Invalidate();
		}

		private void MainWindow_Load(object sender, EventArgs e)
		{
			_polygon = new CustomPolygon();

			_scene = new Scene(Width, Height);
			_scene.AddObject(_polygon);

			_timer.Start();
		}

		private void MainWindow_Paint(object sender, PaintEventArgs e)
		{
			_scene.Draw(e.Graphics, e.ClipRectangle);
		}

		private void MainWindow_MouseWheel(object sender, MouseEventArgs e)
		{
			if (e.Delta > 0)
			{
				_scene.Camera.UpdateZoom(1 + _mouseZoomSpeed);
			}
			else
			{
				_scene.Camera.UpdateZoom(1 - _mouseZoomSpeed);
			}

			Invalidate();
		}

		private void MainWindow_Click(object sender, EventArgs e)
		{
		}

		private void MainWindow_MouseClick(object sender, MouseEventArgs e)
		{
			Vector2 localClickPoint = _scene.Camera.ScreenToLocal(new Vector2(e.Location));

			int pickedVertex = _polygon.FindNearestVertex(new Vector2(e.Location), _scene.Camera);
			PolygonEdge pickedEdge = _polygon.FindNearestEdge(new Vector2(e.Location), _scene.Camera);

			if (pickedVertex != -1 && e.Button == MouseButtons.Left) {
				if (_pickedVertexIndex == -1)
				{
					_pickedVertexIndex = pickedVertex;
					_pickedEdge = null;

					_polygon.PickVertex(pickedVertex);
				}
				else
				{
					if (pickedVertex != _pickedVertexIndex)
					{
						AddEdge(_pickedVertexIndex, pickedVertex);

						_pickedVertexIndex = -1;
						_pickedEdge = null;

						_polygon.ResetPickState();
					}
				}
			}
			else if (pickedEdge != null)
			{
				_pickedVertexIndex = -1;
				_pickedEdge = pickedEdge;
				_polygon.PickEdge(pickedEdge);
			}
			else
			{
				_polygon.ResetPickState();
				_pickedVertexIndex = -1;
				_pickedEdge = null;

				if (e.Button == MouseButtons.Left)
					_polygon.AddVertex(new Vertex(localClickPoint));

			}
		}

		private void AddEdge(int firstVertexIndex, int secondVertexIndex)
		{
			Vertex firstVertex = _polygon.FindVertexByIndex(firstVertexIndex);
			Vertex secondVertex = _polygon.FindVertexByIndex(secondVertexIndex);

			//foreach (PolygonEdge edge in _polygon.Edges)
			//{
			//	Vertex start = _polygon.FindVertexByIndex(edge.StartVertexIndex);
			//	Vertex end = _polygon.FindVertexByIndex(edge.EndVertexIndex);

			//	bool isAjacement = edge.StartVertexIndex == firstVertexIndex || edge.EndVertexIndex == firstVertexIndex ||
			//		edge.StartVertexIndex == secondVertexIndex || edge.EndVertexIndex == secondVertexIndex;

			//	if (!isAjacement && Geometry.AreSegmentsInterseting(start.Position, end.Position,
			//		firstVertex.Position, secondVertex.Position))
			//	{
			//		//return;
			//	}
			//}

			_polygon.AddEdge(firstVertexIndex, secondVertexIndex);
		}

		private void MainWindow_MouseMove(object sender, MouseEventArgs e)
		{
			Vector2 currentMousePosition = new Vector2(e.Location);

			if (e.Button == MouseButtons.Right) {
				Vector2 delta = (currentMousePosition - _lastMousePosition);
				delta.X = delta.X * (_scene.Camera.Size.X / 100);
				delta.Y = -delta.Y * (_scene.Camera.Size.Y / 100);

				_scene.Camera.Move(delta * _mouseSpeed);
			}
			else if (e.Button == MouseButtons.Left && _pickedVertexIndex != -1)
			{
				_polygon.FindVertexByIndex(_pickedVertexIndex).Position = _scene.Camera.ScreenToLocal(new Vector2(e.Location));
			}

			_lastMousePosition = currentMousePosition;
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == (Keys.Delete))
			{
				if (_pickedVertexIndex != -1)
				{
					_polygon.RemoveVertex(_pickedVertexIndex);
					_pickedVertexIndex = -1;
				}

				if (_pickedEdge != null)
				{
					_polygon.RemoveEdge(_pickedEdge);
					_pickedEdge = null;
				}

				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		

		public void CheckTriangulationPreConditions()
		{
			if (_polygon.GetConnectedComponentsCount() != 1)
				throw new Exception("Полигон не является связным");

			if (_polygon.GetMaxVertexDegree() > 2)
				throw new Exception("Максимальное количество ребер на вершину не должно превышать два. Удалите лишнее");
		}

		private void MainMenu_Operations_Monotone_Click(object sender, EventArgs e)
		{
			_pickedVertexIndex = -1;
			_polygon.ResetPickState();

			try
			{
				CheckTriangulationPreConditions();

				TriangulationBuilder.MonotoneCutting(_polygon);
			}
			catch (Exception error)
			{
				MessageBox.Show(error.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void MainMenu_File_Open_Click(object sender, EventArgs e)
		{
			try
			{
				if (OpenPolyDialog.ShowDialog() != DialogResult.OK)
					return;

				string fileName = OpenPolyDialog.FileName;
				CustomPolygon polygon = PolygonIO.LoadFromFile(fileName);

				ResetPolygon(polygon);
			}
			catch (Exception error)
			{
				MessageBox.Show(error.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void ResetPolygon(CustomPolygon polygon)
		{
			_scene.RemoveObject(_polygon);
			_scene.AddObject(polygon);

			_polygon = polygon;
			_pickedVertexIndex = -1;
		}

		private void MainMenu_File_Save_Click(object sender, EventArgs e)
		{
			try
			{
				if (SavePolyDialog.ShowDialog() != DialogResult.OK)
					return;

				string fileName = SavePolyDialog.FileName;
				PolygonIO.WriteToFile(fileName, _polygon);

				MessageBox.Show("Данные сохранены");
			}
			catch (Exception error)
			{
				MessageBox.Show(error.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void MainMenu_File_Exit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void MainMenu_File_Clear_Click(object sender, EventArgs e)
		{
			_polygon.Clear();
			_pickedEdge = null;
			_pickedVertexIndex = -1;
		}
	}
}
