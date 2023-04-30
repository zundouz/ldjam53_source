using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts
{
    public class PlaytimeManager : MonoBehaviour
    {
        public static PlaytimeManager Instance { get; private set; }
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        private float _startTime;
        private float _endTime;
        public float ElapsedTime { get; private set; }
        public Text resultText;

        private void Start()
        {
            ElapsedTime = 0f;
        }
        
        // 計測開始のは外部から関数として呼び出す想定
        public void StartGame()
        {
            _startTime = Time.time;
        }

        public void EndGame()
        {
            _endTime = Time.time;
            ElapsedTime = _endTime - _startTime;
            // DisplayResult();
        }

        // private void DisplayResult()
        // {
        //     resultText.text = "Playtime: " + _elapsedTime.ToString("F2") + " seconds";
        // }
    }
}