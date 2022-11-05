using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public Transform teleportTarget;
    public GameObject thePlayer;
    float waitTime = 0.17f;
    public Animator AnimRefObj;
    public static TeleportPlayer instance;
    public bool fading;

    private void Start()
    {
        instance = this;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(WaitForFadeIn());
            fading = false;
        }
    }

    IEnumerator WaitForFadeIn()
    {
        thePlayer.transform.position = teleportTarget.transform.position;
        AnimRefObj.Play("FadeInAnim");
        PlayerController.instance.fading = true;
        yield return new WaitForSeconds(waitTime);
        PlayerController.instance.fading = false;
    }
}
