using UnityEngine;

namespace App.Scripts
{
    public class PlayerManager : MonoBehaviour
    {
        public static Vector3 PlayerRotate { get; private set; } = Vector3.zero;
        public static Rigidbody2D PlayerRigidBody { get; set; }

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
            PlayerRigidBody = _rb;
        }

        private void Update()
        {
            // プレイヤーの回転を取得しておく
            PlayerRotate = transform.rotation.eulerAngles;
            
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
            _rb.AddForce(force); // Apply transformInput
            // transform の大きさに追従するrotation追加
            float RotationFactor = 0.1f;
            float rotationAmount = force.magnitude * RotationFactor; // 回転量は力の大きさに比例
            _rb.AddTorque(rotationAmount); // Rigidbodyにトルク（回転力）を追加
            
            _rb.AddTorque(_currentRotationSpeed * 2.5f * Time.deltaTime); // Apply rotationInput

            // Limit the player's speed
            var velocity = Vector2.ClampMagnitude(_rb.velocity, MaxSpeed);
            // Limit the player's rotation speed
            var angularVelocity = Mathf.Clamp(_rb.angularVelocity, -RotationSpeed, RotationSpeed / 2f);

            // Apply drag force to velocity
            velocity -= velocity * (Drag * Time.fixedDeltaTime);
            // Apply drag force to rotate
            angularVelocity -= angularVelocity * (Drag * Time.fixedDeltaTime);
            
            // Apply final velocity and angular velocity
            _rb.angularVelocity = angularVelocity;
            _rb.velocity = velocity;
        }
    }
}