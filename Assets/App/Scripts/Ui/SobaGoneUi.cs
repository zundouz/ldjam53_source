using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Ui
{
    public class SobaGoneUi : MonoBehaviour
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
            // 死亡ステートのときのみ表示
            if (MyGameManager.GameState == MyGameManager.GameStateEnum.GameOver)
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
