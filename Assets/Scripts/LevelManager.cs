using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public int currentFood;
    public GameObject foodUI;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        EnableFoodUI();
    }

    public void GetFood(int amount)
    {
        currentFood += amount;
        AudioManager.instance.PlaySFX(3);
        //UIController.instance.foodText.text = currentFood.ToString();
        //UIController.instance.UpdateFoodText();
    }

    public void EnableFoodUI()
    {
        if (currentFood > 0)
        {
            foodUI.SetActive(true);
        }
        else
        {
            foodUI.SetActive(false);
        }
    }
}
