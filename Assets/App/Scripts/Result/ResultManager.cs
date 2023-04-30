using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Result
{
    public class ResultManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            // CanvasからImageを取得し、無効化する
            gameObject.GetComponent<Image>().enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            // ゲームのステートがResultのときのみ画像を有効化、それ以外は無効化する
            if (MyGameManager.GameState == MyGameManager.GameStateEnum.Result)
            {
                gameObject.GetComponent<Image>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<Image>().enabled = false;
            }
        }
    }
}
