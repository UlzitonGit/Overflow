using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class FinishStage : MonoBehaviour
{
    private GameSessionMananger _gameSessionMananger;

    [Inject]
    private void Construct(GameSessionMananger gameSessionMananger)
    {
        _gameSessionMananger = gameSessionMananger;
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
