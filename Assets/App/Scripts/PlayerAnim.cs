using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace App.Scripts
{
    public class PlayerAnim : MonoBehaviour
    {
        private bool _isAnimEnd;
        
        // プレイヤーキャラのアニメーション4枚を行うために、スプライト４枚を読み込む
        // 2と4は実質同じ画像だけど、時間ないので別々にしてしまう
        [SerializeField] private Sprite playerSprite1;
        [SerializeField] private Sprite playerSprite2;
        [SerializeField] private Sprite playerSprite3;
        [SerializeField] private Sprite playerSprite4;
        
        // ゲームオーバー用の画像
        [SerializeField] private Sprite playerGameOverSprite;

        // Start is called before the first frame update
        void Start()
        {
            _isAnimEnd = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (MyGameManager.GameState == MyGameManager.GameStateEnum.GameOver)
            {
                // ゲームオーバー時は、プレイヤーの画像を変更する
                gameObject.GetComponent<SpriteRenderer>().sprite = playerGameOverSprite;
                return;
            }
            
            // 画像アニメーション用のコルーチン
            // （アニメーション完了してから呼ぶ）
            if (_isAnimEnd)
            {
                StartCoroutine(nameof(PlayerAnimCoroutine));
            }
        }
        
        // 0.2秒の感覚でplayerSpriteを切り替えることで、アニメーションを行うコルーチン
        IEnumerator PlayerAnimCoroutine()
        {
            _isAnimEnd = false;
            var waitTime = new WaitForSeconds(0.2f);
            
            // 画像を切り替える
            gameObject.GetComponent<SpriteRenderer>().sprite = playerSprite1;
            yield return waitTime;
            gameObject.GetComponent<SpriteRenderer>().sprite = playerSprite2;
            yield return waitTime;
            gameObject.GetComponent<SpriteRenderer>().sprite = playerSprite3;
            yield return waitTime;
            gameObject.GetComponent<SpriteRenderer>().sprite = playerSprite4;
            yield return waitTime;
            _isAnimEnd = true;
        }
    }
}
