using SaintsDrawer;
using UnityEditor;
using UnityEngine;

namespace SaintsDraw.Samples
{
    public class DrawArrow : MonoBehaviour
    {
        [SerializeField] private Vector3 _up;
        [SerializeField] private Vector3 _target;

        [SerializeField] private float _arrowHeadLength;
        [SerializeField] private float _arrowHeadAngle;

        [SerializeField] private bool _useLocalPosition;
        [SerializeField] private bool _useLineRenderer;
        [SerializeField] private LineRenderer _lineRenderer;

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if(_useLocalPosition)
            {
                Gizmos.matrix = Handles.matrix = transform.localToWorldMatrix;
            }

            using (new ColorScoop(Color.green))
            {
                // 2d game use this
                Arrow.Draw(Vector2.zero, Vector2.up);
            }

            using (new ColorScoop(Color.red))
            {
                Arrow.Draw(Vector2.zero, Vector2.right);
            }

            using (new ColorScoop(Color.blue))
            {
                // 3d game need to specify up vector in some case
                Arrow.Draw(Vector3.zero, Vector3.forward);
                Arrow.Draw(Vector3.zero, Vector3.one, up: new Vector3(-1f, -1f, -1f));
            }

            if (_useLineRenderer)
            {
                Arrow.Draw(_lineRenderer , Vector3.zero, _target, _arrowHeadLength, _arrowHeadAngle, _up);
            }
            else
            {
                _lineRenderer.positionCount = 0;
                Arrow.Draw(Vector3.zero, _target, _arrowHeadLength, _arrowHeadAngle, _up);
            }
        }
#endif
    }
}
