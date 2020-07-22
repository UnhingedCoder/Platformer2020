using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealAreaController : MonoBehaviour
{
    public SpriteRenderer sprRndr;

    private void OnEnable()
    {
        sprRndr.color = new Color(sprRndr.color.r, sprRndr.color.g, sprRndr.color.b, 1);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            sprRndr.color  = new Color(sprRndr.color.r, sprRndr.color.g, sprRndr.color.b, 0);
        }
    }
}
