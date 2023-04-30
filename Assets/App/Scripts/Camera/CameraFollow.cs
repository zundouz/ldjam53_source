using System;
using UnityEngine;

namespace App.Scripts.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        public float smoothSpeed;
        public Vector3 offset;

        private void Start()
        {
            if (MyGameManager.IsRetrying)
            {
                // 検討：以下の処理を一応入れてみたものの、逆に味気なくなってしまったかも
                // カメラ位置の調整ではなく、画面遷移時の演出で回避できそうなので一旦コメントアウトしたまま
                
                // // Rボタンでリトライした時にカメラが中心によっていると画面酔いしてしまうため
                // // もしRボタンでリトライした場合は最初からカメラがプレイヤーを中心にしているような実装にしたい
                // // そのために、カメラの初期位置をプレイヤーの位置にする
                // var position = target.position;
                // // このままだとゲーム開始時に画面真っ暗になる不具合があるので、解消する
                // position = new Vector3(position.x, position.y, -10f);
                // transform.position = position;
            }

        }

        private void LateUpdate()
        {
            // メインゲーム状態以外はカメラ移動を受け付けない
            if (MyGameManager.GameState != MyGameManager.GameStateEnum.MainGame)
            {
                return;
            }
            
            // プレイヤーの回転を取得し、そば箱が上に伸びている場合はカメラ上の視界を良好にするようにオフセットyを追加する
            var playerRotate = PlayerManager.PlayerRotate;
            
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