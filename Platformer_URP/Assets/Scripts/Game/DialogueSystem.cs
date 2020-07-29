using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance { get; set; }
    
    public GameObject dialoguePanel;

    public string npcName;
    public List<string> dialogueLines = new List<string>();

    public Button nextButton;
    public TextMeshProUGUI dialogueText, nameText;

    int dialogueIndex;
    public int DialogueIndex { get => dialogueIndex;}

    Typewriter dialogueTypeWriter;

    private void Awake()
    {
        nextButton.onClick.AddListener(delegate { ContinueDialogue(); } );
        dialogueTypeWriter = dialogueText.GetComponent<Typewriter>();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        nextButton.gameObject.SetActive(false);
        dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (!dialogueTypeWriter.TypewriterActionCompleted)
            nextButton.gameObject.SetActive(false);
        else
            nextButton.gameObject.SetActive(true);
    }

    public void AddNewDialogue(string[] lines, string npcName)
    {
        dialogueIndex = 0;
        dialogueLines = new List<string>(lines.Length);
        dialogueLines.AddRange(lines);

        this.npcName = npcName;
        Debug.Log(dialogueLines.Count);
        CreateDialogue();
    }

    public void CreateDialogue()
    {
        //dialogueText.text = dialogueLines[dialogueIndex];
        StartCoroutine(dialogueTypeWriter.TypeActionText(dialogueLines[DialogueIndex], dialogueText));
        nameText.text = npcName;

        nextButton.gameObject.SetActive(false);
        dialoguePanel.SetActive(true);
    }

    public void ContinueDialogue()
    {
        if (DialogueIndex < dialogueLines.Count - 1)
        {
            dialogueIndex++;
            //dialogueText.text = dialogueLines[dialogueIndex];
            StartCoroutine(dialogueTypeWriter.TypeActionText(dialogueLines[DialogueIndex], dialogueText));
        }
        else
        {
            dialoguePanel.SetActive(false);
        }
    }
}
