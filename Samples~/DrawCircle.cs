using SaintsDrawer;
using UnityEditor;
using UnityEngine;

namespace SaintsDraw.Samples
{
    public class DrawCircle : MonoBehaviour
    {
        [SerializeField] private Vector3 _upward;

        [SerializeField] private float _circleRadis;

        [SerializeField] private bool _useSegCount;
        [SerializeField] private int _segCount;
        [SerializeField] private float _segAngle;

        [SerializeField] private bool _useLocalPosition;

        [SerializeField] private bool _useLineRenderer;
        [SerializeField] private LineRenderer _lineCircle;
        // [SerializeField] private LineRenderer _lineArc;

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            // var oldMatrix = Gizmos.matrix;
            // this wont work for line renderer. You need to manually calculate position for lineRenderer because it'll be affected by it's parent transform.
            if(_useLocalPosition)
            {
                Gizmos.matrix = Handles.matrix = transform.localToWorldMatrix;
            }

            using(new ColorScoop(Color.green))
            {
                Arrow.Draw(Vector3.zero, _upward.normalized * 6f);
                Handles.Label(_upward.normalized * 6f, "[up]");
            }

            if(_useSegCount)
            {
                if (_useLineRenderer)
                {
                    Circle.DrawBySegCount(_lineCircle, Vector3.zero, _circleRadis, _upward, _segCount);
                }
                else
                {
                    _lineCircle.positionCount = 0;
                    Circle.DrawBySegCount(Vector3.zero, _circleRadis, _upward, _segCount);
                }
            }
            else
            {
                if (_useLineRenderer)
                {

                    Circle.Draw(_lineCircle, Vector3.zero, _circleRadis, _upward, _segAngle);
                }
                else
                {
                    _lineCircle.positionCount = 0;
                    Circle.Draw(Vector3.zero, _circleRadis, _upward, _segAngle);
                }
            }
        }
#endif
    }
}
