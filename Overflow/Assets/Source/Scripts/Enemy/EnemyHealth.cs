using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
        [SerializeField] private EnemyBehaviour _enemyBehaviour;
        [SerializeField] private ParticleSystem _hitParticles;
        [SerializeField] private Animator _animator;
        [SerializeField] private CapsuleCollider _capsuleCollider;
        private float _health = 100;
        private bool _isDead;
        
        public void TakeDamage(float damage)
        {
                if(_isDead) return;
                _health -= damage;
                if (_health <= 0)
                {
                        _capsuleCollider.enabled = false;
                        _isDead = true;
                        _hitParticles.Play();
                        _enemyBehaviour.IsDeath = true;
                        _animator.SetTrigger("Death");
                }
        }
}
