using UnityEngine;

namespace _02_Scripts
{
    public class BallPositionGizmoScript : MonoBehaviour
    {
        public Color gizmoColor = Color.yellow;
        public float gizmoSize = 0.5f;

        private void OnDrawGizmos()
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawCube(transform.position, Vector3.one * gizmoSize);
        }
    }
}