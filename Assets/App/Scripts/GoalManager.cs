using UnityEngine;

namespace App.Scripts
{
    public class GoalManager : MonoBehaviour
    {
        private bool _isFirstTouchedGoal;
        
        // Start is called before the first frame update
        void Start()
        {
            _isFirstTouchedGoal = true;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        
        void OnTriggerEnter2D(Collider2D collider2d)
        {
            // ゴール処理は１回だけ
            if (_isFirstTouchedGoal)
            {
                if (collider2d.CompareTag($"SobaBox") || collider2d.CompareTag($"Player"))
                {
                    _isFirstTouchedGoal = false;
                    Debug.Log("GOAL!!!!");
                }
            }
        }
    }
}
