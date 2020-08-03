using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    LevelManager _lvlManager;
    public bool loadNextLvl;
    private void Awake()
    {
        _lvlManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (loadNextLvl)
            {
                _lvlManager.SetPostionForNextLevel();
                _lvlManager.LoadNextScene();
            }
            else
            {
                _lvlManager.SetPostionForPrevLevel();
                _lvlManager.LoadPrevScene();
            }
        }
    }
}
