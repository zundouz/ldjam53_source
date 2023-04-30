using App.Scripts.Title;
using UnityEngine;

namespace App.Scripts
{
    public class GoalManager : MonoBehaviour
    {
        private bool _isFirstTouchedGoal;
        
        // Start is called before the first frame update
        void Start()
        {
            _isFirstTouchedGoal = true;
        }

        // Update is called once per frame
        void Update()
        {
            // ゲームクリアステートのときだけZボタン入力を待ち、入力がきたらタイトルへ戻る処理
            if (MyGameManager.GameState == MyGameManager.GameStateEnum.Result)
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    MyGameManager.ResetGameToInitialState();
                    // 初期化処理としてシーンを読み直してしまう
                    UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
                }
            }
        }
        
        void OnTriggerEnter2D(Collider2D collider2d)
        {
            // ゴール処理は１回だけ
            if (_isFirstTouchedGoal)
            {
                if (collider2d.CompareTag($"SobaBox") || collider2d.CompareTag($"Player"))
                {
                    _isFirstTouchedGoal = false;
                    Debug.Log("GOAL!!!!");
                    MyGameManager.GameState = MyGameManager.GameStateEnum.Result;
                }
            }
        }
    }
}
