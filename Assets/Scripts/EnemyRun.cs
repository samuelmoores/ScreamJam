using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRun : MonoBehaviour
{
    public GameObject TimerGameObject;
    ChaseTimer chaseTimer;
    NavMeshAgent agent;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        chaseTimer = TimerGameObject.GetComponent<ChaseTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("velocity", agent.velocity.magnitude);

        if(!agent.hasPath)
        {
            agent.destination = new Vector3(Random.Range(-40, 40), transform.position.y, Random.Range(-40, 40));

        }
        else if(chaseTimer.GetTime() <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            agent.isStopped = true;
            animator.SetBool("isCaught", true);
            chaseTimer.RemoveEnemy();
        }
    }
}
