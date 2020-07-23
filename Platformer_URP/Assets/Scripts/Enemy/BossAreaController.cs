using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAreaController : MonoBehaviour
{
    public Animator entryDoor;
    public Animator exitDoor;

    public EnemyBossController boss;

    private void Start()
    {
        boss.gameObject.SetActive(false);
        entryDoor.SetTrigger("Open");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!boss.gameObject.activeInHierarchy)
            {
                entryDoor.SetTrigger("Close");
                boss.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!boss.EnemyUnit.IsAlive())
            {
                entryDoor.SetTrigger("Open");
                exitDoor.SetTrigger("Open");
            }
        }
    }
}
