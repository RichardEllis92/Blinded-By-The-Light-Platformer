using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 7.5f;
    public Rigidbody2D theRB;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = transform.right * speed;
    }
}
