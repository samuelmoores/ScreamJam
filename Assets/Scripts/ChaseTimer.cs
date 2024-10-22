using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Threading;
using System;

public class ChaseTimer : MonoBehaviour
{
    public GameObject[] enemies;
    TextMeshProUGUI text;
    float timer;
    int numOfEnemies;
    bool tryAgain;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        timer = 30.0f;
        tryAgain = false;
    }

    // Update is called once per frame
    void Update()
    {
        numOfEnemies = enemies.Length;
        
        if (numOfEnemies == 0)
        {
            text.text = "";
        }
        else if (timer <= 0.0f)
        {
            tryAgain = true;
            gameObject.SetActive(false);
            GameObject.Find("Door").GetComponent<BoxCollider>().isTrigger = true;
            
        }
        else if(timer > 0.0f)
        {
            timer -= Time.deltaTime;
            text.text = timer.ToString("F2");
        }
    }

    public void RemoveEnemy()
    {
        numOfEnemies--;
    }

    public bool CanTryAgain()
    {
        return tryAgain;
    }

    public float GetTime()
    {
        return timer;
    }
}
