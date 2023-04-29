using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Negi : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider2d)
    {
        if (collider2d.CompareTag($"SobaBox") || collider2d.CompareTag($"Player"))
        {
            Destroy(gameObject);
        }
    }
}
