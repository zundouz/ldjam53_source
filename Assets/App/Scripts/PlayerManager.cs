using UnityEngine;

namespace App.Scripts
{
    public class PlayerManager : MonoBehaviour
    {
        private const float Thrust = 5.0f;
        private const float MaxSpeed = 5.0f;
        private const float Drag = 0.25f;
        
        private const float RotationSpeed = 100.0f;
        private const float RotationAcceleration = 10f;
        private float _currentRotationSpeed = 0f;

        private Rigidbody2D _rb;
        private float _horizontal;
        private float _vertical;

        // Start is called before the first frame update
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            // プレイヤーは無重力の空間に対して上下左右キーを入力することで移動ができる
            _horizontal = Input.GetAxis("Horizontal");
            _vertical = Input.GetAxis("Vertical");
            
            // 回転処理
            float rotation = 0f;

            if (Input.GetKey(KeyCode.Q))
            {
                rotation = 1f;
            }
            else if (Input.GetKey(KeyCode.E))
            {
                rotation = -1f;
            }

            _currentRotationSpeed = Mathf.Lerp(_currentRotationSpeed, rotation * RotationSpeed, RotationAcceleration * Time.deltaTime);
        }

        // FixedUpdate is called once per fixed time interval
        void FixedUpdate()
        {
            Vector2 force = new Vector2(_horizontal * Thrust, _vertical * Thrust);
            _rb.AddForce(force); // transform
            _rb.AddTorque(_currentRotationSpeed * 5f * Time.deltaTime); // rotation

            // Limit the player's speed
            var velocity = Vector2.ClampMagnitude(_rb.velocity, MaxSpeed);
            // Limit the player's rotation speed
            _rb.angularVelocity = Mathf.Clamp(_rb.angularVelocity, -RotationSpeed, RotationSpeed);

            // Apply drag force to velocity
            velocity -= velocity * (Drag * Time.fixedDeltaTime);
            // Apply drag force to rotate
            _rb.angularVelocity -= _rb.angularVelocity * (Drag * Time.fixedDeltaTime);
            
            _rb.velocity = velocity;
        }
    }
}