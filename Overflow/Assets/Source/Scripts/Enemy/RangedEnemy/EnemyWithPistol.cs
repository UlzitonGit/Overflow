using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWithPistol : EnemyBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletSpawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _player = FindAnyObjectByType<PlayerController>().GetComponent<Transform>();
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(IsDeath) return;
        _isRaged = CheckPlayerDistance();
        if (_isRaged)
        {
            transform.LookAt(new Vector3(_player.position.x, transform.position.y, _player.position.z));
            
        }
        if (_isRaged && _canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    protected override IEnumerator Attack()
    {
        print("Shoot");
        _canAttack = false;
        Instantiate(_bullet, _bulletSpawn.position, _bulletSpawn.rotation);
        yield return new WaitForSeconds(_durationBetweenAttacks);
        _canAttack = true;
    }

    protected override bool CheckPlayerDistance()
    {
        return Vector3.Distance(_agent.transform.position, _player.position) < _rageDistance;
    }
}
