using UnityEngine;

namespace App.Scripts.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        public float smoothSpeed;
        public Vector3 offset;

        private void LateUpdate()
        {
            // プレイヤーの回転を取得し、そば箱が上に伸びている場合はカメラ上の視界を良好にするようにオフセットyを追加する
            var playerRotate = PlayerManager.PlayerRotate;
            // if (playerRotate.z < 10f && playerRotate.z > -10f)
            // {
            //     offset.y = 4.5f;
            // }
            // else
            // {
            //     offset.y = 0f;
            // }
            
            // 条件分岐よりも cos 使ったほうがよさそう -> よくなった
            offset.x = -4.0f * Mathf.Sin((Mathf.Deg2Rad * playerRotate.z)) * PlayerManager.PlayerRigidBody.velocity.magnitude * 0.3f;
            offset.y = 5.0f * Mathf.Cos((Mathf.Deg2Rad * playerRotate.z)) * PlayerManager.PlayerRigidBody.velocity.magnitude * 0.3f;
            
            Vector3 desiredPosition = target.position + offset;

            // オフセットによってプレイヤーが画面から出てしまうようなら、オフセットを0にする
            if (desiredPosition.x < target.position.x && PlayerManager.PlayerRigidBody.velocity.x > 0f)
            {
                offset.x = 0f;
                desiredPosition = target.position + offset;
            }
            if (desiredPosition.x > target.position.x && PlayerManager.PlayerRigidBody.velocity.x < 0f)
            {
                offset.x = 0f;
                desiredPosition = target.position + offset;
            }
            if (desiredPosition.y > target.position.y && PlayerManager.PlayerRigidBody.velocity.y < 0f)
            {
                offset.y = 0f;
                desiredPosition = target.position + offset;
            }
            if (desiredPosition.y < target.position.y && PlayerManager.PlayerRigidBody.velocity.y > 0f)
            {
                offset.y = 0f;
                desiredPosition = target.position + offset;
            }
            
            
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            smoothedPosition.z = transform.position.z; // Keep the camera's original Z position
            
            transform.position = smoothedPosition;
        }
    }
}