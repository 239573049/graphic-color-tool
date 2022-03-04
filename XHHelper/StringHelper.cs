namespace XHHelper
{
    public class StringHelper
    {
        /// <summary>
        /// 计算百分比向前进俩位
        /// </summary>
        /// <param name="len">数量</param>
        /// <param name="count">总数</param>
        /// <returns></returns>
        public static int CalculatePercentage(int len,int count)
        {
            return (int)((decimal)len / count) * 100;
        }
    }
}
