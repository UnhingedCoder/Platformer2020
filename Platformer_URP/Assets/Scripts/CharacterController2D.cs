using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;
    [SerializeField] private float m_DoubleJumpForce = 400f;
    [SerializeField] private float m_knockbackForce;
    [SerializeField] private float m_InvulnerabiltyDuration;
    [Range(0, 0.3f)] [SerializeField] private float m_MovementSmoothing = 0.05f;
	[Range(0, 0.5f)] [SerializeField] private float m_WallRadius = 0.2f;
	[SerializeField] private bool m_AirControl = false;
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private Transform m_GroundCheck;
	[SerializeField] private Transform m_WallCheck;

    const float k_GroundedRadius = 0.2f;
    private bool m_Grounded;
    private float m_knockbackCount;
    private bool m_Knockbacked;
    private bool m_KnockFromRight;
    private float m_invulnerabiltyTime;
    private bool m_invulnerable = false;
    private bool m_CanDoubleJump = false;
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;
    private Vector3 velocity = Vector3.zero;

    public bool Grounded { get => m_Grounded;}
    public bool KnockFromRight { get => m_KnockFromRight; set => m_KnockFromRight = value; }
    public float KnockbackCount { get => m_knockbackCount; set => m_knockbackCount = value; }
    public bool Invulnerable { get => m_invulnerable; set => m_invulnerable = value; }

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

	private void FixedUpdate()
	{
        KnocbackCheck();

		m_Grounded = false;
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
				m_Grounded = true;
		}
    }

	public void Move(float move, bool jump)
	{
        if (m_Knockbacked)
            return;

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
        }


        // If the player should jump...
        if (jump)
        {
            if (m_Grounded)
            {
                m_Rigidbody2D.AddForce(new Vector2(m_Rigidbody2D.velocity.x / 4, m_JumpForce));
                m_CanDoubleJump = true;
            }
            else
            {
                if (m_CanDoubleJump)
                {
                    m_CanDoubleJump = false;
                    m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
                    m_Rigidbody2D.AddForce(new Vector2(m_Rigidbody2D.velocity.x, m_DoubleJumpForce));
                }
            }
        }
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    public bool IsStationary()
    {
        if (m_Rigidbody2D.velocity.x == 0)
            return true;
        else
            return false;
    }

    private void KnocbackCheck()
    {
        if (m_knockbackCount > 0)
        {
            m_Knockbacked = true;
            if (m_KnockFromRight)
            {
                m_Rigidbody2D.velocity = new Vector2(-m_knockbackForce, m_knockbackForce / 2);
            }
            else
            {
                m_Rigidbody2D.velocity = new Vector2(m_knockbackForce, m_knockbackForce / 2);
            }

            m_knockbackCount -= Time.deltaTime;
        }
        else
        {
            m_Knockbacked = false;
        }

        if (m_invulnerabiltyTime > 0)
        {
            m_invulnerabiltyTime -= Time.deltaTime;
            m_invulnerable = true;
        }
        else
        {
            m_invulnerable = false;
        }
    }

    public void MakeInvulnerable()
    {
        m_invulnerable = true;
        m_invulnerabiltyTime = m_InvulnerabiltyDuration;
    }
}
