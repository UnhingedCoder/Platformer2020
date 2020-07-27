using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUIController : MonoBehaviour
{
    public Text coordinatesText;

    private PlayerController player;

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
        coordinatesText.text = "X: " + player.playerMovement.joystickDirection.x.ToString("0.00") +
                                "\nY: " + player.playerMovement.joystickDirection.y.ToString("0.00") +
                                "\nM: " + Vector2.SqrMagnitude(player.playerMovement.joystickDirection).ToString("0.00");
    }
}
