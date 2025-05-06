using UnityEngine;

public class ObstaclePicker : MonoBehaviour
{
    [SerializeField] private GameObject[] _obstacles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
            int rand = Random.Range(0, _obstacles.Length);
            _obstacles[rand].SetActive(true);
    }

  
}
