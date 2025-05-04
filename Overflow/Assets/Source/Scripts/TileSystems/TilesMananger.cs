using Unity.AI.Navigation;
using UnityEngine;

public class TilesMananger : MonoBehaviour
{
    [SerializeField] private GameObject[] _tiles;
    [SerializeField] private Transform _firsTileSpawnTransform;
    [SerializeField] private int _tilesCount;
    [SerializeField] private NavMeshSurface _navMeshSurface;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Tile currenTile = Instantiate(_tiles[Random.Range(0, _tiles.Length)], _firsTileSpawnTransform).GetComponent<Tile>();
        for (int i = 0; i < _tilesCount; i++)
        {
            Tile prevTile = currenTile;
            currenTile = Instantiate(_tiles[Random.Range(0, _tiles.Length)], prevTile.NextTileSpawner.position, Quaternion.Euler(new Vector3(0, 180, 0))).GetComponent<Tile>();
            currenTile.transform.parent = null;
        }

        _navMeshSurface.BuildNavMesh();
    }
    
}
