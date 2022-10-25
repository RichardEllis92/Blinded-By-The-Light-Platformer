using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPickup : MonoBehaviour
{
    public int foodValue = 1;
    public float rotationSpeed;

    private void Update()
    {
        transform.Rotate(Vector3.back * Time.deltaTime * rotationSpeed, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            LevelManager.instance.GetFood(foodValue);

            Destroy(gameObject);
            //AudioManager.instance.PlaySFX(4);
        }
    }
}
