using System.Linq;
using UnityEngine;

namespace SaintsDraw
{
    public static class UIGizmos
    {
        public static void DrawWireRectTransform(RectTransform rectTransform)
        {
#if !SAINTSDRAW_GIZMOS_DISABLE && UNITY_EDITOR
            Vector3[] corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);

            for (int i = 0; i < 4; i++)
            {
                Gizmos.DrawLine(corners[i], corners[(i + 1) % 4]);
            }
#endif
        }

        public static void DrawRectTransform(RectTransform rectTransform)
        {
#if !SAINTSDRAW_GIZMOS_DISABLE && UNITY_EDITOR
            Vector3[] corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);

            for (int i = 0; i < 4; i++)
            {
                Gizmos.DrawLine(corners[i], corners[(i + 1) % 4]);
            }
#endif
        }

        public static void DrawWireRectTransform(LineRenderer lineRenderer, RectTransform rectTransform)
        {
            Vector3[] corners = new Vector3[5];
            rectTransform.GetWorldCorners(corners);
            corners[4] = corners[0];

            lineRenderer.positionCount = 5;
            lineRenderer.SetPositions(corners);
        }
    }
}
