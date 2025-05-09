using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class FinishStage : MonoBehaviour
{
    private GameSessionMananger _gameSessionMananger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _gameSessionMananger = FindAnyObjectByType<GameSessionMananger>();
            _gameSessionMananger.AddStage();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            this.enabled = false;
        }
    }
}
