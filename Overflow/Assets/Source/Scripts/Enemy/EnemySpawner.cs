using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyBehaviour[] _enemies;

    public void SpawnEnemie(Transform player)
    {
        int rand = Random.Range(0, _enemies.Length);
        _enemies[rand].gameObject.SetActive(true);
        _enemies[rand].Player = player;
    }
}
