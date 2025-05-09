using System;
using UnityEngine;

public class GameSessionMananger : MonoBehaviour
{
    public int Stage;

    public void InitializeStageNumber()
    {
        Stage = PlayerPrefs.GetInt("Stage");
    }
    public void AddStage()
    {
        Stage = PlayerPrefs.GetInt("Stage");
        Stage++;
        PlayerPrefs.SetInt("Stage", Stage);
    }
}
