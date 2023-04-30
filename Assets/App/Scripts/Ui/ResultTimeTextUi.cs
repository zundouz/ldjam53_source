using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Ui
{
    public class ResultTimeTextUi : MonoBehaviour
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
                float timeSec = PlaytimeManager.Instance.ElapsedTime;
                // timeSec の結果を 分 に変換する
                float timeMin = (timeSec / 60f);
                // テキストの中身を、変数に反映
                gameObject.GetComponent<Text>().text = timeMin.ToString(CultureInfo.InvariantCulture);
                gameObject.GetComponent<Text>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<Text>().enabled = false;
            }
        }
    }
}