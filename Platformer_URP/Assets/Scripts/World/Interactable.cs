using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject interactionBubble;
    public float interactionDetectionTime;

    float detectionTime;

    public bool playerNearby;

    public bool interactionStarted;

    private PlayerController player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (playerNearby)
        {
            detectionTime += Time.deltaTime;

            if (detectionTime >= interactionDetectionTime)
            {
                EnableInteraction();
            }
            else
            {
                DisableInteraction();
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerNearby = true;
            interactionStarted = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DisableInteraction();
            detectionTime = 0;
            playerNearby = false;
            interactionStarted = false;
        }
    }

    private void EnableInteraction()
    {
        interactionBubble.SetActive(true);
        player.playerMovement.InteractionAvailable = true;
    }

    private void DisableInteraction()
    {
        interactionBubble.SetActive(false);
        player.playerMovement.InteractionAvailable = false;
    }
}
