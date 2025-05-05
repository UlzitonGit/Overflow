using System;
using UnityEngine;

public class GameSessionMananger : MonoBehaviour
{
    public int Stage;

    private void Start()
    {
        DontDestroyOnLoad(gameObject.transform);
    }
}
