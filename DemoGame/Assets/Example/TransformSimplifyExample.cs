using UnityEngine;

namespace QFramework
{
    public class TransformSimplifyExample
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/4.Transform API 简化", false, 5)]
#endif
        private static void MenuClicked1()
        {
            var transform = new GameObject("transform").transform;

            TransformSimplify.SetLocalPosX(transform, 5.0f);
            TransformSimplify.SetLocalPosY(transform, 5.0f);
            TransformSimplify.SetLocalPosZ(transform, 5.0f);
            TransformSimplify.Identity(transform);

            var childTrans = new GameObject("Child").transform;

            TransformSimplify.AddChild(transform,childTrans);
        }
    }
}