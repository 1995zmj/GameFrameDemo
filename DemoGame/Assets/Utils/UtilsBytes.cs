namespace Utils
{
	/// <summary>
	/// 位检查&校验类
	/// </summary>
	public static class UtilsBytes {
		
		/// <summary>
		/// 检测并检验指定位是否为1<BR/>
		///   备注：可以同时检测多位<BR/>
		///   如：<BR/>
		///   iDstVale ： 0x000000111<BR/>
		///   iCheckByte : 0x00000100<BR/>
		///   计算结果 : <BR/>
		///     false => iDstVale(0x000001011) + iCheckByte(0x00000100) 结果为 0x00000000 就不等于 0x00000100<BR/>
		///     true => iDstVale(0x000000111) + iCheckByte(0x00000100) 结果为 0x00000100 就等于 0x00000100
		/// </summary>
		/// <param name="iDstVale">目标值</param>
		/// <param name="iCheckByte">检测位</param>
		/// <returns>true:OK; false:NG;</returns>
		public static bool CheckByte(int iDstVale, int iCheckByte)
		{
			return (iDstVale & iCheckByte) == iCheckByte;
		}
	}
}

