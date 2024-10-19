using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState { MOVING, FIGHTING}
    PlayerState state;
    private CharacterController controller;
    private Animator animator;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 5.0f;
    private float gravityValue = -9.81f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        state = PlayerState.MOVING;
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Item"))
        {
        }
    }
}
