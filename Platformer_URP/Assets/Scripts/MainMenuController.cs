using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public VectorValue playerSpawnPos;

    public void OnNewGameClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level_01");
        playerSpawnPos.runtimeValue = playerSpawnPos.initialValue;
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }
}
