using System;
using System.Diagnostics;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XHHelper;

namespace TestProject1.Tests
{
    [TestClass()]
    public class UnitTest1Tests
    {
        [TestMethod()]
        public void Test1Test()
        {
            ///找Docker图标
            while(true)
            {
                try
                {
                    var data = GetScreenCapture();//截图
                    var sw = Stopwatch.StartNew();
                    var color = RecognitionColorHelper
                        .ColorDiscern(ref data, 0, 0, 0, 0, "f4c51f",
                        "6|-3|f4c51f,11|3|131212,11|9|594916,15|9|a7881a,14|4|aa8a1a,14|-3|f4c51f,18|-2|f4c51f,19|6|131212,12|17|131212", 80);
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