using UnityEngine;
// テキストUi用のインポート
using UnityEngine.UI;

namespace App.Scripts.Ui
{
    public class FinalScoreTextUi : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            // ゲーム開始時はImage を無効化する
            gameObject.GetComponent<Text>().enabled = false;
            
            // textのフォントのフィルターを切る
            gameObject.GetComponent<Text>().font.material.mainTexture.filterMode = FilterMode.Point;
        }

        // Update is called once per frame
        void Update()
        {
            // リザルト画面でのみ表示するフォント
            if (MyGameManager.GameState == MyGameManager.GameStateEnum.Result)
            {
                float timeSec = PlaytimeManager.Instance.ElapsedTime;
                // timeSec の結果を 分 に変換する
                float timeMin = (timeSec / 60f);
                
                var finalScore = (float)SobaBoxManager.SobaBoxCount * (float)MyGameManager.ToppingCount / timeMin;
                int finalScoreInt = (int)(finalScore * 100f); // 見栄えの良さもあって、最終スコアは×100した整数を表示する
                // テキストの中身を、変数に反映
                gameObject.GetComponent<Text>().text = finalScoreInt.ToString(); // 少数第二桁まで表示
                gameObject.GetComponent<Text>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<Text>().enabled = false;
            }
        }
    }
}