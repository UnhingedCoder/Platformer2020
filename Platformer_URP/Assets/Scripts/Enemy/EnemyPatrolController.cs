using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolController : MonoBehaviour
{
    public LayerMask groundLayer;
    public float speed;
    public float distance;
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
        canMove = true;
    }

    private void Update()
    {
        if (!canMove)
            return;


        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance, groundLayer);
        RaycastHit2D wallInfo = Physics2D.Raycast(wallDetection.position, new Vector2(dir, 0), 0.25f, groundLayer);

        Debug.Log(groundInfo.collider);
        if (!groundInfo.collider || wallInfo.collider)
        {
            if (movingRight == true)
            {
                dir = -1;
                transform.localScale = new Vector2(-1, 1);
                movingRight = false;
            }
            else
            {
                dir = 1;
                transform.localScale = new Vector2(1, 1);
                movingRight = true;
            }
        }

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
}
