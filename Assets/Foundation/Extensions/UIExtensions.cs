using Assets.Foundation.DataAccess;
using Assets.Foundation.Managers;
using Assets.Foundation.Tool;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Foundation.Extensions
{
    /// <summary>
    /// ui扩展功能
    /// </summary>
    public static class UIExtensions
    {
        public static float GetWidth(this RectTransform t)
        {
            return t.rect.width;
        }

        public static void SetWidth(this RectTransform t, float w)
        {
            if (t.parent == null)
            {
                return;
            }


            float diffW = w - t.rect.width;

            Vector2 offsetMin = t.offsetMin;
            offsetMin.x -= diffW * t.pivot.x;
            Vector2 offsetMax = t.offsetMax;
            offsetMax.x += diffW * (1 - t.pivot.x);

            t.offsetMin = offsetMin;
            t.offsetMax = offsetMax;
        }

        public static void SetMiddleWidth(this RectTransform t, float w)
        {
            if (t.parent == null)
            {
                return;
            }
            var parentRect = t.parent.GetComponent<RectTransform>();

            Vector2 offsetMin = t.offsetMin;
            offsetMin.x = (parentRect.rect.width - w) * 0.5f;
            Vector2 offsetMax = t.offsetMax;
            offsetMax.x = (w - parentRect.rect.width) * 0.5f;

            t.offsetMin = offsetMin;
            t.offsetMax = offsetMax;
        }

        public static float GetHeight(this RectTransform t)
        {
            return t.rect.height;
        }

        public static void SetHeight(this RectTransform t, float h)
        {
            if (t.parent == null)
            {
                return;
            }

            float diffH = h - t.rect.height;

            Vector2 offsetMin = t.offsetMin;
            offsetMin.y -= diffH * t.pivot.y;
            Vector2 offsetMax = t.offsetMax;
            offsetMax.y += diffH * (1 - t.pivot.y);

            t.offsetMin = offsetMin;
            t.offsetMax = offsetMax;
        }

        public static void SetCenterHeight(this RectTransform t, float h)
        {
            if (t.parent == null)
            {
                return;
            }
            var parentRect = t.parent.GetComponent<RectTransform>();

            Vector2 offsetMin = t.offsetMin;
            offsetMin.y = (parentRect.rect.height - h) * 0.5f;
            Vector2 offsetMax = t.offsetMax;
            offsetMax.y = (h - parentRect.rect.height) * 0.5f;

            t.offsetMin = offsetMin;
            t.offsetMax = offsetMax;
        }
    }
}
