using System;
using UnityEngine;

namespace Utils
{
    /// <summary>
    /// 数学相关工具类
    /// </summary>
    public class UtilsMath
    {
#region Line & Point

        /// <summary>
        /// 检测制定点是在直线的那个位置<BR/>
        /// 忽略高度，指判断在XZ平面上的关系
        /// </summary>
        /// <param name="iLineStartPos">直线开始点</param>
        /// <param name="iLineEndPos">直线结束点</param>
        /// <param name="iCheckPoint">检测点</param>
        /// <returns>1:左侧; 0:线上; -1:右侧;</returns>
        public static int CheckPositionWithLine(Vector3 iLineStartPos, Vector3 iLineEndPos, Vector3 iCheckPoint)
        {
            var checkLine = iCheckPoint - iLineStartPos;
            var baseline = iLineEndPos - iLineStartPos;
            // 利用叉成几何意义，检测点在指定线的左侧/右侧/线上
            var value = checkLine.x * baseline.z - checkLine.z * baseline.x;
            if (0 > value) return -1;
            return 0 < value ? 1 : 0;
        }
        
#endregion
    }
}


