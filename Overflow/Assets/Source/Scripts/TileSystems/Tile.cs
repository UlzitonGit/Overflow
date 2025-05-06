using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private EnemySpawner[] _enemies;
    [SerializeField] private GameObject[] _obstacles;
    [SerializeField] private int _enemiesMinCount;
    [SerializeField] private int _obsMinCount;

    [SerializeField] public Transform NextTileSpawner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void SpawnEnemies(Transform PlayerTransform)
    {
        int randomValue = Random.Range(_enemiesMinCount, _enemies.Length);
        for (int i = 0; i < randomValue; i++)
        {
            print(PlayerTransform);
            int randomEnemy = Random.Range(0, _enemies.Length);
            _enemies[randomEnemy].gameObject.SetActive(true);
            _enemies[randomEnemy].SpawnEnemie(PlayerTransform);
        }
    }
    public void SpawnObstacles()
    {
        int randomValue = Random.Range(_obsMinCount, _obstacles.Length);
        for (int i = 0; i < randomValue; i++)
        {
            int randomObs = Random.Range(0, _obstacles.Length);
            _obstacles[randomObs].SetActive(true);
        }
    }
}
