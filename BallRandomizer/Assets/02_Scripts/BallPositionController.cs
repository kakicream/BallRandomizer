using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace _02_Scripts
{
    public class BallPositionController : MonoBehaviour
    {
        [Header("실험 순서 조합을 읽어올 CSV 파일 경로")]
        public string filePath;

        public List<GameObject> positionPreset = new();
        public GameObject ball;
        private Transform ballTransform;

        [SerializeField] private int positionListIndex;
        [SerializeField] private List<int> randomizedPositionIndex = new();

        // 참가자 번호를 입력 (유니티 인스펙터에서 설정 가능)
        [SerializeField] private int participantNumber;

        private void Start()
        {
            ballTransform = ball.GetComponent<Transform>();
            positionListIndex = 0;
            LoadRandomizedPositionsFromCSV();
            SetBallPosition();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (positionListIndex < positionPreset.Count - 1)
                {
                    positionListIndex += 1;
                }
                else
                {
                    positionListIndex = 0;
                }
                // SetBallPosition();
            }

            if (Input.GetKeyDown(KeyCode.Period))
            {
                participantNumber += 1;
                LoadRandomizedPositionsFromCSV();
                // SetBallPosition(); // 키보드를 누를 때만 ball의 위치 업데이트
            }

            SetBallPosition(); // 실시간으로 ball의 위치 업데이트
        }

        private void SetBallPosition()
        {
            if (randomizedPositionIndex.Count > 0)
            {
                var index = randomizedPositionIndex[positionListIndex] - 1;
                ballTransform.position = positionPreset[index].transform.position;
                ballTransform.rotation = positionPreset[index].transform.rotation;
            }
            else
            {
                Debug.LogError("randomizedPositionIndex 리스트가 비어 있습니다.");
            }
        }

        // CSV 파일에서 참가자 번호에 맞는 랜덤 위치 인덱스를 가져오는 함수
        // ReSharper disable Unity.PerformanceAnalysis
        private void LoadRandomizedPositionsFromCSV()
        {
            // string filePath = Path.Combine(Application.dataPath, "Outcomes", "random_sequences.csv");

            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);

                // 첫 번째 행은 헤더이므로 제외하고 참가자 번호에 맞는 행을 찾음
                for (var i = 1; i < lines.Length; i++)
                {
                    var values = lines[i].Split(',');

                    // 참가자 번호와 일치하는지 확인
                    if (int.TryParse(values[0], out var participant) && participant == participantNumber)
                    {
                        randomizedPositionIndex.Clear(); // 기존 리스트 초기화

                        // 참가자 번호 뒤의 숫자들을 randomizedPositionIndex 리스트에 추가
                        for (var j = 1; j < values.Length; j++)
                        {
                            if (int.TryParse(values[j], out var position))
                            {
                                randomizedPositionIndex.Add(position);
                            }
                        }

                        break;
                    }
                }

                if (randomizedPositionIndex.Count == 0)
                {
                    Debug.LogError($"참가자 번호 {participantNumber}에 해당하는 데이터를 찾을 수 없습니다.");
                }
            }
            else
            {
                Debug.LogError($"CSV 파일을 찾을 수 없습니다: {filePath}");
            }
        }
    }
}

// using System.Collections.Generic;
// using UnityEngine;
//
// namespace _02_Scripts
// {
//     public class BallPositionController : MonoBehaviour
//     {
//         public List<GameObject> positionPreset = new();
//         public GameObject ball;
//         private Transform ballTransform;
//
//         [SerializeField] private int positionListIndex;
//         [SerializeField] private List<int> randomizedPositionIndex = new() { 3, 6, 9, 1, 4, 7, 2, 5, 8, 11, 10 };
//
//         private void Start()
//         {
//             ballTransform = ball.GetComponent<Transform>();
//             positionListIndex = 0;
//             SetBallPosition();
//         }
//
//         private void Update()
//         {
//             if (Input.GetKeyDown(KeyCode.Space))
//             {
//                 if (positionListIndex < positionPreset.Count - 1)
//                 {
//                     positionListIndex += 1;
//                 }
//                 else
//                 {
//                     positionListIndex = 0;
//                 }
//                 SetBallPosition();
//             }
//         }
//
//         private void SetBallPosition()
//         {
//             var index = randomizedPositionIndex[positionListIndex] - 1;
//             ballTransform.position = positionPreset[index].transform.position;
//             ballTransform.rotation = positionPreset[index].transform.rotation;
//         }
//     }
// }