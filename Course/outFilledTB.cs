using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Course
{
  public static class outFilledTB
  {
    public static TextBox GetInstance(string text, bool readOnly, int color)
    {
      TextBox tb = new TextBox() { Dock = DockStyle.Fill };
      var margin = tb.Margin;
      margin.Left = 0;
      margin.Right = 0;
      margin.Top = 0;
      margin.Bottom = 0;
      tb.Margin = margin;
      tb.ReadOnly = readOnly;
      tb.Font = new System.Drawing.Font("Microsoft Sans Serif", 11);
      tb.Text = text;
      if (color == 0)
        tb.BackColor = System.Drawing.Color.FromArgb(209, 238, 252);
      else
        tb.BackColor = System.Drawing.Color.FromArgb(255, 252, 214);
      return tb;
    }
  }
}
