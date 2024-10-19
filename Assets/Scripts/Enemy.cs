using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject item;
    public Transform itemDrop;
    Animator animator;
    PlayerFight player;
    float health;
    bool isStunned;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerFight>();
        animator = GetComponent<Animator>();
        health = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage()
    {
        health -= 0.55f;
        
        if(health <= 0.0f)
        {
            isStunned = true;
            animator.SetBool("isStunned", true);
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
        GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;

        GameObject.Find("Player").GetComponent<PlayerController>().SetState(PlayerController.PlayerState.MOVING);
        Instantiate(item, itemDrop.position, Quaternion.identity);
        Object.Destroy(gameObject);
    }
}
