using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockFireball : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DebugController.instance.AddToListFireBall();
        }
    }
}
