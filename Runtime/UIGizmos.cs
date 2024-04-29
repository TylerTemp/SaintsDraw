using UnityEngine;

namespace SaintsDraw
{
    public static class UIGizmos
    {
        private static void GetPos(RectTransform rectTransform, Camera camera, float distance, Vector3[] corners)
        {
            // Vector3[] corners = new Vector3[4];

            rectTransform.GetWorldCorners(corners);

            if (camera == null)
            {
                return;
            }

            Vector3 centerWorldPos = rectTransform.transform.position;
            Vector3 centerWorldDistancedPos = new Vector3(centerWorldPos.x, centerWorldPos.y, distance);

            Gizmos.DrawSphere(centerWorldPos, 0.5f);

            // Vector3 centerViewPos = camera.ScreenToWorldPoint(centerWorldDistancedPos);
            // using (new ColorScoop(Color.red))
            // {
            //     Gizmos.DrawSphere(centerViewPos, 0.01f);
            // }
            //
            // foreach (Vector3 cor in corners)
            // {
            //     Gizmos.DrawSphere(cor, 0.5f);
            // }

            for (int index = 0; index < corners.Length; index++)
            {
                Vector3 cornerDistancedPos = centerWorldDistancedPos + centerWorldPos - corners[index];
                Vector3 shiftedPos = camera.ScreenToWorldPoint(cornerDistancedPos);
                corners[index] = shiftedPos;
                // using (new ColorScoop(Color.red))
                // {
                //     Gizmos.DrawSphere(shiftedPos, 0.01f);
                // }
            }
        }

        public static void DrawWireRectTransform(RectTransform rectTransform, Camera camera=null, float distance=5f)
        {
#if !SAINTSDRAW_GIZMOS_DISABLE && UNITY_EDITOR
            Vector3[] corners = new Vector3[4];
            GetPos(rectTransform, camera, distance, corners);

            for (int i = 0; i < 4; i++)
            {
                Gizmos.DrawLine(corners[i], corners[(i + 1) % 4]);
            }

#endif
        }

        public static void DrawWireRectTransform(LineRenderer lineRenderer, RectTransform rectTransform, Camera camera=null, float distance=1f)
        {
            Vector3[] corners = new Vector3[5];
            GetPos(rectTransform, camera, distance, corners);

            corners[4] = corners[0];

            lineRenderer.positionCount = 5;
            lineRenderer.SetPositions(corners);
        }
    }
}
