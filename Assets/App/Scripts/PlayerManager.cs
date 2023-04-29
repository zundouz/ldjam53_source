using UnityEngine;

namespace App.Scripts
{
    public class PlayerManager : MonoBehaviour
    {
        private float speed = 0.01f;
        
        // Start is called before the first frame update
        void Start()
        {
        }
        // Update is called once per frame
        void Update()
        {
            // プレイヤーは無重力の空間に対して上下左右キーを入力することで移動ができる
            Vector2 position = transform.position;

            if (Input.GetKey("left"))
            {
                position.x -= speed;
            }
            if (Input.GetKey("right"))
            {
                position.x += speed;
            }
            if (Input.GetKey("up"))
            {
                position.y += speed;
            }
            if (Input.GetKey("down"))
            {
                position.y -= speed;
            }

            transform.position = position;
        }
    }
}
