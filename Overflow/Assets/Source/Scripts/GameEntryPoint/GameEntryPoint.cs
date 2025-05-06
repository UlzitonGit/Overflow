using System;
using UnityEngine;
using Zenject;

public class GameEntryPoint : MonoBehaviour
{
    private GameSessionMananger _sessionMananger;
    private StageLoader _stageLoader;
    private TilesMananger _tilesMananger;
    [Inject]
    private void Construct(GameSessionMananger gameSessionMananger, TilesMananger tilesMananger, StageLoader stageLoader)
    {
        _sessionMananger = gameSessionMananger;
        _stageLoader = stageLoader;
        _tilesMananger = tilesMananger;
    }

    private void Start()
    {
        _tilesMananger.InitializeTiles();
        _stageLoader.InitializeStageCountdown();
    }
}
