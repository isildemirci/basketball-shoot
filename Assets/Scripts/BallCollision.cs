using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    private AudioManager _audioManager;

    private void Awake()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("Ball")) return;
        if (gameObject.CompareTag("Spawned") && gameObject.activeInHierarchy)
        {
            if (collision.collider.CompareTag("Untagged"))
            {
                _audioManager.PlayAudio("BallHit");
            }
        }
    }
}
