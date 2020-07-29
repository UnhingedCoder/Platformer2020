using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Typewriter : MonoBehaviour
{
    public float speed;
    bool typewriterActionCompleted = false;

    public bool TypewriterActionCompleted { get => typewriterActionCompleted; set => typewriterActionCompleted = value; }

    private void Start()
    {
        typewriterActionCompleted = false;
    }
    public IEnumerator TypeActionText(string textToDisplay, TextMeshProUGUI displayField)
    {
        ResetActionText(displayField);
        foreach (char letter in textToDisplay.ToCharArray())
        {
            displayField.text += letter;
            yield return new WaitForSeconds(speed);
        }
        typewriterActionCompleted = true;
    }

    public void ResetActionText(TextMeshProUGUI displayField)
    {
        displayField.text = "";
        typewriterActionCompleted = false;
    }
}
