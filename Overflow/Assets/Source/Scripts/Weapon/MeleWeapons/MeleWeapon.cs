using System;
using System.Collections;
using UnityEngine;

public class MeleWeapon : WeaponGeneral
{
     [SerializeField] private float _attackingTime = 0.2f;
     [SerializeField] private BoxCollider _hitCollider;
     [SerializeField] private ParticleSystem _hitParticles;
     private void Update()
     {
          Inputs();
     }

     private void Inputs()
     {
          if (Input.GetMouseButton(0) && _isReadyForAttack)
          {
               Attack();
          }
     }
     

     protected override void Attack()
     {
          _animator.SetTrigger("Attack");
          StartCoroutine(Reloading());
     }

     protected override IEnumerator Reloading()
     {
          _isReadyForAttack = false;
          _hitCollider.enabled = true;
          yield return new WaitForSeconds(_attackingTime);
          _hitCollider.enabled = false;
          yield return new WaitForSeconds(_timeBetweenAttacks);
          _isReadyForAttack = true;
     }

     private void OnTriggerEnter(Collider other)
     {
          if (other.CompareTag("Door"))
          {
               other.GetComponent<Door>().GetImpulse(_attackPoint.transform.forward);
          }
          if (other.CompareTag("Enemy"))
          {
               other.GetComponent<EnemyHealth>().TakeDamage(_damage);
               _hitParticles.Play();
          }
     }
}
