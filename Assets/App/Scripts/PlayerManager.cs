using System;
using UnityEngine;

namespace App.Scripts
{
    public class PlayerManager : MonoBehaviour
    {
        private const float Thrust = 5.0f;
        private const float MaxSpeed = 5.0f;
        private const float Drag = 0.25f;

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
        }

        // FixedUpdate is called once per fixed time interval
        void FixedUpdate()
        {


            Vector2 force = new Vector2(_horizontal * Thrust, _vertical * Thrust);
            _rb.AddForce(force);

            // Limit the player's speed
            var velocity = Vector2.ClampMagnitude(_rb.velocity, MaxSpeed);

            // Apply drag force
            velocity -= velocity * (Drag * Time.fixedDeltaTime);
            _rb.velocity = velocity;
        }
    }
}