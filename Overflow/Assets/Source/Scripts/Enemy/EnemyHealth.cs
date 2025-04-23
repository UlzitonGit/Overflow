using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
        [SerializeField] private EnemyBehaviour _enemyBehaviour;
        [SerializeField] private Animator _animator;
        private float _health = 100;

        public void TakeDamage(float damage)
        {
                _health -= damage;
                if (_health <= 0)
                {
                        _enemyBehaviour.IsDeath = true;
                        _animator.SetTrigger("Death");
                }
        }
}
