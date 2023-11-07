using SaintsDraw;
using UnityEditor;
using UnityEngine;

namespace SaintsDraw.Samples
{
    public class DrawCircleArc : MonoBehaviour
    {
        [SerializeField] private Vector3 _upward;
        [SerializeField] private Vector3 _plate;

        [SerializeField] private float _arcRadis;
        [SerializeField] private float _circleRadis;
        [SerializeField] private float _fromArc;
        [SerializeField] private float _toArc;

        [SerializeField] private bool _useSegCount;
        [SerializeField] private int _segCount;
        [SerializeField] private float _segAngle;

        [SerializeField] private bool _normalAngle;

        [SerializeField] private bool _useLocalPosition;

        [SerializeField] private bool _useLineRenderer;
        [SerializeField] private LineRenderer _lineCircle;
        [SerializeField] private LineRenderer _lineArc;

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

            float formArc = _fromArc;
            float toArc = _toArc;
            if(_normalAngle)
            {
                (float normFromArc, float normToArc) = Arc.NormalAngleRange(_fromArc, _toArc);
                formArc = normFromArc;
                toArc = normToArc;
            }

            using (new ColorScoop(Color.blue))
            {
                foreach (float angle in new[]{0, formArc, toArc})
                {
                    Vector3 startPos = Arc.GetDirection(_upward, _plate, angle).normalized * _arcRadis;
                    Arrow.Draw(Vector3.zero, startPos);
                    Handles.Label(startPos, $"[{angle}]");
                }
            }

            if(_useSegCount)
            {
                if (_useLineRenderer)
                {
                    Arc.DrawBySegCount(_lineArc, Vector3.zero, _arcRadis, formArc, toArc, _upward, _plate, _segCount);
                    Circle.DrawBySegCount(_lineCircle, Vector3.zero, _circleRadis, _upward, _segCount);
                }
                else
                {
                    _lineCircle.positionCount = 0;
                    _lineArc.positionCount = 0;
                    Arc.DrawBySegCount(Vector3.zero, _arcRadis, formArc, toArc, _upward, _plate, _segCount);
                    Circle.DrawBySegCount(Vector3.zero, _circleRadis, _upward, _segCount);
                }
            }
            else
            {
                if (_useLineRenderer)
                {
                    Arc.Draw(_lineArc, Vector3.zero, _arcRadis, formArc, toArc, _upward, _plate, _segAngle);
                    Circle.Draw(_lineCircle, Vector3.zero, _circleRadis, _upward, _segAngle);
                }
                else
                {
                    _lineCircle.positionCount = 0;
                    _lineArc.positionCount = 0;

                    Arc.Draw(Vector3.zero, _arcRadis, formArc, toArc, _upward, _plate, _segAngle);
                    Circle.Draw(Vector3.zero, _circleRadis, _upward, _segAngle);
                }
            }
        }
#endif
    }
}
