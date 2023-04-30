using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Ui
{
    public class ToppingTextUi : MonoBehaviour
    {
        void Start()
        {
            // ゲーム開始時はImage を無効化する
            gameObject.GetComponent<Text>().enabled = false;
            
            // textのフォントのフィルターを切る
            gameObject.GetComponent<Text>().font.material.mainTexture.filterMode = FilterMode.Point;
        }

        void Update()
        {
            // リザルト画面でのみ表示するフォント
            if (MyGameManager.GameState == MyGameManager.GameStateEnum.Result)
            {
                // テキストの中身を、変数に反映
                gameObject.GetComponent<Text>().text = MyGameManager.ToppingCount.ToString();
                gameObject.GetComponent<Text>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<Text>().enabled = false;
            }
        }
    }
}