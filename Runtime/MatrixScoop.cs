using System;
using UnityEngine;

namespace SaintsDrawer
{
    public class MatrixScoop: IDisposable
    {
        private Matrix4x4 _matrix4;

        public MatrixScoop(Matrix4x4 matrix4)
        {
            _matrix4 = Gizmos.matrix;
            Gizmos.matrix = matrix4;
        }

        public void Dispose()
        {
            Gizmos.matrix = _matrix4;
        }
    }
}
