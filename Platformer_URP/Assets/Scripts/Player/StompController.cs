using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StompController : MonoBehaviour
{
    private PlayerController player;
    public UnityEvent e_OnStomp;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();    
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyHead"))
        {
            if (!player.playerMovement.controller.Invulnerable)
            {
                GameObject fx = Instantiate(collision.transform.parent.GetComponent<KnockbackController>().burstFX.gameObject, new Vector3(this.transform.position.x, this.transform.position.y, 0), collision.transform.parent.GetComponent<KnockbackController>().burstFX.transform.rotation);
                fx.transform.localScale = new Vector3(1, 1, 1);
                e_OnStomp.Invoke();
                Destroy(collision.transform.parent.gameObject);
            }
        }
    }
}
