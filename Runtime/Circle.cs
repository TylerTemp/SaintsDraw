using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SaintsDrawer
{
    public static class Circle
    {
        private static (Vector3 right, Vector3 forward) PrepareRightForward(Vector3 upward)
        {
            Vector3 normal = upward.normalized;
            Vector3 right = Vector3.Cross(Vector3.up, normal).normalized;

            if (right.sqrMagnitude < 0.01f)
            {
                right = Vector3.Cross(Vector3.right, normal).normalized;
            }
            Vector3 forward = Vector3.Cross(normal, right).normalized;

            return (right, forward);
        }

        private static IEnumerable<Vector3> GetOffsets(float radius, Vector3 right, Vector3 forward, IEnumerable<float> degrees)
        {
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (float degree in degrees)
            {
                float angle = degree * Mathf.Deg2Rad;
                float x = Mathf.Cos(angle) * radius;
                float z = Mathf.Sin(angle) * radius;
                yield return right * x + forward * z;
            }
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

            lineRenderer.positionCount = circleVertices.Count + 1;
            lineRenderer.SetPositions(circleVertices.Append(circleVertices[0]).ToArray());
        }

        public static void Draw(Vector3 center, float radius, Vector3 upward, float segAngle=10f)
        {
            if (segAngle <= 0)
            {
                return;
            }

            float clampAngle = Mathf.Clamp(segAngle, 0, 180);

            (Vector3 right, Vector3 forward) = PrepareRightForward(upward);
            IEnumerable<float> degrees = Arc.FloatStep(0f, 360f, clampAngle);
            Vector3[] circleVertices = GetOffsets(radius, right, forward, degrees)
                .Select(each => center + each)
                .ToArray();

            DrawGizmos(circleVertices);
        }

        public static void Draw(LineRenderer lineRenderer, Vector3 center, float radius, Vector3 upward, float segAngle=10f)
        {
            if (segAngle <= 0)
            {
                return;
            }

            float clampAngle = Mathf.Clamp(segAngle, 0, 180);

            (Vector3 right, Vector3 forward) = PrepareRightForward(upward);
            IEnumerable<float> degrees = Arc.FloatStep(0f, 360f, clampAngle);
            Vector3[] circleVertices = GetOffsets(radius, right, forward, degrees)
                .Select(each => center + each)
                .ToArray();

            DrawLineRenderer(lineRenderer, circleVertices);
        }

        public static void DrawBySegCount(Vector3 center, float radius, Vector3 upward, int numSegments=40)
        {
            if (numSegments < 2)
            {
                return;
            }

            (Vector3 right, Vector3 forward) = PrepareRightForward(upward);
            IEnumerable<float> degrees = Arc.FloatRange(0, 360, numSegments);
            Vector3[] circleVertices = GetOffsets(radius, right, forward, degrees)
                .Select(each => center + each)
                .ToArray();

            DrawGizmos(circleVertices);
        }

        public static void DrawBySegCount(LineRenderer lineRenderer, Vector3 center, float radius, Vector3 upward, int numSegments=40)
        {
            if (numSegments < 2)
            {
                return;
            }

            (Vector3 right, Vector3 forward) = PrepareRightForward(upward);
            IEnumerable<float> degrees = Arc.FloatRange(0, 360, numSegments);
            Vector3[] circleVertices = GetOffsets(radius, right, forward, degrees)
                .Select(each => center + each)
                .ToArray();

            DrawLineRenderer(lineRenderer, circleVertices);
        }
    }
}
