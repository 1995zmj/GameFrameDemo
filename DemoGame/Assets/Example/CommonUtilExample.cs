using UnityEngine;

namespace QFramework
{
    public class CommonUtilExample : MonoBehaviour
    {

#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/1.复制文本到剪切板", false, 2)]
#endif
        private static void MenuClicked2()
        {
            CommonUtil.CopyText("要复制的关键字");
        }
    }
}