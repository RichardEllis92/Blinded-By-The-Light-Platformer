using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public Animator AnimRefObj;
    public string nextLevel;
    public Transform teleportTarget;
    public GameObject thePlayer;
    float waitTime = 4f;
    void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(WaitForFadeIn());
    }

    IEnumerator WaitForFadeIn()
    {
        AnimRefObj.Play("FadeInAnimEndOfLevel");
        thePlayer.transform.position = teleportTarget.transform.position;
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(nextLevel);
    }
}
