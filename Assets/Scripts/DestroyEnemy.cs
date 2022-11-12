using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    public ParticleSystem smokeEffect;
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fireball")
        {
            AudioManager.instance.PlaySFX(5);
            Destroy(gameObject);
            Destroy(GameObject.FindWithTag("Fireball"));
            Instantiate(smokeEffect, transform.position, Quaternion.LookRotation(transform.localScale));
            Debug.Log("Enemy hit!");
        }
    }
}
