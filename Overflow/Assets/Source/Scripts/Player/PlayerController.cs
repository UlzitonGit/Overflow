using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _dashParticle;
    [SerializeField] private float _dashPower = 3;
    [SerializeField] private float _dashDuration = 0.2f;
    [SerializeField] private float _dashReloadSpeed = 1;
    private int _speedMultiply = 1;
    private bool _canDash = true;
    private Rigidbody _rb;
    private Vector2 _input;
    private Vector3 _direction;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        GetInputs();
    }
    void FixedUpdate()
    {     
        Moving();
    }
    #region Inputs
    private void GetInputs()
    {
        _input =new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _direction = Quaternion.Euler(0, Camera.allCameras[0].transform.eulerAngles.y, 0) * new Vector3(_input.x, 0, _input.y);
    }
    #endregion
    #region Movement
    private void Moving()
    {
        if (_speedMultiply == 0) return;
        Vector3 moveDirection = new Vector3(_direction.x * _speed, _rb.linearVelocity.y, _direction.z * _speed);
        _rb.linearVelocity = moveDirection * _speedMultiply;
        if (Physics.Raycast(Camera.allCameras[0].ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        {
            Vector3 diff = hit.point - transform.position;
            diff.Normalize();
            float rot = Mathf.Atan2(diff.x, diff.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, rot, 0), Time.deltaTime * _rotationSpeed);
            _direction = transform.InverseTransformDirection(_direction);

        }
        if (Input.GetKey(KeyCode.LeftShift) && _canDash)
        {
            Dash(moveDirection);
        }
        _animator.SetFloat("Input", _input.magnitude, 0.1f, Time.deltaTime);
        _animator.SetFloat("Hor", _direction.x, 0.2f, Time.deltaTime);
        _animator.SetFloat("Ver", _direction.z, 0.2f, Time.deltaTime);
    }
    private void Dash(Vector3 dashDirection)
    {
        _rb.AddForce(dashDirection * _dashPower, ForceMode.Impulse);
        StartCoroutine(Dashing());
    }
    IEnumerator Dashing()
    {
        _canDash = false;
        _animator.SetTrigger("Dash");
        _speedMultiply = 0;
        _dashParticle.Play();
        yield return new WaitForSeconds(_dashDuration);
        _speedMultiply = 1;
        yield return new WaitForSeconds(_dashReloadSpeed);
        
        _canDash = true;
    }
    #endregion
}
