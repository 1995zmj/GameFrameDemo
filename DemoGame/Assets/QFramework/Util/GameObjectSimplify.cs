using UnityEngine;

namespace QFramework
{
    public partial class GameObjectSimplify
    {
        public static void Show(GameObject gameObj)
        {
            gameObj.SetActive(true);
        }

        public static void Hide(GameObject gameObj)
        {
            gameObj.SetActive(false);
        }

        public static void Show(Transform transform)
        {
            transform.gameObject.SetActive(true);
        }

        public static void Hide(Transform transform)
        {
            transform.gameObject.SetActive(false);
        }
    }
}