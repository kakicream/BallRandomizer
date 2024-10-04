using System.Collections.Generic;
using UnityEngine;

namespace _02_Scripts
{
    public class BallPositionGenerator : MonoBehaviour
    {
        public Transform player; // Player 오브젝트의 Transform
        public List<GameObject> positionPreset = new(); // 위치 오브젝트를 담는 리스트
        public float radius = 1f; // 원형 궤도의 반지름
        public float angleIncrement = 10f; // 각도 증가량

        private void Start()
        {
            SetPosition();
        }

        private void SetPosition()
        {
            // Pos_6은 Player의 전방 1m에 위치 설정
            if (positionPreset.Count > 0)
            {
                Transform pos6 = positionPreset[5].transform; // Pos_6은 리스트의 5번째 인덱스
                pos6.position = player.position + player.forward * radius; // Pos_6 위치 : Player 정면

                // Pos_1~Pos_5 (왼쪽)
                for (int i = 0; i < 5; i++)
                {
                    Transform pos = positionPreset[i].transform;
                    float angle = -angleIncrement * (4 - i) - 10; // 왼쪽은 음수, Pos_1이 가장 왼쪽
                    Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;
                    pos.position = player.position + direction * radius;
                }

                // Pos_7~Pos_11 (오른쪽)
                for (int i = 0; i < 5; i++)
                {
                    Transform pos = positionPreset[i + 6].transform; // Pos_7~Pos_11
                    float angle = angleIncrement * (i + 1); // 오른쪽은 양수
                    Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;
                    pos.position = player.position + direction * radius;
                }
            }
            else
            {
                Debug.LogWarning("positionPreset 리스트에 위치 오브젝트가 없습니다.");
            }
        }

        private void OnDrawGizmos()
        {
            if (player != null && positionPreset.Count > 0)
            {
                // Player의 위치를 Gizmos로 표시
                Gizmos.color = Color.red; // 색상 설정
                Gizmos.DrawSphere(player.position, 0.1f); // Player 위치에 구체 그리기

                // Pos_N 위치를 설정하고, Player에서 Pos_N까지 선을 그리기
                foreach (var pos in positionPreset)
                {
                    if (pos != null)
                    {
                        Gizmos.color = Color.magenta; // 색상 설정
                        Gizmos.DrawLine(player.position, pos.transform.position); // Player에서 Pos_N까지 선 그리기
                        Gizmos.DrawSphere(pos.transform.position, 0.1f); // Pos_N 위치에 구체 그리기
                    }
                }
            }
        }
    }
}
