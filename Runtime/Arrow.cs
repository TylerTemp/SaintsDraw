using UnityEngine;

namespace SaintsDraw
{
    public static class Arrow
    {
        public static void Draw(LineRenderer lineRenderer, Vector3 from, Vector3 to, float arrowHeadLength = 0.5f, float arrowHeadAngle = 20.0f, Vector3? up = null)
        {
            DrawLineRenderer(lineRenderer, GetPoints(from, to, arrowHeadLength, arrowHeadAngle, up));
        }

        public static void Draw(Vector3 from, Vector3 to, float arrowHeadLength = 0.5f, float arrowHeadAngle = 20.0f, Vector3? up = null)
        {
            DrawGizmos(GetPoints(from, to, arrowHeadLength, arrowHeadAngle, up));
        }

        private static (Vector3 tail, Vector3 head, Vector3 arrowheadLeft, Vector3 arrowheadRight) GetPoints(Vector3 from, Vector3 to, float arrowHeadLength = 0.5f, float arrowHeadAngle = 20.0f, Vector3? up = null)
        {
            // Debug.Assert(arrowHeadLength>0);
            // Debug.Assert(arrowHeadAngle>0);

            Vector3 upward = up ?? Vector3.forward;

            Vector3 arrowDirection = (to - from).normalized;

            // Calculate the rotation to make the arrow perpendicular to the "up" vector
            Quaternion rotation = Quaternion.LookRotation(arrowDirection, upward);

            // Draw the arrow body using Gizmos
            // Gizmos.color = Color.red; // You can change the arrow's color as needed.
            // Gizmos.DrawRay(from, to - from);

            // Draw arrowhead lines
            // float arrowheadLength = 0.2f;
            // Vector3 arrowheadTip = to;
            // Vector3 arrowheadLeft = to - rotation * new Vector3(-arrowHeadLength, 0, arrowHeadLength);
            // Vector3 arrowheadRight = to - rotation * new Vector3(arrowHeadLength, 0, arrowHeadLength);

            Vector3 arrowheadLeft = to - rotation * new Vector3(
                -arrowHeadLength * Mathf.Sin(Mathf.Deg2Rad * arrowHeadAngle),
                0,
                arrowHeadLength * Mathf.Cos(Mathf.Deg2Rad * arrowHeadAngle)
            );
            Vector3 arrowheadRight = to - rotation * new Vector3(
                arrowHeadLength * Mathf.Sin(Mathf.Deg2Rad * arrowHeadAngle),
                0,
                arrowHeadLength * Mathf.Cos(Mathf.Deg2Rad * arrowHeadAngle)
            );

            // Gizmos.DrawLine(arrowheadTip, arrowheadLeft);
            // Gizmos.DrawLine(arrowheadTip, arrowheadRight);

            return (
                from,
                to,
                arrowheadLeft,
                arrowheadRight
            );
        }

        private static void DrawGizmos((Vector3 tail, Vector3 head, Vector3 arrowheadLeft, Vector3 arrowheadRight) points)
        {
            Gizmos.DrawLine(points.tail, points.head);
            Gizmos.DrawLine(points.head, points.arrowheadLeft);
            Gizmos.DrawLine(points.head, points.arrowheadRight);
        }

        private static void DrawLineRenderer(LineRenderer lineRenderer, (Vector3 tail, Vector3 head, Vector3 arrowheadLeft, Vector3 arrowheadRight) points)
        {
            (Vector3 tail, Vector3 head, Vector3 arrowheadLeft, Vector3 arrowheadRight) = points;

            lineRenderer.positionCount = 5;
            lineRenderer.SetPositions(new[]
            {
                arrowheadLeft,
                head,
                tail,
                head,
                arrowheadRight,
            });
        }
    }
}
