using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMenu : MonoBehaviour
{
    public GameObject FightMenu;
    public GameObject AttackMenu;


    // Start is called before the first frame update
    void Start()
    {
        FightMenu.SetActive(true);
        AttackMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Back()
    {
        Debug.Log("Back");

        FightMenu.SetActive(true);
        AttackMenu.SetActive(false);
    }
}
