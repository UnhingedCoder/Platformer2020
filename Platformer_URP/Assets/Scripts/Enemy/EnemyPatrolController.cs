using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolController : MonoBehaviour
{
    public LayerMask groundLayer;
    public float speed;
    public float distance;
    public bool moveOnSpawn;
    private bool canMove;

    private float dir = 1;
    private bool movingRight = true;

    public Transform groundDetection;
    public Transform wallDetection;

    private Rigidbody2D _rigidBody;

    public bool CanMove { get => canMove; set => canMove = value; }
    public float Dir { get => dir;}

    private void Awake()
    {
        _rigidBody = this.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        canMove = moveOnSpawn;
    }

    private void Update()
    {
        if (!canMove)
            return;


        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance, groundLayer);
        RaycastHit2D wallInfo = Physics2D.Raycast(wallDetection.position, new Vector2(dir, 0), 0.25f, groundLayer);

        if (!groundInfo.collider || wallInfo.collider)
        {
            if (movingRight == true)
            {
                //Debug.Log("Moving left now");
                ChangeFacingDirection(-1);
            }
            else
            {
                //Debug.Log("Moving right now");
                ChangeFacingDirection(1);
            }
        }
        //Debug.Log(this.gameObject.name + " moving towards: " + dir);
        _rigidBody.velocity = new Vector2(dir * speed, _rigidBody.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            this.transform.SetParent(collision.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            this.transform.SetParent(null);
        }
    }

    public void ChangeFacingDirection(float val)
    {
        dir = val;

        transform.localScale = new Vector2(val, 1);

        if (val == 1)
            movingRight = true;
        else
            movingRight = false;
    }


}
