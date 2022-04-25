using System;
using UnityEngine;

public class BasketManager : MonoBehaviour
{
    private int _score = 0;

    public static event Action<int> OnScored;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spawned"))
        {
            _score++;
            OnScored?.Invoke(_score);
        }
    }
}
