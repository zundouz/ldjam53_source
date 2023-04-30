using UnityEngine;

namespace App.Scripts
{

    
    public class SobaBoxManager : MonoBehaviour
    {
        public enum SobaBoxHavingState
        {
            OnPlayer,
            AwayFromPlayer,
            Destroyed
        };

        public static bool IsGameOver { get; private set; }
        public static int SobaBoxCount { get; private set; }

        // 最初のステート
        public static SobaBoxHavingState[] SobaBoxHavingStateList =
        {
            SobaBoxHavingState.OnPlayer,
            SobaBoxHavingState.OnPlayer,
            SobaBoxHavingState.OnPlayer,
            SobaBoxHavingState.OnPlayer,
            SobaBoxHavingState.OnPlayer,
            //
            SobaBoxHavingState.OnPlayer,
            SobaBoxHavingState.OnPlayer,
            SobaBoxHavingState.OnPlayer,
            SobaBoxHavingState.OnPlayer,
            SobaBoxHavingState.OnPlayer,
            //
            SobaBoxHavingState.OnPlayer,
            SobaBoxHavingState.OnPlayer,
            SobaBoxHavingState.OnPlayer,
            SobaBoxHavingState.OnPlayer,
            SobaBoxHavingState.OnPlayer,
        };
        
        [SerializeField] private int SobaBoxId; // ID を設定することで、どのそば箱がぶつかったかを判別できるようにする
        // [SerializeField] private PhysicsMaterial2D noFrictionMaterial; // できないらしい
        
        // プレイヤーの手元から離れているか
        private bool _isAwayFromPlayer = false;
        
        // Start is called before the first frame update
        void Start()
        {
            // 初期化
            for (int i = 0 ; i < SobaBoxHavingStateList.Length ; i++)
            {
                SobaBoxHavingStateList[i] = SobaBoxHavingState.OnPlayer;
            }

            IsGameOver = false;
            SobaBoxCount = 15; // 初期値
        }

        // Update is called once per frame
        void Update()
        {
            // 以下の処理はさすがに操作が厳しくなりすぎなので消しました
            // // 自分より上のそば箱を全て無効にする
            // if (SobaBoxHavingStateList[SobaBoxId] == SobaBoxHavingState.AwayFromPlayer)
            // {
            //     for (int i = SobaBoxId; i < SobaBoxHavingStateList.Length; i++)
            //     {
            //         SobaBoxHavingStateList[i] = SobaBoxHavingState.AwayFromPlayer;
            //     }
            // }
        }
        
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (MyGameManager.GameState == MyGameManager.GameStateEnum.Result)
            {
                // リザルト画面中は、ぶつかり判定を行わない
                return;
            }
            
            if (collision.collider.CompareTag($"Wall") || SobaBoxHavingStateList[SobaBoxId] == SobaBoxHavingState.AwayFromPlayer)
            {
                Debug.Log($"ぶつかりました。ID: {SobaBoxId}");
                
                // ただ消すだけだと味気ないので、ぶつかった後にちょっとRigidBodyのような挙動が入るようにしてみるテスト
                // Destroy(gameObject);
                
                // ぶつかった最初の１回だけ
                if (!_isAwayFromPlayer)
                {
                    // 効果音
                    AudioManager.Instance.PlaySe(AudioManager.SePath.SOBA_HIT);
                    
                    // RigidBody2Dコンポーネントを、新規でアタッチ
                    var rb = gameObject.AddComponent<Rigidbody2D>();
                    rb.gravityScale = 0.01f;
                    rb.angularVelocity = 0.05f;
                    rb.mass = 0.0001f; // 質量があると、間の箱がぶつかってしまったときにプレイヤー操作感に悪影響が出る
                    _isAwayFromPlayer = true;
                    gameObject.transform.parent = null;
                    
                    // 色を変えて、壁に当たったことを分かりやすくする
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(0.35f, 0.35f, 0.35f, 1.0f);
                    // // BoxColliderを取得し、Colliderの当たり判定を無効化する
                    // gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    
                    SobaBoxHavingStateList[SobaBoxId] = SobaBoxHavingState.AwayFromPlayer;
                    
                    // // SobaBoxHavingStateList の中身をデバッグ表示
                    // foreach (var state in SobaBoxHavingStateList)
                    // {
                    //     Debug.Log($"{state}");
                    // }
                    
                    // ぶつかった時だけ、現在の生存そば数を更新しておく
                    CountSobaBox();
                    
                    // ぶつかった時だけ、ゲームオーバー判定を行う
                    CheckGameOver();
                }
            }
        }

        private static void CountSobaBox()
        {
            // 生存中の蕎麦箱の数を数える
            int aliveSobaNum = 0;
            foreach (var state in SobaBoxHavingStateList)
            {
                if (state == SobaBoxHavingState.OnPlayer)
                {
                    aliveSobaNum++;
                }
            }

            SobaBoxCount = aliveSobaNum;
        }
        
        private static void CheckGameOver()
        {
            // SobaBoxHavingStateList の中にOnPlayer が存在しなければ、ゲームオーバー判定
            foreach (var state in SobaBoxHavingStateList)
            {
                if (state == SobaBoxHavingState.OnPlayer)
                {
                    IsGameOver = false;
                    return;
                }
            }
            
            // ここまで来たら、ゲームオーバー
            IsGameOver = true;
        }
        
    }
}
