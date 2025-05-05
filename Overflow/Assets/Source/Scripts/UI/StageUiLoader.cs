using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageUiLoader : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _stageNumberText;
    [SerializeField] private TextMeshProUGUI _stageText;

    [SerializeField] private TextMeshProUGUI _diveText;

    [SerializeField] private string _stageTextToPrint;

    [SerializeField] private GameObject _stagePanel;
    private GameSessionMananger _gameSessionMananger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(TextPrinting());
        _gameSessionMananger =FindAnyObjectByType<GameSessionMananger>();
    }

    private IEnumerator TextPrinting()
    {
        _diveText.text = "Dive " + PlayerPrefs.GetInt("Dive");
        foreach (var text in _stageTextToPrint)
        {
            yield return new WaitForSeconds(0.05f);
            _stageText.text += text;
        }

        _stageNumberText.text = _gameSessionMananger.Stage.ToString();
        yield return new WaitForSeconds(1);
        _stagePanel.SetActive(false);
    }
}
