using UnityEngine;

namespace App.Scripts
{
    public class PlayerManager : MonoBehaviour
    {
        private const float Thrust = 5.0f;
        private const float MaxSpeed = 5.0f;
        private const float Drag = 0.25f;

        private Rigidbody2D _rb;

        // Start is called before the first frame update
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        // FixedUpdate is called once per fixed time interval
        void FixedUpdate()
        {
            // プレイヤーは無重力の空間に対して上下左右キーを入力することで移動ができる
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector2 force = new Vector2(horizontal * Thrust, vertical * Thrust);
            _rb.AddForce(force);

            // Limit the player's speed
            var velocity = Vector2.ClampMagnitude(_rb.velocity, MaxSpeed);

            // Apply drag force
            velocity -= velocity * (Drag * Time.fixedDeltaTime);
            _rb.velocity = velocity;
        }
    }
}