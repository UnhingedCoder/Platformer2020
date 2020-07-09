using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public List<float> scaleValList = new List<float>();
    public List<float> speedValList = new List<float>();
    public int index;
    public GameObject groundBurstFX;
    UnitController unit;
    PlayerMovement playerMovement;
    PlayerViewController playerView;

    private void Awake()
    {
        unit = GetComponent<UnitController>();
        playerMovement = GetComponent<PlayerMovement>();
        playerView = GetComponent<PlayerViewController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        CheckHealth();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void CheckHealth()
    {
        index = (int)unit.currentHealth - 1;

        if (index >= 0)
        {
            float scale = scaleValList[index];

            // Multiply the player's x local scale by -1.
            Vector3 theScale = new Vector3(scale, scale, scale);
            if (!playerMovement.controller.FacingRight)
            {
                theScale.x *= -1;
            }
            transform.localScale = theScale;
            playerMovement.runSpeed = speedValList[index];
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.gameObject.CompareTag("BreakableGround"))
        {
            CheckForGroundBreak(collision);
        }

        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            this.transform.SetParent(collision.transform);
        }

    }

    void CheckForGroundBreak(Collision2D collision)
    {
        if (index < 1)
            return;

        Vector2 impactVelocity = collision.relativeVelocity;
        float magnitude = Mathf.Max(0f, impactVelocity.magnitude - 1);
        if (magnitude > 10)
        {
            GameObject fx = Instantiate(groundBurstFX, new Vector3(this.transform.position.x, this.transform.position.y, 0), groundBurstFX.transform.rotation);
            fx.transform.localScale = new Vector3(1, 1, 1);
            collision.transform.gameObject.SetActive(false);
        }
        Debug.Log("Touching breaking ground");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            this.transform.SetParent(null);
        }
    }
}
