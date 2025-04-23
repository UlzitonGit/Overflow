using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float _impulseSpeed = 3;
    [SerializeField] private Transform[] _parts;
    [SerializeField] private Rigidbody[] _partsRb;
    private bool _isBroken;
    private Rigidbody _rb;
    private bool _isHited = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void GetImpulse(Vector3 dir)
    {
        if(_isHited) return;
        _isHited = true;
        _rb.isKinematic = false;
        _rb.AddForce(dir * _impulseSpeed, ForceMode.Impulse);
        StartCoroutine(Breaking());
    }

    private IEnumerator  Breaking()
    {
        
        yield return new WaitForSeconds(0.5f);
        _isBroken = true;
        foreach (var part in _partsRb)
        {
            yield return new WaitForSeconds(0.1f);
            part.isKinematic = false;
            part.transform.parent = null;
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.GetComponent<EnemyHealth>() != null && !_isBroken)
        {
            other.transform.GetComponent<EnemyHealth>().TakeDamage(100);
            _isBroken = true;
        }
    }
}
