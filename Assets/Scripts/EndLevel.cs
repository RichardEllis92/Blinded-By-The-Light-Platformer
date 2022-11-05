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
    public GameObject fadeUI;

    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Scene scene = SceneManager.GetActiveScene();
        string sceneName = scene.name;

        if (sceneName == "Level 1")
        {
            if (other.transform.localScale.x < 0)
            {
                other.transform.localScale = new Vector2(other.transform.localScale.x * -1, other.transform.localScale.y);
            }
            StartCoroutine(WaitForFadeInLevel1());
        }
        else
        {
            other.transform.position = teleportTarget.transform.position;
            if(other.transform.localScale.x < 0)
            {
                other.transform.localScale = new Vector2(other.transform.localScale.x * -1, other.transform.localScale.y);
            }
            StartCoroutine(WaitForFadeInNotLevel1());
        }
        
    }

    IEnumerator WaitForFadeInLevel1()
    {
        fadeUI.SetActive(true);
        AnimRefObj.Play("FadeInAnimEndOfLevel");
        thePlayer.transform.position = teleportTarget.transform.position;
        yield return new WaitForSeconds(waitTime);
        fadeUI.SetActive(false);
        SceneManager.LoadScene(nextLevel);
    }

    IEnumerator WaitForFadeInNotLevel1()
    {
        fadeUI.SetActive(true);
        AnimRefObj.Play("FadeInAnimEndOfLevel");
        yield return new WaitForSeconds(waitTime);
        fadeUI.SetActive(false);
        SceneManager.LoadScene(nextLevel);
    }
}
