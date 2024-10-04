using UnityEngine;

namespace _02_Scripts
{
    public class BallPositionGizmoScript : MonoBehaviour
    {
        public Color gizmoColor = Color.yellow;
        public float gizmoSize = 0.5f;

        private void OnDrawGizmos()
        {
            Gizmos.color = gizmoColor; // ball이 위치할 수 있는 position을 scene view에서만 보여줄 수 있게 커러 세팅
            Gizmos.DrawCube(transform.position, Vector3.one * gizmoSize); // gizmo shape : cube
        }
    }
}