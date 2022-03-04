using System.Drawing;

namespace XHHelper.Model
{
    public class ColorsModel
    {
        public ColorsModel()
        {
        }

        public ColorsModel(Color colors, short x, short y)
        {
            Colors = colors;
            X = x;
            Y = y;
        }

        /// <summary>
        /// 颜色点
        /// </summary>
        public Color Colors { get; set; }
        /// <summary>
        /// x
        /// </summary>
        public short X { get; set; }
        /// <summary>
        /// y
        /// </summary>
        public short Y { get; set; }

    }
}
