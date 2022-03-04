﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject1.Tests
{
    [TestClass()]
    public class UnitTest1Tests
    {
        [TestMethod()]
        public void Test1Test()
        {

<<<<<<< HEAD
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
            for (int i = 0; i < 20; i++)
            {
                var data = GetScreenCapture();//截图
                var sw = Stopwatch.StartNew();
                var color = RecognitionColorHelper.ColourDiscern(ref data, 0, 0, 0, 0, ColorTranslator.FromHtml("#00aeff"), 60, colors);
                sw.Stop();
                Debug.WriteLine($"图色次数{i}; 坐标X:{color.X},坐标Y:{color.Y}  耗时：{sw.Elapsed.TotalMilliseconds}ms");
            }
=======
>>>>>>> 74064d412ce6e6ebfe66d2a3c2213e7b33ca7ba7
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