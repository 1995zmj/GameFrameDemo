using UnityEngine;

namespace QFramework
{
    public partial class MathUtil
    {
        /// <summary>
        /// 输入百分比返回是否命中概率
        /// </summary>
        public static bool Percent(int percent)
        {
            return Random.Range (0, 100) <= percent;
        }

        public static T GetRandomValueFrom<T>(params T[] values)
        {
            return values[Random.Range(0, values.Length)];
        }
    }
}