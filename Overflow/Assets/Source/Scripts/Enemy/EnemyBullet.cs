using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody _rigidbody;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        _rigidbody.MovePosition(transform.position + transform.forward * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int dive = PlayerPrefs.GetInt("Dive");
            dive++;
            PlayerPrefs.SetInt("Dive", dive);
            PlayerPrefs.SetInt("Stage", 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (other.CompareTag("Obstacle") || other.CompareTag("Door"))
        {
            Destroy(gameObject);
        }
    }
}
