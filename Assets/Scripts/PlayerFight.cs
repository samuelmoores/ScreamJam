using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFight : MonoBehaviour
{
    public enum PlayerFightState { IDLE, ATTACKING, DAMAGED};
    public PlayerFightState state;
    Animator animator;
    Enemy enemy;
    float health;
    float attackCooldown;

    private void Start()
    {
        state = PlayerFightState.IDLE;
        animator = GetComponent<Animator>();
        enemy = GameObject.Find("Zombie").GetComponent<Enemy>();
        health = 1.0f;
        GetComponent<PlayerController>().enabled = false;
        attackCooldown = 3.0f;
    }
    private void Update()
    {
        Debug.Log(attackCooldown);
        if (state == PlayerFightState.ATTACKING && attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
        else
        {
            state = PlayerFightState.IDLE;
            attackCooldown = 3.0f;
        }
    }
    public void OnAttack()
    {
        if(state == PlayerFightState.IDLE)
        {
            state = PlayerFightState.ATTACKING;
            animator.SetTrigger("punch");
        }
    }

    public void Attack()
    {
        enemy.Damage();
        state = PlayerFightState.ATTACKING;
    }

    public void Damage()
    {
        animator.SetTrigger("damage");
        health -= 0.1f;
    }

    public float GetHealth()
    {
        return health;
    }
}
