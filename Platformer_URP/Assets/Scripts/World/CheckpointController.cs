using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    private SaveManager _saveManager;
    private LevelManager _lvlManager;

    private void Awake()
    {
        _lvlManager = FindObjectOfType<LevelManager>();
        _saveManager = FindObjectOfType<SaveManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _lvlManager.SavePositionAtThisLevel();
            _saveManager.SaveGame();
        }

    }
}
