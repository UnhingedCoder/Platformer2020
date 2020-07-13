using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public DynamicJoystick joystick;
    public float runSpeed = 40f;
    public float dir = 1f;

    float dirBeforeStop = 1f;
    float horizontalMove = 0f;
    bool m_jump = false;

    private void Start()
    {
        dir = 0;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        //if (Input.GetAxisRaw("Horizontal") != 0)
            dir = Input.GetAxisRaw("Horizontal");

#elif UNITY_ANDROID
        //if (joystick.Horizontal != 0)
            dir = joystick.Horizontal;
#endif

        horizontalMove = dir * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetButtonUp("Jump"))
        {
            JumpStop();
        }

    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, m_jump);
        m_jump = false;
    }

    public void ChangeDirection()
    {
        float newDir = dir > 0 ? -1f : 1f;
        dir = newDir;
    }

    public void FaceTowardsDirection(float newDir)
    {
        dir = newDir;
    }

    public void Jump()
    {
        m_jump = true;
    }

    public void JumpStop()
    {
        controller.BreakJump();
    }

    public void Stop()
    {
        if (dir != 0)
        {
            dirBeforeStop = dir;
            dir = 0;
        }
        else
        {
            dir = dirBeforeStop;
        }
    }

}
