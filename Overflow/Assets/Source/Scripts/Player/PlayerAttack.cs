using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private Transform _card;
    [SerializeField] private Transform _abilityCard;
    [SerializeField] private Transform _shootPos;
    private Transform _nextCard;
    private bool _canAttack = true;
    private bool _canSpell = true;
    private int _comboCount = 0;  

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && _canAttack)
        {
            StartCoroutine(Attack());
        }
        if (Input.GetKey(KeyCode.Mouse1) && _canSpell && _canAttack)
        {
            StartCoroutine(Spell());
        }
    }
    public void SpawnCard()
    {
        Instantiate(_nextCard, _shootPos.position, _shootPos.rotation);
    }
    IEnumerator Attack()
    {
        _canAttack = false;
        _anim.SetTrigger("Attack");
        _anim.SetInteger("Combo", _comboCount);
        _nextCard = _card;
        yield return new WaitForSeconds(0.5f);
        _canAttack = true;
        _comboCount++;
        if (_comboCount > 2) _comboCount = 0;
    }
    IEnumerator Spell()
    {
        _canAttack = false;
        _canSpell = false;
        _anim.SetTrigger("Attack");
        _anim.SetInteger("Combo", _comboCount);
        _nextCard = _abilityCard;
        yield return new WaitForSeconds(0.5f);
        _canAttack = true;
        yield return new WaitForSeconds(7f);
        _canSpell = true;
        _comboCount++;
        if (_comboCount > 2) _comboCount = 0;
    }
}
