using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;
    [SerializeField] private float m_DoubleJumpForce = 400f;
    [SerializeField] private float m_JumpBufferLength;
    [SerializeField] private float m_HangTime;
    [SerializeField] private float m_knockbackForce;
    [SerializeField] private float m_StompBounce;
    [SerializeField] private float m_InvulnerabiltyDuration;
    [Range(0, 0.3f)] [SerializeField] private float m_MovementSmoothing = 0.05f;
	[Range(0, 0.5f)] [SerializeField] private float m_WallRadius = 0.2f;
	[SerializeField] private bool m_AirControl = false;
    [SerializeField] private BoolValue m_AllowDoubleJump;
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private Transform m_GroundCheck;
	[SerializeField] private Transform m_WallCheck;
    [SerializeField] private ParticleSystem ps_footsteps;
    [SerializeField] private ParticleSystem ps_impactDust;
    [SerializeField] private ParticleSystem ps_doubleJumpBlast;
    [SerializeField] private ParticleSystem ps_waterImpact;
    [SerializeField] private ParticleSystem ps_poisonImpact;
    private ParticleSystem.EmissionModule footstepsModule;

    const float k_GroundedRadius = 0.33f;
    private bool m_jump; 
    private float m_jumpScale = 1f;
    private float m_hangCounter;
    private float m_jumpBufferCount;
    private bool m_InWater = false;
    private bool m_Grounded;
    private float m_knockbackCount;
    private bool m_Knockbacked;
    private bool m_KnockFromRight;
    private float m_invulnerabiltyTime;
    private bool m_invulnerable = false;
    private bool m_CanDoubleJump = false;
    private bool m_DoingDoubleJump = false;
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;
    private bool m_FacingUp = true;
    private bool m_canMove = false;
    private Vector3 velocity = Vector3.zero;

    public bool Grounded { get => m_Grounded;}
    public bool KnockFromRight { get => m_KnockFromRight; set => m_KnockFromRight = value; }
    public float KnockbackCount { get => m_knockbackCount; set => m_knockbackCount = value; }
    public bool Invulnerable { get => m_invulnerable; set => m_invulnerable = value; }
    public bool CanMove { get => m_canMove; set => m_canMove = value; }
    public bool FacingRight { get => m_FacingRight; set => m_FacingRight = value; }
    public bool CanDoubleJump { get => m_CanDoubleJump; set => m_CanDoubleJump = value; }

    public UnityEvent e_OnJump;
    public UnityEvent e_OnLanding;
    public UnityEvent e_OnHurt;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        footstepsModule = ps_footsteps.emission;
    }

    private void Update()
    {
        //Check for hangtime
        if (m_Grounded)
        {
            m_hangCounter = m_HangTime;
        }
        else
        {
            m_hangCounter -= Time.deltaTime;
        }

        //Check for jump buffer
        if (m_jump)
        {
            m_jumpBufferCount = m_JumpBufferLength;
        }
        else
        {
            m_knockbackCount -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
	{
        KnockbackCheck();

		m_Grounded = false;
        if (!m_InWater)
        {
            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
            }
        }
        else
        {
            m_Grounded = true;
        }
    }

	public void Move(float move, bool jump)
	{
        m_jump = jump;

        if (!m_canMove)
        {
            m_Rigidbody2D.velocity = Vector2.zero;
            footstepsModule.rateOverTime = 0f;
            return;
        }

        if (m_Knockbacked)
            return;

        //Footsteps dust trail FX
        if (move != 0 && m_Grounded && !m_InWater)
        {
            footstepsModule.rateOverTime = 35f;
        }
        else
        {
            footstepsModule.rateOverTime = 0f;
        }

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
				FlipHorizontal();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				FlipHorizontal();
			}
        }


        // If the player should jump...
        if (jump)
        {
            if (m_hangCounter > 0 && m_jumpBufferCount >= 0)
            {
                m_Rigidbody2D.AddForce(new Vector2(m_Rigidbody2D.velocity.x / 4, m_JumpForce * m_jumpScale));

                if(m_AllowDoubleJump.RuntimeValue)
                    CanDoubleJump = true;

                m_jumpBufferCount = 0;

                e_OnJump.Invoke();
            }
            else
            {
                if (CanDoubleJump)
                {
                    Time.timeScale = 0f;
                    m_DoingDoubleJump = true;
                }
            }
        }
	}

    public void DoubleJump(Vector2 dir)
    {
        if (m_DoingDoubleJump)
        {
            m_Rigidbody2D.velocity = new Vector2(0, 0);
            float jumpForce = (float)m_DoubleJumpForce * 1.2f;
            if (dir.x <= 0.5 && dir.x >= -0.5)
                jumpForce = (float)m_DoubleJumpForce/1.15f;

            m_Rigidbody2D.AddForce(new Vector2(dir.x * jumpForce, dir.y * jumpForce));

            ps_doubleJumpBlast.gameObject.SetActive(true);
            ps_doubleJumpBlast.Stop();
            ps_doubleJumpBlast.Play();

            CanDoubleJump = false;
            m_DoingDoubleJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            if (!m_InWater)
            {
                ps_waterImpact.gameObject.SetActive(true);
                ps_waterImpact.transform.position = new Vector2(collision.ClosestPoint(this.transform.position).x, collision.ClosestPoint(this.transform.position).y);
                ps_waterImpact.Stop();
                ps_waterImpact.Play();
            }
            m_InWater = true;
            Debug.Log("Stepped into water");
        }
        else if (collision.gameObject.CompareTag("Poison"))
        {
                ps_poisonImpact.gameObject.SetActive(true);
                ps_poisonImpact.transform.position = new Vector2(collision.ClosestPoint(this.transform.position).x, collision.ClosestPoint(this.transform.position).y);
                ps_poisonImpact.Stop();
                ps_poisonImpact.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            m_InWater = false;
            Debug.Log("Stepped out of water");
        }
    }

    public void BreakJump()
    {
        if (m_Rigidbody2D.velocity.y > 0)
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.y * 0.6f);

    }


	private void FlipHorizontal()
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

    private void KnockbackCheck()
    {
        if (m_knockbackCount > 0)
        {
            Debug.Log("Got knockbacked");
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

    public void SetCharacterMass(float val)
    {
        m_Rigidbody2D.mass = val;
    }

    public void MakeInvulnerable()
    {
        m_invulnerable = true;
        m_invulnerabiltyTime = m_InvulnerabiltyDuration;
        e_OnHurt.Invoke();
    }

    public void Stomp()
    {
        Debug.Log("Stomping");
        m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_StompBounce);
    }

    public void OnGroundImpact()
    {
        ps_impactDust.gameObject.SetActive(true);
        ps_impactDust.Stop();
        ps_impactDust.Play();
        e_OnLanding.Invoke();
    }
}
