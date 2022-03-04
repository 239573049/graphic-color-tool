using System;
using System.Diagnostics;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XHHelper;
using XHHelper.Model;

namespace TestProject1.Tests
{
    [TestClass()]
    public class UnitTest1Tests
    {
        [TestMethod()]
        public void Test1Test()
        {
            var colors = new ColorsModel[]
            {
                 new ColorsModel { Colors = ColorTranslator.FromHtml("#00aeff"), X =7, Y = -5 },
                 new ColorsModel { Colors = ColorTranslator.FromHtml("#00aaff"), X = 10, Y = -15 },
                 new ColorsModel { Colors = ColorTranslator.FromHtml("#00aeff"), X =-8, Y =-20 },
                 new ColorsModel { Colors = ColorTranslator.FromHtml("#00aeff"), X =-14, Y = -10 },
                 new ColorsModel { Colors = ColorTranslator.FromHtml("#ffffff"), X =-13, Y = -3},
                 new ColorsModel { Colors = ColorTranslator.FromHtml("#ffffff"), X =-10, Y =2 },
                 new ColorsModel { Colors = ColorTranslator.FromHtml("#ffffff"), X =-6, Y = -9 },
            };
            ///找Docker图标
            while(true)
            {
                try
                {
                    var data = GetScreenCapture();//截图
                    var sw = Stopwatch.StartNew();
                    var color = RecognitionColorHelper.ColourDiscern(ref data, 0, 0, 0, 0, ColorTranslator.FromHtml("#00aeff"), 60, colors);
                    sw.Stop();
                    Debug.WriteLine($"坐标X:{color.X},坐标Y:{color.Y}  耗时：{sw.Elapsed.TotalMilliseconds}ms");
                    Debug.Close();
                }
                catch (Exception)
                {
                }
            }
        }
        private Bitmap GetScreenCapture()
        {
            Rectangle tScreenRect = new Rectangle(0, 0, 1920, 1080);
            Bitmap tSrcBmp = new Bitmap(tScreenRect.Width, tScreenRect.Height); // 用于屏幕原始图片保存
            Graphics gp = Graphics.FromImage(tSrcBmp);
            gp.CopyFromScreen(0, 0, 0, 0, tScreenRect.Size);
            gp.DrawImage(tSrcBmp, 0, 0, tScreenRect, GraphicsUnit.Pixel);
            return tSrcBmp;
        }
    }
}