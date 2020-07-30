using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignboardController : Interactable
{
    public List<Dialogue> dialogueList = new List<Dialogue>();
    //public string[] dialogue;
    public string name;

    int dialogueBlockIndex;
    private void Start()
    {
        dialogueBlockIndex = 0;
    }

    public void ShowSignboardInfo()
    {
        if (!playerNearby || interactionStarted)
            return;

        DialogueSystem.Instance.AddNewDialogue(dialogueList[dialogueBlockIndex].dialogue, name);
        Debug.Log("Interacting with NPC");
        if (dialogueBlockIndex < dialogueList.Count - 1)
        {
            dialogueBlockIndex++;
        }
        interactionStarted = true;
    }
}
