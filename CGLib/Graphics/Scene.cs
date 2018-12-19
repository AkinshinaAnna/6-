using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBitmap = System.Drawing.Bitmap;
using CGraphics = System.Drawing.Graphics;
using CRect = System.Drawing.Rectangle;
using CColor = System.Drawing.Color;

namespace CGLib.Graphics
{
	public class Scene
	{
		public Camera Camera { get; set; }

		protected List<IDrawable> _sceneObjects = new List<IDrawable>();

		public Scene(int width, int height)
		{
			float aspectRatio = (float)width / height;

			float sizeX = 10.0f;
			float sizeY = sizeX / aspectRatio;

			Camera = new Camera(new Math.Vector2(width, height), new Math.Vector2(-sizeX/2, sizeY / 2), new Math.Vector2(sizeX, sizeY));
		}

		public void AddObject(IDrawable obj)
		{
			_sceneObjects.Add(obj);
		}

		public void RemoveObject(IDrawable obj)
		{
			_sceneObjects.Remove(obj);
		}

		public void Update() {

		}

		public void Draw(CGraphics g, CRect clipArea)
		{
			g.Clear(CColor.White);

			DrawState drawState = new DrawState()
			{
				Graphics = g,
				Camera = this.Camera
			};

			foreach (IDrawable sceneObject in _sceneObjects)
			{
				sceneObject.Draw(drawState);
			}
		}
	}
}
