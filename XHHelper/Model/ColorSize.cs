namespace XHHelper.Model
{
    public class ColorSize
    {
        public short X { get; set; }
        public short Y { get; set; }
        /// <summary>
        /// 数据是否为空
        /// </summary>
        public bool IsNull { get; set; } = true;
        public ColorSize() =>
            IsNull = true;
        public ColorSize(short x, short y)
        {
            X = x;
            Y = y;
            IsNull = false;
        }
        public ColorSize(ColorSize size)
        {
            X = size.X;
            Y = size.Y;
            IsNull = false;
        }
    }
}
