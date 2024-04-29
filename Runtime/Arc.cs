using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace SaintsDraw
{
    public static class Arc
    {
        public static (float fromAngle, float toAngle) NormalAngleRange(float fromAngle, float toAngle)
        {
            if (Mathf.Abs(toAngle - fromAngle) >= 360)
            {
                return (0, 360);
            }

            float min = Mathf.Min(fromAngle, toAngle);
            float max = Mathf.Max(fromAngle, toAngle);

            float minMod = min % 360;
            float maxMod = max % 360;
            if (maxMod < minMod)
            {
                maxMod += 360f;
            }

            return (minMod, maxMod);
        }

        public static Vector3 GetDirection(Vector3 upward, Vector3 plate, float toArc)
        {
            Vector3 side = Vector3.Cross(upward, plate).normalized;
            Vector3 forward = Vector3.Cross(side, upward).normalized;
            float deg = toArc * Mathf.Deg2Rad;
            return forward * Mathf.Cos(deg) + side * Mathf.Sin(deg);
        }

        private static Vector3 GetPoint(Vector3 center, float radius, float angle, Vector3 side, Vector3 forward)
        {
            float deg = angle * Mathf.Deg2Rad;
            return center + forward * radius * Mathf.Cos(deg) + side * radius * Mathf.Sin(deg);
        }

        public static void DrawBySegCount(Vector3 center, float radius, float fromArc, float toArc, Vector3 upward, Vector3 plate, int numSegments=40)
        {
#if !SAINTSDRAW_GIZMOS_DISABLE && UNITY_EDITOR
            DrawGizmos(GetDrawPoints(center, radius, upward, plate, FloatRange(fromArc, toArc, numSegments)));
#endif
        }
        public static void DrawBySegCount(LineRenderer lineRenderer, Vector3 center, float radius, float fromArc, float toArc, Vector3 upward, Vector3 plate, int numSegments=40)
        {
            DrawLineRenderer(lineRenderer, GetDrawPoints(center, radius, upward, plate, FloatRange(fromArc, toArc, numSegments)));
        }

        public static void Draw(Vector3 center, float radius, float fromArc, float toArc, Vector3 upward, Vector3 plate, float segAngle=10f)
        {
#if !SAINTSDRAW_GIZMOS_DISABLE && UNITY_EDITOR
            DrawGizmos(GetDrawPoints(center, radius, upward, plate, FloatStep(fromArc, toArc, segAngle)));
#endif
        }

        public static void Draw(LineRenderer lineRenderer, Vector3 center, float radius, float fromArc, float toArc, Vector3 upward, Vector3 plate, float segAngle=10f)
        {
            DrawLineRenderer(lineRenderer, GetDrawPoints(center, radius, upward, plate, FloatStep(fromArc, toArc, segAngle)));
        }

        private static IReadOnlyList<Vector3> GetDrawPoints(Vector3 center, float radius, Vector3 upward, Vector3 plate, IEnumerable<float> angles)
        {
            Vector3 side = Vector3.Cross(upward, plate).normalized;
            Vector3 forward = Vector3.Cross(side, upward).normalized;

            return angles
                .Select(each => GetPoint(center, radius, each, side, forward))
                .ToArray();
            // if(circleVertices.Length < 2)
            // {
            //     return Array.Empty<Vector3>();
            // }
            //
            // return circleVertices;
            //
            // for (int index = 0; index < circleVertices.Length - 1; index++)
            // {
            //     Vector3 curPoint = circleVertices[index];
            //     Vector3 nextPoint = circleVertices[index + 1];
            //     Gizmos.DrawLine(curPoint, nextPoint);
            // }
        }

        private static void DrawGizmos(IReadOnlyList<Vector3> circleVertices)
        {
            if (circleVertices.Count < 2)
            {
                return;
            }
            for (int index = 0; index < circleVertices.Count - 1; index++)
            {
                Vector3 curPoint = circleVertices[index];
                Vector3 nextPoint = circleVertices[index + 1];
                Gizmos.DrawLine(curPoint, nextPoint);
            }
        }

        private static void DrawLineRenderer(LineRenderer lineRenderer, IReadOnlyList<Vector3> circleVertices)
        {
            if (circleVertices.Count < 2)
            {
                return;
            }

            lineRenderer.positionCount = circleVertices.Count;
            lineRenderer.SetPositions(circleVertices.ToArray());
        }


        public static IEnumerable<float> FloatRange(float min, float max, int sep)
        {
            float step = (max - min) / sep;

            foreach (int curSep in Enumerable.Range(0, sep + 1))
            {
                yield return min + step * curSep;
            }
        }

        public static IEnumerable<float> FloatStep(float min, float max, float step)
        {
            float curValue = min;
            while (curValue <= max)
            {
                yield return curValue;
                curValue += step;
            }

            if(!Mathf.Approximately(curValue, max))
            {
                yield return max;
            }
        }
    }
}
