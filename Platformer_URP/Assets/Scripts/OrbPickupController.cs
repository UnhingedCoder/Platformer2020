using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbPickupController : MonoBehaviour
{
    public int orbsToAdd;
    private SaveManager _saveManager;

    private void Awake()
    {
        _saveManager = FindObjectOfType<SaveManager>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_saveManager.orbCount.RuntimeValue < _saveManager.maxOrbs)
            {
                _saveManager.AddOrbs(orbsToAdd);
                this.gameObject.SetActive(false);
            }
        }
    }
}
