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

        // 最初のステート
        public static SobaBoxHavingState[] SobaBoxHavingStateList =
        {
            SobaBoxHavingState.OnPlayer,
            SobaBoxHavingState.OnPlayer,
            SobaBoxHavingState.OnPlayer,
            SobaBoxHavingState.OnPlayer,
            SobaBoxHavingState.OnPlayer,
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
        
        }

        // Update is called once per frame
        void Update()
        {
            // 自分より上のそば箱を全て無効にする
            if (SobaBoxHavingStateList[SobaBoxId] == SobaBoxHavingState.AwayFromPlayer)
            {
                for (int i = SobaBoxId; i < SobaBoxHavingStateList.Length; i++)
                {
                    SobaBoxHavingStateList[i] = SobaBoxHavingState.AwayFromPlayer;
                }
            }
        }
        
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag($"Wall") || SobaBoxHavingStateList[SobaBoxId] == SobaBoxHavingState.AwayFromPlayer)
            {
                Debug.Log($"ぶつかりました。ID: {SobaBoxId}");
                
                // ただ消すだけだと味気ないので、ぶつかった後にちょっとRigidBodyのような挙動が入るようにしてみるテスト
                // Destroy(gameObject);
                
                // ぶつかった最初の１回だけ
                if (!_isAwayFromPlayer)
                {
                    // RigidBody2Dコンポーネントを、新規でアタッチ
                    var rb = gameObject.AddComponent<Rigidbody2D>();
                    rb.gravityScale = 0.01f;
                    rb.angularVelocity = 0.05f;
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
                }
            }
        }
    }
}
