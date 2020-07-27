using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public IntValue orbCount;
    public VectorValue playerSpawnPos;
    public BoolValue doubleJumpPowerUp;

    public void ResetGame()
    {
        playerSpawnPos.runtimeValue = playerSpawnPos.initialValue;
        orbCount.RuntimeValue = orbCount.initialValue;
        doubleJumpPowerUp.RuntimeValue = doubleJumpPowerUp.initialValue;
    }

    public void AddOrbs(int val)
    {
        orbCount.RuntimeValue += val;

    }
}
