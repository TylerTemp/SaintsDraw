using UnityEngine;

namespace SaintsDrawer
{
    public static class Circle
    {
        public static void Draw(Vector3 center, float radius, Vector3 upward, Vector3 plate, float segAngle=10f) => Arc.Draw(center, radius, 0, 360, upward, plate, segAngle);
        public static void Draw(LineRenderer lineRenderer, Vector3 center, float radius, Vector3 upward, Vector3 plate, float segAngle=10f) => Arc.Draw(lineRenderer, center, radius, 0, 360, upward, plate, segAngle);

        public static void DrawBySegCount(Vector3 center, float radius, Vector3 upward, Vector3 plate, int numSegments=40) => Arc.DrawBySegCount(center, radius, 0, 360, upward, plate, numSegments);
        public static void DrawBySegCount(LineRenderer lineRenderer, Vector3 center, float radius, Vector3 upward, Vector3 plate, int numSegments=40) => Arc.DrawBySegCount(lineRenderer, center, radius, 0, 360, upward, plate, numSegments);
    }
}
