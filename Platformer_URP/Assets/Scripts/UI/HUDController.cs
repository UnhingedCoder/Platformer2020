using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public GameObject[] healthUnits;

    private PlayerController player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        for (int i = 0; i < healthUnits.Length; i++)
        {
            healthUnits[i].SetActive(false);
        }

        for (int i = 0; i < player.unit.currentHealth; i++)
        {
            healthUnits[i].SetActive(true);
        }
    }
}
