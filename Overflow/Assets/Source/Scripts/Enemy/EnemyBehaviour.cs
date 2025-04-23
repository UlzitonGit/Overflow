
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] protected Transform _player;
    [SerializeField]  protected float _durationBetweenAttacks;
    [SerializeField] protected float _rageDistance;
    protected NavMeshAgent _agent;
    public bool IsDeath;
    protected bool _isRaged;

    protected bool _canAttack = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    abstract protected bool CheckPlayerDistance();
    abstract protected IEnumerator Attack();
    
    

}
