using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform ItemHolder;
    public Transform throwPoint;
    public GameObject[] items;
    public enum PlayerState { MOVING, FIGHTING}
    PlayerState state;
    private CharacterController controller;
    private Animator animator;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private bool isThrowing;
    private float throwCoolDown;
    private float playerSpeed = 5.0f;
    private float gravityValue = -9.81f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        state = PlayerState.MOVING;
        isThrowing = false;
        throwCoolDown = 2.2f;
    }

    void Update()
    {
        if (state == PlayerState.MOVING)
        {
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = -0.1f;
            }

            if(Input.GetMouseButtonDown(0) && !isThrowing)
            {
                animator.SetTrigger("throw");
                playerSpeed = 0.0f;
                isThrowing = true;
            }

            if(isThrowing && throwCoolDown > 0.0f)
            {
                throwCoolDown -= Time.deltaTime;
            }
            else
            {
                isThrowing = false;
                throwCoolDown = 2.2f;
                playerSpeed = 5.0f;
            }

            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            move.Normalize();
            animator.SetFloat("velocity", move.magnitude);

            if (move != Vector3.zero)
            {
                Quaternion ToRotation = Quaternion.LookRotation(move);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, ToRotation, Time.deltaTime * 720.0f);
            }

            controller.Move(move * Time.deltaTime * playerSpeed);

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }
    }

    public void SetState(PlayerState newState)
    {
        state = newState;
    }

    public void Throw()
    {
        GameObject projectile = Instantiate(items[0], throwPoint.position, throwPoint.rotation);
        projectile.GetComponent<Rigidbody>().AddForce(transform.forward * 15.0f, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Item") && !isThrowing)
        {
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.GetComponent<MeshCollider>().enabled = false;

            other.gameObject.transform.position = ItemHolder.position;
            other.gameObject.transform.rotation = ItemHolder.rotation;

            other.gameObject.transform.parent = ItemHolder;
        }
    }
}
