using UnityEngine;
using Zenject;
public class InGameBindingsSettup : MonoInstaller
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private GameSessionMananger _sessionMananger;
    [SerializeField] private StageLoader _stageLoader;
    [SerializeField] private TilesMananger _tilesMananger;
    public override void InstallBindings()
    {
        Container.Bind<IPlayable>().FromInstance(_player).AsSingle();
        Container.Bind<GameSessionMananger>().FromInstance(_sessionMananger).AsSingle();
        Container.Bind<StageLoader>().FromInstance(_stageLoader).AsSingle();
        Container.Bind<TilesMananger>().FromInstance(_tilesMananger).AsSingle();
    }
}
