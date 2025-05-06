using Unity.AI.Navigation;
using UnityEngine;
using Zenject;

public class TilesMananger : MonoBehaviour
{
    [SerializeField] private GameObject[] _tiles;
    [SerializeField] private Transform _firsTileSpawnTransform;
    [SerializeField] private NavMeshSurface _navMeshSurface;
    [SerializeField] private GameObject _lastTile;
    
    [SerializeField] private int _tilesCount;
    
    private Transform _playerTransform;
    
    [Inject]
    private void Construct(IPlayable player)
    {
        _playerTransform = player.PlayerTransform();
        print(_playerTransform);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void InitializeTiles()
    {
        Tile currenTile = Instantiate(_tiles[Random.Range(0, _tiles.Length)], _firsTileSpawnTransform).GetComponent<Tile>();
        currenTile.SpawnObstacles();
        currenTile.SpawnEnemies(_playerTransform);
        currenTile.transform.parent = null;
        for (int i = 0; i < _tilesCount; i++)
        {
            Tile prevTile = currenTile;
            currenTile = SpawnTile(prevTile);
        }

        Instantiate(_lastTile, currenTile.NextTileSpawner.position, Quaternion.Euler(new Vector3(0, 180, 0)));
        _navMeshSurface.BuildNavMesh();
    }

    private Tile SpawnTile(Tile prevTile)
    {
        Tile  currenTile = Instantiate(_tiles[Random.Range(0, _tiles.Length)], prevTile.NextTileSpawner.position, Quaternion.Euler(new Vector3(0, 180, 0))).GetComponent<Tile>();
        currenTile.SpawnObstacles();
        currenTile.SpawnEnemies(_playerTransform);
        currenTile.transform.parent = null;
        return currenTile;
    }
    
}
