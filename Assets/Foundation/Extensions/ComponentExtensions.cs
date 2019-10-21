using UnityEngine;

namespace Assets.Foundation.Extensions
{
    public static class ComponentExtensions
    {
        public static T CreateComponent<T>(this Component t) where T : Component
        {
            if (t == null)
            {
                return null;
            }
            var c = t.GetComponent<T>();
            if (c == null)
            {
                c = t.gameObject.AddComponent<T>();
            }

            return c;
        }

        public static void DestoryComponent<T>(this Component t) where T : Component
        {
            if (t == null)
            {
                return;
            }
            var c = t.GetComponent<T>();
            if (c == null)
            {
                return;
            }

            Component.DestroyImmediate(c);
        }
    }
}
