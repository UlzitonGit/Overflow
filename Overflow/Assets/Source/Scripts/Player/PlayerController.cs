using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _parkourDuration = 0.4f;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _dashPower = 3;
    [SerializeField] private float _parkourPower = 3;
    [SerializeField] private float _dashDuration = 0.2f;
    [SerializeField] private float _dashAttackDuration = 1f;
    [SerializeField] private float _dashAttackPower = 3;
    [SerializeField] private float _dashReloadSpeed = 1;
    [SerializeField] private Transform _parkourCheckPoint;
    [SerializeField] private LayerMask _parkourLayerMask;
    private CapsuleCollider _capsuleCollider;
    private int _speedMultiply = 1;
    private bool _canDash = true;
    private Rigidbody _rb;
    private Vector2 _input;
    private Vector3 _direction;
    private bool _canParkour;
    private bool _isParkouring;
    private bool _isDashing;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>(); 
    }
    void Update()
    {
        GetInputs();
        CheckParkourObstacles();
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

        if (Input.GetKey(KeyCode.Space) && _canParkour && !_isParkouring && !_isDashing)
        {
            StartCoroutine(Parkouring());
        }
        if (Input.GetKey(KeyCode.LeftShift) && _canDash && !_isParkouring)
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

    private IEnumerator Parkouring()
    {
        _rb.linearVelocity = Vector3.zero;
        _isParkouring = true;
        _rb.useGravity = false;
        print("Parkouring");
        _capsuleCollider.isTrigger = true;
        _speedMultiply = 0;
        _rb.AddForce(transform.forward * _parkourPower, ForceMode.Impulse);
        _animator.SetBool("Vaulting", true);
        while (_canParkour)
        {
            yield return new WaitForEndOfFrame();
        }
        _animator.SetBool("Vaulting", false);
        _capsuleCollider.isTrigger = false;
        _rb.useGravity = true;
        _speedMultiply = 1;
        _isParkouring = false;
    }
    IEnumerator Dashing()
    {
        _canDash = false;
        _animator.SetBool("Dashing", true);
        _speedMultiply = 0;
        _isDashing = true;  
        yield return new WaitForSeconds(_dashDuration);
        _speedMultiply = 1;
        _isDashing = false;
        _animator.SetBool("Dashing", false);
        yield return new WaitForSeconds(_dashReloadSpeed);
        _canDash = true;
    }
    IEnumerator DashingAttack()
    {
        _canDash = false;
        _speedMultiply = 0;
        _rb.AddForce(transform.forward * _dashAttackPower, ForceMode.Impulse);
        yield return new WaitForSeconds(_dashAttackDuration);
        _speedMultiply = 1;
        yield return new WaitForSeconds(_dashReloadSpeed);
        
        _canDash = true;
    }
    public void AttackDash()
    {
        StartCoroutine(DashingAttack());
    }
  
    #endregion
    #region Parkour
    private void CheckParkourObstacles()
    {
        Collider[] _hitCollider = Physics.OverlapSphere(_parkourCheckPoint.position, 0.3f, _parkourLayerMask);
        _canParkour = _hitCollider.Length > 0;
        print(_canParkour);
    }
    #endregion
}
