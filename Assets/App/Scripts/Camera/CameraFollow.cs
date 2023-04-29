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
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            smoothedPosition.z = transform.position.z; // Keep the camera's original Z position
            
            transform.position = smoothedPosition;
        }
    }
}