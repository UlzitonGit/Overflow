using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishStage : MonoBehaviour
{
    private GameSessionMananger _gameSessionMananger;

    private void Start()
    {
        _gameSessionMananger =FindAnyObjectByType<GameSessionMananger>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _gameSessionMananger.Stage++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            this.enabled = false;
        }
    }
}
