using System.Collections;
using UnityEngine;

namespace App.Scripts.Ui
{
    public class DescriptionUi : MonoBehaviour
    {
        private bool _isAnimEnd;
        
        void Start()
        {
            _isAnimEnd = true;
        }

        void Update()
        {
            // タイトル画面でのみ表示するフォント
            if (MyGameManager.GameState == MyGameManager.GameStateEnum.MainGame)
            {
                if (_isAnimEnd)
                {
                    StartCoroutine(nameof(UiBlinkAnim));
                }
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        
                
        // 0.2秒の感覚でplayerSpriteを切り替えることで、アニメーションを行うコルーチン
        IEnumerator UiBlinkAnim()
        {
            _isAnimEnd = false;
            var waitTime = new WaitForSeconds(1.0f);
            
            // 画像を切り替える
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            yield return waitTime;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            yield return waitTime;
            _isAnimEnd = true;
        }
    }
}