using UnityEngine;

namespace App.Scripts
{
    public class PlayerManager : MonoBehaviour
    {
        private float thrust = 5.0f;
        private float maxSpeed = 5.0f;
        private float drag = 0.25f;

        private Rigidbody2D rb;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // FixedUpdate is called once per fixed time interval
        void FixedUpdate()
        {
            // プレイヤーは無重力の空間に対して上下左右キーを入力することで移動ができる
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector2 force = new Vector2(horizontal * thrust, vertical * thrust);
            rb.AddForce(force);

            // Limit the player's speed
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);

            // Apply drag force
            rb.velocity -= rb.velocity * drag * Time.fixedDeltaTime;
        }
    }
}