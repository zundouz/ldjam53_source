using UnityEngine;

namespace App.Scripts
{
    public class SobaBoxManager : MonoBehaviour
    {
        [SerializeField] private int SobaBoxId; // ID を設定することで、どのそば箱がぶつかったかを判別できるようにする
        
        // プレイヤーの手元から離れているか
        private bool _isAwayFromPlayer = false;
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag($"Wall"))
            {
                Debug.Log($"ぶつかりました。ID: {SobaBoxId}");
                
                // ただ消すだけだと味気ないので、ぶつかった後にちょっとRigidBodyのような挙動が入るようにしてみるテスト
                // Destroy(gameObject);
                
                // ぶつかった最初の１回だけ
                if (!_isAwayFromPlayer)
                {
                    // RigidBody2Dコンポーネントを、新規でアタッチ
                    var rb = gameObject.AddComponent<Rigidbody2D>();
                    rb.gravityScale = 0.1f;
                    _isAwayFromPlayer = true;
                    gameObject.transform.parent = null;
                }
            }
        }
    }
}
