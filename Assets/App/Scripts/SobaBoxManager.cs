using UnityEngine;

namespace App.Scripts
{
    public class SobaBoxManager : MonoBehaviour
    {
        [SerializeField] private int SobaBoxId; // ID を設定することで、どのそば箱がぶつかったかを判別できるようにする
        
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
                Destroy(gameObject);
            }
        }
    }
}
