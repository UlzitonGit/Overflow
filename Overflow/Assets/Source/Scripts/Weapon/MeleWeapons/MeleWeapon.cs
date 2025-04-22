using System;
using System.Collections;
using UnityEngine;

public class MeleWeapon : WeaponGeneral
{
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
          yield return new WaitForSeconds(_timeBetweenAttacks);
          _isReadyForAttack = true;
     }
}
