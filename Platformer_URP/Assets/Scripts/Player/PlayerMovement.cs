using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public InputMaster controls;
    public CharacterController2D controller;
    public DynamicJoystick joystick;
    public float runSpeed = 40f;
    public float dir = 1f;

    public Vector2 joystickDirection;

    public Signal interactingSignal;

    float dirBeforeStop = 1f;
    float horizontalMove = 0f;
    bool m_jump = false;
    bool interactionAvailable = false;

    public bool InteractionAvailable { get => interactionAvailable; set => interactionAvailable = value; }

    private void Awake()
    {
        controls = new InputMaster();
        controls.Player.Jump.started += ctx => ActionInput();
        controls.Player.Jump.canceled += ctx => JumpStop();
        controls.Player.Movement.performed += ctx => joystickDirection = ctx.ReadValue<Vector2>();
        controls.Player.Movement.canceled += ctx => joystickDirection =Vector2.zero;
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    private void Start()
    {
        dir = 0;
    }

    // Update is called once per frame
    void Update()
    {
        dir = 0;
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        //if (Input.GetAxisRaw("Horizontal") != 0)
        dir = joystickDirection.x;

#elif UNITY_ANDROID
        joystickDirection = new Vector2(joystick.Horizontal, joystick.Vertical);
        dir = joystickDirection.x;
#endif
        
        horizontalMove = dir * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            ActionInput();
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

    public void Move(Vector2 direction)
    {
        Debug.Log(direction);
        dir = direction.x;
    }

    public void ActionInput()
    {
        if (interactionAvailable)
        {
            interactingSignal.Raise();
        }
        else
        {
            m_jump = true;
        }
    }

    public void JumpStop()
    {
        controller.BreakJump();
        Time.timeScale = 1;
        dir = 0;
        controller.DoubleJump(joystickDirection);
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
