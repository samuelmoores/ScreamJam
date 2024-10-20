using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int levelNumber;
    public int numOfEnemies;
    bool enemiesCleared;
    bool enemiesCaught;

    // Start is called before the first frame update
    void Start()
    {
        enemiesCleared = false;   
    }

    // Update is called once per frame
    void Update()
    {
        if (numOfEnemies == 0 && !enemiesCleared)
        {
            GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;
            GameObject.Find("Canvas").gameObject.SetActive(false);
            enemiesCleared = true;
        }

    }

    
}
