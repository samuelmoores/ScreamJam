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
        health = 1.0f;
        GetComponent<PlayerController>().enabled = false;
        attackCooldown = 3.0f;
        enemy = null;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            // Check if the ray hits an object
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit object is tagged as "Enemy"
                if (hit.collider.CompareTag("Enemy"))
                {
                    // Get the Enemy component from the hit object
                    Enemy enemy = hit.collider.GetComponent<Enemy>();

                    // If the enemy is not null, call the SetEnemy function
                    if (enemy != null)
                    {
                        SetEnemy(enemy);
                    }
                }
            }
        }

        if (state == PlayerFightState.ATTACKING && attackCooldown > 0)
        {
            Debug.Log(attackCooldown);
            attackCooldown -= Time.deltaTime;
        }
        else
        {
            state = PlayerFightState.IDLE;
            attackCooldown = 3.0f;
        }
    }

    public void SetEnemy(Enemy enemyToAttack)
    {
        Debug.Log("enemy set to" + enemyToAttack);
        enemy = enemyToAttack;
    }

    public void OnAttack()
    {
        if(state == PlayerFightState.IDLE && enemy != null)
        {
            state = PlayerFightState.ATTACKING;
            animator.SetTrigger("punch");
        }
    }

    public void Attack()
    {
        enemy.Damage();
        state = PlayerFightState.ATTACKING;
        enemy = null;
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
