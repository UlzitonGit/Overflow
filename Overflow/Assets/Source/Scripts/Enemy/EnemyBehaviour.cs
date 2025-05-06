using Zenject;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBehaviour : MonoBehaviour
{

    [SerializeField]  protected float _durationBetweenAttacks;
    [SerializeField] protected float _rageDistance;
    
    public Transform Player;
    public bool IsDeath;
    
    protected Transform _player => Player;
    protected NavMeshAgent _agent;
    protected bool _isRaged;
    protected bool _canAttack = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    abstract protected bool CheckPlayerDistance();
    abstract protected IEnumerator Attack();
    

}
