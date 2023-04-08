using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;

namespace WebApp.App_Helpers
{
  public class VerifyCodeHelper
  {
    public static Bitmap CreateVerifyCode(out string code)
    {
      //建立Bitmap对象，绘图
      Bitmap bitmap = new Bitmap(200, 60);
      Graphics graph = Graphics.FromImage(bitmap);
      graph.FillRectangle(new SolidBrush(Color.White), 0, 0, 200, 60);
      Font font = new Font(FontFamily.GenericSerif, 48, FontStyle.Bold, GraphicsUnit.Pixel);
      Random r = new Random();
      string letters = "ABCDEFGHIJKLMNPQRSTUVWXYZ0123456789";

      StringBuilder sb = new StringBuilder();

      //添加随机的五个字母
      for (int x = 0; x < 5; x++)
      {
        string letter = letters.Substring(r.Next(0, letters.Length - 1), 1);
        sb.Append(letter);
        graph.DrawString(letter, font, new SolidBrush(Color.Black), x * 38, r.Next(0, 15));
      }
      code = sb.ToString();

      //混淆背景
      Pen linePen = new Pen(new SolidBrush(Color.Black), 2);
      for (int x = 0; x < 6; x++)
        graph.DrawLine(linePen, new Point(r.Next(0, 199), r.Next(0, 59)), new Point(r.Next(0, 199), r.Next(0, 59)));
      return bitmap;
    }
  }
}