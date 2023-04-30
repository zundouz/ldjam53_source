using UnityEngine;

namespace App.Scripts.Title
{
    public class TitleManager : MonoBehaviour
    {
        public static bool IsTitleEnded { get; set; } = false;
        
        // Start is called before the first frame update
        void Start()
        {
            // 初回とそれ以外でタイトル画像表示の処理を分ける
            if (IsTitleEnded)
            {
                // ２回目以降なので、リスタートはMainGame側に任せる
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                // 初回
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (MyGameManager.GameState == MyGameManager.GameStateEnum.Title)
            {
                // タイトル処理
                if (Input.GetKeyDown(KeyCode.Z) || 
                    Input.GetKeyDown(KeyCode.Space) ||
                    Input.GetKeyDown(KeyCode.Return)) // Enter Key
                {
                    IsTitleEnded = true;
                    MyGameManager.GameState = MyGameManager.GameStateEnum.LoadingMainGame;
                }
            }
            else
            {
                // 今はタイトル画面ではないので無効化
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
}
