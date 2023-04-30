using UnityEngine;

namespace App.Scripts.Bg
{
    public class ScrollingBackgroundSpace : MonoBehaviour
    {
        public GameObject player;
        public float parallaxFactor;
    
        private Vector3 startPosition;
        private Vector3 playerStartPosition;
        private UnityEngine.Camera mainCamera;

        void Start()
        {
            startPosition = transform.position;
            playerStartPosition = player.transform.position;
            mainCamera = UnityEngine.Camera.main;
        }

        void Update()
        {
            Vector3 offset = player.transform.position - playerStartPosition;
            Vector3 newPosition = startPosition + new Vector3(offset.x * parallaxFactor, offset.y * parallaxFactor, 0);

            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            float backgroundWidth = spriteRenderer.bounds.size.x;
            float backgroundHeight = spriteRenderer.bounds.size.y;

            float camHalfHeight = mainCamera.orthographicSize + 164f;
            float camHalfWidth = mainCamera.aspect * camHalfHeight;

            float clampedX = Mathf.Clamp(newPosition.x, mainCamera.transform.position.x - camHalfWidth, mainCamera.transform.position.x + camHalfWidth - backgroundWidth);
            float clampedY = Mathf.Clamp(newPosition.y, mainCamera.transform.position.y - camHalfHeight, mainCamera.transform.position.y + camHalfHeight - backgroundHeight);

            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }
}