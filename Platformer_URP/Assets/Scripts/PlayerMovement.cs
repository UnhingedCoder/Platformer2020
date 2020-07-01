using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public CharacterController2D controller;

	public float runSpeed = 40f;
	public float dir = 1f;
	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;

	// Update is called once per frame
	void Update()
	{
		if (Input.GetAxisRaw("Horizontal") != 0)
			dir = Input.GetAxisRaw("Horizontal");

		horizontalMove = dir * runSpeed;

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
		}
	}

	void FixedUpdate()
	{
		// Move our character
		controller.Move( horizontalMove * Time.fixedDeltaTime, jump);
		jump = false;
	}

	public void ChangeDirection()
	{
		float newDir = dir > 0 ? -1f : 1f;
		dir = newDir;
	}
}
