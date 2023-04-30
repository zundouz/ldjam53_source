using UnityEngine;
// テキストUi用のインポート
using UnityEngine.UI;

namespace App.Scripts.Ui
{
    public class SobaTextUi : MonoBehaviour
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
                // テキストの中身を、変数に反映
                gameObject.GetComponent<Text>().text = SobaBoxManager.SobaBoxCount.ToString();
                gameObject.GetComponent<Text>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<Text>().enabled = false;
            }
        }
    }
}
