using System;
using System.Collections;
using UnityEngine;

public class MeleWeapon : WeaponGeneral
{
     [SerializeField] private float _attackingTime = 0.2f;
     [SerializeField] private Transform _hitPoint;
     [SerializeField] private LayerMask _enemyLayerMask;
     [SerializeField] private LayerMask _obstLayerMask;
     [SerializeField] PlayerController _player;
     private bool _isAttacking;
     private void Update()
     {
          Attack();
          if(_isAttacking) CheckHits();
     }
     
     

     protected override void Attack()
     {
          if (Input.GetMouseButton(0) && _isReadyForAttack)
          {
               _animator.SetTrigger("Attack");
               StartCoroutine(Reloading(0.5f));
          }
          if (Input.GetMouseButton(1) && _isReadyForAttack)
          {
               _animator.SetTrigger("DashAttack");
               StartCoroutine(Reloading(0));
               _player.AttackDash();
          }
     }

     protected override IEnumerator Reloading(float durationBeforeAttack)
     {
          _isReadyForAttack = false;
          yield return new WaitForSeconds(_attackingTime * durationBeforeAttack);
          _isAttacking = true;
          yield return new WaitForSeconds(_attackingTime);
          _isAttacking = false;
          yield return new WaitForSeconds(_timeBetweenAttacks);
          _isReadyForAttack = true;
     }

     private void CheckHits()
     {
          Collider[] _hitCollider = Physics.OverlapSphere(_hitPoint.position, 1, _enemyLayerMask);
          if (_hitCollider.Length > 0)
          {
               foreach (var enemy in _hitCollider)
               {
                    enemy.GetComponent<EnemyHealth>().TakeDamage(_damage);
               }
          }
          _hitCollider = Physics.OverlapSphere(_hitPoint.position, 1, _obstLayerMask);
          if (_hitCollider.Length > 0)
          {
               foreach (var obs in _hitCollider)
               {
                    obs.GetComponent<Door>().GetImpulse(_hitPoint.forward);
               }
          }
     }
    
}
