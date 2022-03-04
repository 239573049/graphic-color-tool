using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using XHHelper.Model;
using XHHelper.XHExceptions;

namespace XHHelper
{
    /// <summary>
    /// 图色工具
    /// </summary>
    public class RecognitionColorHelper
    {
        /// <summary>
        /// 图色识别
        /// 切图坐标(0,0,0,0)时不切图
        /// </summary>
        /// <param name="bitmap">图片源</param>
        /// <param name="x1">左上x</param>
        /// <param name="y1">左上y</param>
        /// <param name="x2">左下x</param>
        /// <param name="y2">左下y</param>
        /// <param name="anchorPoint">第一个颜色点（定位点）</param>
        /// <param name="deviascope">偏移值最大100</param>
        /// <param name="colors">大概位置定位</param>
        /// <exception cref="NullReferenceException">未找到定位点图色|未找到图色定位</exception>
       [DebuggerStepThrough]
        public static ColorSize ColorDiscern(ref Bitmap bitmap, short x1, short y1, short x2, short y2, Color anchorPoint, byte deviascope = 80, params ColorsModel[] colors)
        {
            if (deviascope > 100) deviascope = 100;
            if (deviascope < 1) deviascope = 1;
            if (colors.Length == 0) throw new ArgumentNullException("colours长度不能是0");
            if (x1 > 0 && y1 > 0 && x2 > 0 && y2 > 0)
            {
                bitmap = CaptureImage(bitmap, x1, y1, (short)(x2 - x1), (short)(y2 - y1));
            }
            var positioning = new ColorSize();//定位点
            for (int i = 0; i < bitmap.Width; i++)
            {
                short len = 0;//当前查找位置
                var positionings = GetColourSize(bitmap, anchorPoint, positioning.X, positioning.Y);
                positioning = positionings;
                if (positioning.IsNull) throw new NullColorException("未找到定位点图色");
                foreach (var d in colors)
                {
                    var y = positioning.Y + d.Y;
                    var x = positioning.X + d.X;
                    if (bitmap.GetPixel((x<1?0: x), (y<1?0: y)) == d.Colors)//坐标不能小于0
                    {
                        len++;
                    }
                    if (StringHelper.CalculatePercentage(len, colors.Length) >= deviascope)
                    {
                        positioning.X += x1;
                        positioning.Y += y1;
                        return positioning;
                    }
                }
                if (StringHelper.CalculatePercentage(len, colors.Length) >= deviascope)
                {
                    positioning.X += x1;
                    positioning.Y += y1;
                    return positioning;
                }
            }
            throw new NullColorException("未找到图色定位");
        }

        /// <summary>
        /// 图色识别
        /// 切图坐标(0,0,0,0)时不切图
        /// </summary>
        /// <param name="bitmap">图片源</param>
        /// <param name="x1">左上x</param>
        /// <param name="y1">左上y</param>
        /// <param name="x2">左下x</param>
        /// <param name="y2">左下y</param>
        /// <param name="anchorPoint">第一个颜色点（定位点）</param>
        /// <param name="deviascope">偏移值最大100</param>
        /// <param name="colors">大概位置定位</param>
        /// <exception cref="NullReferenceException">未找到定位点图色|未找到图色定位</exception>
        public static ColorSize ColorDiscern(ref Bitmap bitmap, short x1, short y1, short x2, short y2, string anchorPoint, byte deviascope = 80, params ColorsModel[] colors)
        {
            return ColorDiscern(ref bitmap, x1, y1, x2, y2, anchorPoint.StartsWith("#")?ColorTranslator.FromHtml(anchorPoint): ColorTranslator.FromHtml("#"+anchorPoint), deviascope,colors);
        }
        /// <summary>
        /// 图色识别
        /// 切图坐标(0,0,0,0)时不切图
        /// </summary>
        /// <param name="bitmap">图片源</param>
        /// <param name="x1">左上x</param>
        /// <param name="y1">左上y</param>
        /// <param name="x2">左下x</param>
        /// <param name="y2">左下y</param>
        /// <param name="anchorPoint">第一个颜色点（定位点）</param>
        /// <param name="deviascope">偏移值最大100</param>
        /// <param name="colors">大概位置定位</param>
        public static async Task<ColorSize> ColorDiscernAsync(Bitmap bitmap, short x1, short y1, short x2, short y2, Color anchorPoint, byte deviascope = 80, params ColorsModel[] colors)
        {
            return await Task.Run(() => ColorDiscern(ref bitmap, x1, y1, x2, y2, anchorPoint, deviascope, colors));
        }
        /// <summary>
        /// 图色识别
        /// 切图坐标(0,0,0,0)时不切图
        /// </summary>
        /// <param name="bitmap">图片源</param>
        /// <param name="x1">左上x</param>
        /// <param name="y1">左上y</param>
        /// <param name="x2">左下x</param>
        /// <param name="y2">左下y</param>
        /// <param name="anchorPoint">第一个颜色点（定位点）</param>
        /// <param name="deviascope">偏移值最大100</param>
        /// <param name="colors">大概位置定位</param>
        /// <returns></returns>
        /// <exception cref="FigureColorProcessingException"></exception>
        public static ColorSize ColorDiscern(ref Bitmap bitmap, short x1, short y1, short x2, short y2,string anchorPoint,string colors,byte deviascope = 80)
        {
            var anchorPointColor = ColorTranslator.FromHtml(anchorPoint.StartsWith("#")? anchorPoint : "#" +anchorPoint);
            var colorDataModel = colors.Split(",");
            var colorsModel = new ColorsModel[colorDataModel.Length];
            try
            {
                for (int i = 0; i < colorDataModel.Length; i++)
                {
                    var colorData = colorDataModel[i].Split("|");
                    colorsModel[i] = new ColorsModel
                    {
                        X = Convert.ToInt16(colorData[0]),
                        Y = Convert.ToInt16(colorData[1]),
                        Colors = ColorTranslator.FromHtml(colorData[2].StartsWith("#") ? colorData[2]:"#" + colorData[2])
                    };
                }
            }
            catch (Exception e)
            {
                throw new FigureColorProcessingException("图色处理异常，请检查图色数据",e);
            }
            return ColorDiscern(ref bitmap, x1, y1, x2, y2, anchorPointColor, deviascope, colorsModel);
        }
        /// <summary>
        /// 判断颜色是否一致
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="colorSize">定位点坐标</param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static bool Positioning(ref Bitmap bitmap, ColorSize colorSize, ColorsModel color) =>
            bitmap.GetPixel(colorSize.X + color.X, colorSize.Y + color.Y) == color.Colors;

        /// <summary>
        /// 从大图中截取一部分图片
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="x1">左上x</param>
        /// <param name="y1">左上y</param>
        /// <param name="x2">左下x</param>
        /// <param name="y2">左下y</param>
        public static Bitmap CaptureImage(Bitmap bitmap, short x1, short y1, short x2, short y2)
        {
            Rectangle cropRegion = new Rectangle(x1, y1, x2, y2);
            Bitmap result = new Bitmap(cropRegion.Width, cropRegion.Height);
            Graphics.FromImage(result).DrawImage(bitmap, new Rectangle(0, 0, cropRegion.Width, cropRegion.Height), cropRegion, GraphicsUnit.Pixel);
            return result;
        }
        /// <summary>
        /// 获取定位点
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="colors">定位图色</param>
        /// <param name="x">偏移值x</param>
        /// <param name="y">偏移值y</param>
        /// <returns></returns>
        public static ColorSize GetColourSize(Bitmap bitmap, Color colors, short x = 0, short y = 0)
        {
            if (y != 0) y += 1;
            for (short bx = x; bx < bitmap.Width; bx++)
            {
                for (short by = y; by < bitmap.Height; by++)
                {
                    if (bitmap.GetPixel(bx, by).Equals(colors))
                    {
                        return new ColorSize(bx, by);
                    }
                }
                y = 0;
            }
            return new ColorSize();
        }
    }
}
