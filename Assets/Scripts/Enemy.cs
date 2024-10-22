using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject item;
    public Transform itemDrop;
    public GameObject selector;
    Animator animator;
    PlayerFight player;
    GameManager manager;
    float health;
    bool isStunned;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerFight>();
        animator = GetComponent<Animator>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        health = 1.0f;
        DeSelect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage()
    {
        health -= 1.55f;
        
        if(health <= 0.0f)
        {
            isStunned = true;
            animator.SetBool("isStunned", true);
            DeSelect();
        }
        else
        {
            animator.SetTrigger("damage");

        }
        

    }

    public void Attack()
    {
        player.Damage();
    }

    public float GetHealth()
    {
        return health;
    }

    public void Destroy()
    {
        manager.numOfEnemies--;
        Instantiate(item, itemDrop.position, Quaternion.identity);
        Object.Destroy(gameObject);
    }

    public void Select()
    {
        selector.SetActive(true);
    }

    public void DeSelect()
    {
        selector.SetActive(false);
    }

}
