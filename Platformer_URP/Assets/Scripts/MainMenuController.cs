using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public VectorValue playerSpawnPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
