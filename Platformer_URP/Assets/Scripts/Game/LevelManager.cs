using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public float transitionTime = 1.3f;
    public Animator anim;

    Scene currentScene;
    int currentSceneIndex;
    int nextSceneIndex;
    string currentSceneName;


    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        currentSceneIndex = currentScene.buildIndex;
        currentSceneName = currentScene.name;
        nextSceneIndex = currentSceneIndex + 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadLevel(int sceneIndex)
    {
        anim.SetTrigger("NextLevel");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }

    public void ReloadCurrentScene()
    {
        StartCoroutine(LoadLevel(currentSceneIndex));
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadLevel(nextSceneIndex));
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadLevel(0));
    }
}
