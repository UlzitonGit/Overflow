using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IPlayable
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
    [SerializeField] private LayerMask _playerParkourLayerMask;
    [SerializeField] private LayerMask _playerLayerMask;
    public PlayerController PlayerControllerBind { get; set; }
    private CharacterController _characterController;
    private bool _isActive;
    private CapsuleCollider _capsuleCollider;
    private int _speedMultiply = 1;
    private bool _canDash = true;
    private Vector2 _input;
    private Vector3 _direction;
    private bool _canParkour;
    private bool _isParkouring;
    private bool _isDashing;
    private bool _isDashingAttacking;
    public void InitializePlayer()
    {
        _characterController = GetComponent<CharacterController>();
        PlayerControllerBind = this;
        _capsuleCollider = GetComponent<CapsuleCollider>(); 
    }

    Transform IPlayable.PlayerTransform()
    {
        return transform;
    }

    public void IsActive(bool isActive)
    {
        _isActive = isActive;
    }
    
    void Update()
    {
        if(!_isActive) return;
        GetInputs();
        CheckParkourObstacles();
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
        Vector3 moveDirection = new Vector3(_direction.x, -6, _direction.z);
        if (_isDashingAttacking) _characterController.Move(transform.forward * _dashAttackPower  * Time.deltaTime);
        if(_isDashing)  _characterController.Move(moveDirection * _dashPower * Time.deltaTime);
        if (_speedMultiply == 0) return;
        //_rb.linearVelocity = moveDirection * _speedMultiply;
        _characterController.Move(moveDirection * _speed * Time.deltaTime);
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
        StartCoroutine(Dashing());
    }

    private IEnumerator Parkouring()
    {
        _isParkouring = true;
        print("Parkouring");
        _capsuleCollider.isTrigger = true;
        _speedMultiply = 0;
        _animator.SetBool("Vaulting", true);
        gameObject.layer = LayerMask.NameToLayer("PlayerParkour");
        while (_canParkour)
        {
            _characterController.Move(transform.forward  * _parkourPower * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        gameObject.layer = LayerMask.NameToLayer("Player");
        _animator.SetBool("Vaulting", false);
        _capsuleCollider.isTrigger = false;
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
        _isDashingAttacking = true;
        yield return new WaitForSeconds(_dashAttackDuration);
        _isDashingAttacking = false;
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
