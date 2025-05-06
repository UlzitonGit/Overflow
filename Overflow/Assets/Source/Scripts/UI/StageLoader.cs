using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class StageLoader : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _stageNumberText;
    [SerializeField] private TextMeshProUGUI _stageText;

    [SerializeField] private TextMeshProUGUI _diveText;

    [SerializeField] private string _stageTextToPrint;

    [SerializeField] private GameObject _stagePanel;
    private GameSessionMananger _gameSessionMananger;
    IPlayable _playable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Inject]
    private void Construct(GameSessionMananger gameSessionMananger, IPlayable playable)
    {
        _gameSessionMananger = gameSessionMananger;
        _playable = playable;
    }
    public void InitializeStageCountdown()
    {
        StartCoroutine(StageInitializer());
    }

    private IEnumerator StageInitializer()
    {
        _playable.IsActive(false);
        _diveText.text = "Dive " + PlayerPrefs.GetInt("Dive");
        foreach (var text in _stageTextToPrint)
        {
            yield return new WaitForSeconds(0.05f);
            _stageText.text += text;
        }

        _stageNumberText.text = _gameSessionMananger.Stage.ToString();
        yield return new WaitForSeconds(1);
        _playable.IsActive(true);
        _playable.InitializePlayer();
        _stagePanel.SetActive(false);
    }
}
