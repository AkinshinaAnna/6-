using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGLib.Math;

namespace CGLib.Graphics
{
	public class Camera
	{
		private Vector2 _position;
		private Vector2 _size;

		private Vector2 _zoom;

		private Vector2 _screenSize;

		Matrix3 _localToScreenTransform;
		Matrix3 _screenToLocalTransform;

		public Vector2 Position
		{
			get
			{
				return _position;
			}
		}

		public Vector2 Size
		{
			get
			{
				return _size;
			}
		}

		public Vector2 Zoom
		{
			get
			{
				return _zoom;
			}
		}

		public Camera(Vector2 screenSize, Vector2 position, Vector2 size)
		{
			SetVisibleArea(position, size);
			SetScreenSize(screenSize);
		}

		public void SetScreenSize(Vector2 screenSize)
		{
			_screenSize = screenSize;
			UpdateZoom();
		}

		public void SetVisibleArea(Vector2 position, Vector2 size)
		{
			_position = position;
			_size = size;
		}

		public void Move(Vector2 movement)
		{
			_position += movement;
			UpdateMatrices();
		}

		public void SetPosition(Vector2 position)
		{
			_position = position;
			UpdateMatrices();
		}

		public void SetSize(Vector2 size)
		{
			SetVisibleArea(_position, size);
			UpdateZoom();
		}

		public void UpdateZoom(float factor)
		{
			_size *= factor;
			UpdateZoom();
		}

		public Vector2 LocalToScreen(Vector2 point)
		{
			Vector3 transformedPoint = _localToScreenTransform * new Vector3(point, 1.0f);
			return new Vector2(transformedPoint.X, transformedPoint.Y);
		}

		public Vector2 ScreenToLocal(Vector2 point)
		{
			Vector3 transformedPoint = _screenToLocalTransform * new Vector3(point, 1.0f);
			return new Vector2(transformedPoint.X, transformedPoint.Y);
		}

		private void UpdateZoom()
		{
			_zoom = new Vector2(System.Math.Abs(_screenSize.X / _size.X), System.Math.Abs(_screenSize.Y / _size.Y));
			UpdateMatrices();
		}

		private void UpdateMatrices()
		{
			_localToScreenTransform = Matrix3.GetScale(_zoom.X, -_zoom.Y) * Matrix3.GetTranslation(-_position.X, -_position.Y);
			_screenToLocalTransform = _localToScreenTransform.Inverse();
		}
	}
}
