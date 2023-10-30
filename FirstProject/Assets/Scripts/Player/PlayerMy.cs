using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMy : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody rigidbody;
    [Header("Health Point")]
    private float _maxHp = 10;
    [SerializeField] public float _currentHp;
    
    [SerializeField] private float _boostDuration = 5;
    [SerializeField] private float _boostPower = 8f;

    [Header("Bomb")]
    [SerializeField] private GameObject _bombPref;
    [SerializeField] private Transform _bombStartPosition;
    private float _throwForce = 1.2f;

    [Header("Bullet")]
    [SerializeField] private GameObject _bulletPref;
    [SerializeField] private Transform _bulletStartPosition;
    private float _bulletDamage = 5;

    [Header("Bullet")]
    [SerializeField] private GameObject _minePref;
    [SerializeField] private Transform _mineStartPosition;

    [Header("Jump")]
    [SerializeField] private Transform _isGroundedPosition;
    [SerializeField] private float _jumpForce = 450;
    private bool _isGrounded = true;

    [Header("Movement and Mouse Rotation")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _mouseSpeed = 250f;
    private Vector3 _direction;
    private Vector3 _rotation;

    [Header("Keys")]
    public bool keyOne = false;
    public bool keyTwo = false;
    
    [Header("Reload")]
    private const float _maxReloadTime = 0.5f;
    private bool _isReloaded = true;

    [Header("Boxes Saver")]
    [SerializeField] private GameObject boxes;
    private List<Vector3> boxesPositionSave = new List<Vector3>();
    private List<Quaternion> boxesRotationSave = new List<Quaternion>();
    
   


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _currentHp = _maxHp;
        rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
       
    }
    private void Start()
    {
        BoxesSave();
       
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) 
            CreateBoom();
        
        if (Input.GetButtonDown("Fire1") && _isReloaded == true) 
            Fire();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            
        }
        
        
        if (Input.GetKeyDown(KeyCode.Y)) 
            RestartBoxes();
        
        if (Input.GetKeyDown(KeyCode.G)) 
            CreateMine();
        
        Move();
        cameraMove();
    }

    public void ColaMove()
    {
        StartCoroutine(CocaCola());
    }
    private IEnumerator CocaCola()
    {
        float elapsedTime = 0;
        float _speedSave = _speed;
        Debug.Log($"START BOOST !");
        while (elapsedTime < _boostDuration)
        {
            Debug.Log($"BOOSTING");
            _speed = _boostPower;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _speed = _speedSave;
        Debug.Log($"END BOOST ! Speed = {_speed}");
    }
    
    
    
    private void Reload()
    {
        _isReloaded = true;
    }
    private void Fire()
    {
        // var bullet = Instantiate(_bulletPref, _bulletStartPosition.position, transform.rotation).GetComponent<Rigidbody>();
        // bullet.AddForce(Vector3.forward * force);
        // bullet.AddTorque(Vector3.left * force);
        RaycastHit hit;
        var rayCast = Physics.Raycast(_bulletStartPosition.position, transform.forward, out hit, 100);
        if (rayCast)
        {
            Debug.Log($"{hit.collider.gameObject.name} <<");
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                hit.collider.gameObject.GetComponent<Enemy>().TakeDamage(_bulletDamage);
            }
        }
        _isReloaded = false;
        Invoke("Reload", _maxReloadTime);
    }
    private void CreateBoom()
    {
        var bomb = Instantiate(_bombPref, _bombStartPosition.position, Quaternion.identity).GetComponent<Rigidbody>();
        bomb.AddForce(_bombStartPosition.forward * _throwForce, ForceMode.Impulse);
    }
    private void CreateMine()
    {
        var bomb = Instantiate(_minePref, _mineStartPosition.position, Quaternion.identity);
    }
    public void Move()
    {
        _direction.x = Input.GetAxis("Horizontal");
        _direction.z = Input.GetAxis("Vertical");
        var speed = _direction * _speed * Time.deltaTime;
        if (speed != Vector3.zero)
            _animator.SetBool("Go", true);
        else
            _animator.SetBool("Go", false);
        transform.Translate(speed);
    }
    public void cameraMove()
    {
        _rotation.y = Input.GetAxis("Mouse X");
        transform.Rotate(_rotation * _mouseSpeed);
    }
    public void Jump()
    {
        Debug.Log("JUMP");
        
        RaycastHit hit;
        var rayCast = Physics.Raycast(_isGroundedPosition.position, Vector3.down, out hit, 0.2f);

        if (rayCast)
            _isGrounded = true;
        else _isGrounded = false;
        
        if (_isGrounded)
        {
            rigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
        }

    }
    public void TakeDamage(float damage)
    {
        _currentHp -= damage;
        if (_currentHp <= 0)
        {
            _animator.SetTrigger("Death" );
            Debug.Log("YOU ARE DEAD");
        }
    }
    public void Heal(float heal)
    {
        if (_currentHp + heal > _maxHp)
            _currentHp = _maxHp;
        else
            _currentHp += heal;
    }

    public void RestartBoxes()
    {
        int index = 0;
        int childNumber = boxes.transform.childCount;
        for (int i = 0; i < childNumber; i++)
        {
            var box = boxes.transform.GetChild(i).gameObject.GetComponent<Transform>();
            box.GetComponent<Rigidbody>().velocity = Vector3.zero;
            box.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            box.transform.position = boxesPositionSave[index];
            box.rotation = boxesRotationSave[index];
            index++;
        }
    }

    public void BoxesSave()
    {
        int childNumber = boxes.transform.childCount;
        for(int i = 0; i < childNumber; i++)
        {
            var box = boxes.transform.GetChild(i).gameObject.GetComponent<Transform>();
            boxesPositionSave.Add(box.position);
            boxesRotationSave.Add(box.rotation);
        }
    }
}
