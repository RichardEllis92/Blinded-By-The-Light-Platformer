using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public Transform teleportTarget;
    float waitTime = 0.17f;
    public Animator AnimRefObj;
    public static TeleportPlayer instance;
    public bool fading;
    public GameObject fadeUI;

    private void Start()
    {
        instance = this;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.position = teleportTarget.transform.position;
            StartCoroutine(WaitForFadeIn());
            fading = false;
        }
    }

    IEnumerator WaitForFadeIn()
    {
        fadeUI.SetActive(true);
        AnimRefObj.Play("FadeInAnim");
        AudioManager.instance.PlaySFX(6);
        PlayerController.instance.fading = true;
        yield return new WaitForSeconds(waitTime);
        PlayerController.instance.fading = false;
        fadeUI.SetActive(false);
    }
}
