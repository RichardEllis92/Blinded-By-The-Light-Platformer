using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoubleJump : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DebugController.instance.AddToListDoubleJump();
        }
    }
}
