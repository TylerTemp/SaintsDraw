using System;
using UnityEngine;

namespace SaintsDraw
{
    public class ColorScoop: IDisposable
    {
        private readonly Color _color;

        public ColorScoop(Color newColor)
        {
            _color = Gizmos.color;
            Gizmos.color = newColor;
        }

        public void Dispose()
        {
            Gizmos.color = _color;
        }
    }
}
