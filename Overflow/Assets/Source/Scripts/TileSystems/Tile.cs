using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private GameObject[] _obstacles;
    [SerializeField] private int _enemiesMinCount;
    [SerializeField] private int _obsMinCount;

    [SerializeField] public Transform NextTileSpawner;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnEnemies();
        SpawnObstacles();
    }

    private void SpawnEnemies()
    {
        int randomValue = Random.Range(_enemiesMinCount, _enemies.Length);
        for (int i = 0; i < randomValue; i++)
        {
            int randomEnemy = Random.Range(0, _enemies.Length);
            _enemies[randomEnemy].SetActive(true);
        }
    }
    private void SpawnObstacles()
    {
        int randomValue = Random.Range(_obsMinCount, _obstacles.Length);
        for (int i = 0; i < randomValue; i++)
        {
            int randomObs = Random.Range(0, _obstacles.Length);
            _obstacles[randomObs].SetActive(true);
        }
    }
}
