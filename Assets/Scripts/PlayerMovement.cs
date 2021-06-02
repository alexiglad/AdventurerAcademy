using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private FloatValueSO moveSpeed;
    [SerializeField] private Rigidbody2D playerRidgidbody;

    private Vector2 moveDirection;

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    /// <summary>
    /// Physics Calculations go in FixedUpdate so it's not reliant on the framerate
    /// </summary>
    void FixedUpdate()
    {
        /*switch (GameController.controller.GetWorldState())
        {
            case WorldState.roam:
                Move(); break;
            default:
                Stop(); break;
        }*/

        Move();
    }

    void ProcessInputs() 
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move() 
    {
        playerRidgidbody.velocity = new Vector2(moveDirection.x * moveSpeed.GetFloatValue(),
            moveDirection.y * moveSpeed.GetFloatValue());
    }

    void Stop()
    {
        playerRidgidbody.velocity = new Vector2(0, 0);
    }
}
