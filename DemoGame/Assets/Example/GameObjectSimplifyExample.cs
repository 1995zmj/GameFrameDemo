using UnityEngine;

namespace QFramework
{
    public class GameObjectSimplifyExample
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("QFramework/Example/6.GameObejct API 简化", false, 7)]
#endif
        private static void MenuClicked()
        {
            var gameObject = new GameObject();

            GameObjectSimplify.Hide(gameObject);
            GameObjectSimplify.Hide(gameObject.transform);
        }
    }
}