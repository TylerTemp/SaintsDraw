using UnityEngine;

namespace SaintsDraw.Samples
{
    public class DrawRect : MonoBehaviour
    {
        public bool draw = true;

        private void OnDrawGizmos()
        {
            if (!draw)
            {
                return;
            }

            using (new ColorScoop(Color.green))
            {
                UIGizmos.DrawWireRectTransform(GetComponent<RectTransform>());
            }

        }
    }
}
