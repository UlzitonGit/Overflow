using System;
using System.Collections;
using UnityEngine;

public abstract class WeaponGeneral : MonoBehaviour
{
    [SerializeField] protected Animator _animator;
    
    [SerializeField] protected float _damage;
    [SerializeField] protected float _timeBetweenAttacks;
    
    protected bool _isReadyForAttack = true;
    
    protected abstract void Attack();
    protected abstract IEnumerator Reloading();
}
