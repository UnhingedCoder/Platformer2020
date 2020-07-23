using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveManager : MonoBehaviour
{
    public IntValue orbCount;
    public VectorValue playerSpawnPos;

    public float maxOrbs;

    public Image saveBar;
    public GameObject indicator;

    public TextMeshProUGUI saveInfo;
    private PlayerController player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();    
    }

    private void Start()
    {
        saveBar.fillAmount = (float)orbCount.RuntimeValue / maxOrbs;
        saveInfo.text = "";
    }

    private void Update()
    {
        saveBar.fillAmount = (float)orbCount.RuntimeValue / maxOrbs;

        if (indicator != null)
        {
            if (orbCount.RuntimeValue >= maxOrbs)
                indicator.SetActive(true);
            else
                indicator.SetActive(false);
        }
    }

    public void AddOrbs(int val)
    {
        orbCount.RuntimeValue += val;

    }

    public void OnSaveClicked()
    {
        if (orbCount.RuntimeValue >= maxOrbs)
        {
            if (!player.playerMovement.controller.Grounded)
            {
                StopCoroutine(ShowGameInfo("Not on ground", 2f));
                StartCoroutine(ShowGameInfo("Not on  ground", 2f));
                return;
            }
            player.unit.Heal(3f);
            orbCount.RuntimeValue = 0;
            playerSpawnPos.runtimeValue = player.transform.position;
            StopCoroutine(ShowGameInfo("Game saved", 2f));
            StartCoroutine(ShowGameInfo("Game saved", 2f));
        }
        else
        {
            StopCoroutine(ShowGameInfo("Collect more orbs", 3f));
            StartCoroutine(ShowGameInfo("Collect more orbs", 3f));
        }
    }

    public void ResetGame()
    {
        playerSpawnPos.runtimeValue = Vector3.zero;
        orbCount.RuntimeValue = 0;
    }

    IEnumerator ShowGameInfo(string infoText, float waitTime)
    {
        saveInfo.text = infoText;
        yield return new WaitForSeconds(waitTime);
        saveInfo.text = "";
    }

}
