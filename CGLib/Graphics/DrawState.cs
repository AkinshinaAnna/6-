using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGraphics = System.Drawing.Graphics;
using CBitmap = System.Drawing.Bitmap;

namespace CGLib.Graphics
{
	public struct DrawState
	{
		public CGraphics Graphics { get; set; }
		public Camera Camera { get; set; }

		public DrawState(CGraphics graphics, Camera camera)
		{
			Graphics = graphics;
			Camera = camera;
		}
	}
}
