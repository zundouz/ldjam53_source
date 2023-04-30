using App.Scripts.Title;
using UnityEngine;

namespace App.Scripts
{
    public class MyGameManager : MonoBehaviour
    {
        // ゲーム起動時のステートはTitleとする
        public static GameStateEnum GameState { get; set; } = GameStateEnum.Title;

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
        }
    }
}
