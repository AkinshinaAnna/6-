using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGLib.Graphics
{
	public interface IDrawable
	{
		void Draw(DrawState state);
	}
}
