using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFight : MonoBehaviour
{
    public enum PlayerFightState { IDLE, ATTACKING, DAMAGED};
    public PlayerFightState state;
    public GameObject Explosion;
    public GameObject AttackMenu;
    public GameObject FightMenu;
    public GameObject ErrorMessage;

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
        AttackMenu.SetActive(false);
        ErrorMessage.SetActive(false);

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
                    if (enemy != null)
                    {
                        enemy.DeSelect();
                    }

                    // Get the Enemy component from the hit object
                    enemy = hit.collider.GetComponent<Enemy>();

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
        enemy = enemyToAttack;
        enemy.Select();
        ErrorMessage.SetActive(false);

    }

    public void OnAttack()
    {
        if(state == PlayerFightState.IDLE && enemy != null)
        {
            state = PlayerFightState.ATTACKING;
            animator.SetTrigger("punch");
        }
        else
        {
            ErrorMessage.SetActive(true);

        }
    }

    public void OpenAttackMenu()
    {
        FightMenu.SetActive(false);
        AttackMenu.SetActive(true);

    }

    public void Attack()
    {
        enemy.Damage();
        Explosion.GetComponent<ParticleSystem>().Play();
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
