using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public VectorValue playerSpawnPos;

    LevelManager _lvlManager;

    private void Awake()
    {
        _lvlManager = FindObjectOfType<LevelManager>();
    }

    public void OnNewGameClicked()
    {
        _lvlManager.LoadNextScene();
    }

    public void OnContinueClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level_01");
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }
}
