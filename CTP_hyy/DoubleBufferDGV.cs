using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CTP_交易
{
	class DoubleBufferDGV:DataGridView
	{
		public DoubleBufferDGV()
		{
			SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
			UpdateStyles(); 
			//this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
		}
	}
}
