// using QFramework;
//
// namespace Utils
// {
//     public class ClassExtension
//     {
//         /// <summary>
//         /// 日志输出 - 信息
//         /// </summary>
//         /// <param name="iFormat">日志格式</param>
//         /// <param name="iArgs"></param>
//         public void I(string iFormat, params object[] iArgs)
//         {
//             if (Log.Level < LogLevel.Normal)
//                 return;
//             var className = GetType().Name;
//             var log = string.Format(iFormat, iArgs);
//             Log.I($"[{className}] {log}");
//         }
//         
//         /// <summary>
//         /// 日志输出 - 警告
//         /// </summary>
//         /// <param name="iFormat">日志格式</param>
//         /// <param name="iArgs"></param>
//         public void W(string iFormat, params object[] iArgs)
//         {
//             if (Log.Level < LogLevel.Normal)
//                 return;
//             var className = GetType().Name;
//             var log = string.Format(iFormat, iArgs);
//             Log.W($"[{className}] {log}");
//         }
//         
//         /// <summary>
//         /// 日志输出 - 错误
//         /// </summary>
//         /// <param name="iFormat">日志格式</param>
//         /// <param name="iArgs"></param>
//         public void E(string iFormat, params object[] iArgs)
//         {
//             if (Log.Level < LogLevel.Normal)
//                 return;
//             var className = GetType().Name;
//             var log = string.Format(iFormat, iArgs);
//             Log.E($"[{className}] {log}");
//         }
//         
//         /// <summary>
//         /// 日志输出 - 异常
//         /// </summary>
//         /// <param name="iExp">异常</param>
//         public void E(System.Exception iExp)
//         {
//             if (Log.Level < LogLevel.Normal)
//                 return;
//             var className = GetType().Name;
//             var log = $"{iExp.GetType().Name} : {iExp.Message}\n {iExp.StackTrace}";
//             Log.E($"[{className}] {log}");
//         }
//     }
// }
