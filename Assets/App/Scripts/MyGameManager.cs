using App.Scripts.Title;
using UnityEngine;

namespace App.Scripts
{
    public class MyGameManager : MonoBehaviour
    {
        // ゲーム起動時のステートはTitleとする
        public static GameStateEnum GameState { get; set; } = GameStateEnum.Title;
        // リトライによる初期化かどうかの判定
        public static bool IsRetrying { get; set; } = false;

        public enum GameStateEnum
        {
            Title,
            Option,
            LoadingMainGame,
            MainGame,
            GameOver,
            Result,
        };
    
        // Start is called before the first frame update
        void Start()
        {
            if (TitleManager.IsTitleEnded)
            {
                GameState = GameStateEnum.LoadingMainGame;
            }
            else
            {
                GameState = GameStateEnum.Title;
            }
        }

        // Update is called once per frame
        void Update()
        {
            // 画面遷移処理が終わったら、MainGameを開始する
            // TODO: 画面遷移処理の実装
            if (GameState == GameStateEnum.LoadingMainGame)
            {
                GameState = GameStateEnum.MainGame;
            }
            
            // ゲームオーバーステートの変更
            if (SobaBoxManager.IsGameOver)
            {
                GameState = GameStateEnum.GameOver;
            }

            // 画面遷移中以外のステートは、Rキーでリトライできるようにしておく
            if (GameState != GameStateEnum.LoadingMainGame)
            {
                // Rキーで最初からできるようにしておく
                if (Input.GetKeyDown(KeyCode.R))
                {
                    MyGameManager.ExecuteRetry();
                }
            }
        }

        public static void ExecuteRetry()
        {
            // プレイヤーのRボタン押下を検知して、シーンを再読み込みすることでスタートからリトライする
            IsRetrying = true;
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        }

        // ゲーム起動直後の状態に初期化する関数
        // リザルト→タイトル遷移時に呼ばれるイメージ
        public static void ResetGameToInitialState()
        {
            GameState = GameStateEnum.Title;
            IsRetrying = true;
            TitleManager.IsTitleEnded = false;
        }
    }
}
