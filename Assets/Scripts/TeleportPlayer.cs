using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public Transform teleportTarget;
    public GameObject thePlayer;
    float waitTime = 5f;
    public Animator AnimRefObj;

    void OnTriggerEnter2D(Collider2D other)
    {
        //AnimRefObj.Play("FadeOutAnim");
        thePlayer.transform.position = teleportTarget.transform.position;
        AnimRefObj.Play("FadeInAnim");
        //AnimRefObj.Play("NoFade");
    }

    IEnumerator WaitForFadeIn()
    {
        yield return new WaitForSeconds(waitTime);
    }
}
