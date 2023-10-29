using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMy : MonoBehaviour
{
    private Rigidbody rigidbody;

    public float _speed = 5f;
    public float _explosionTime = 5f;
    private float _maxHp = 10;
    [SerializeField] public float _currentHp;

    public GameObject _bombPref;
    public Transform _bombStartPosition;
    private float _throwForce = 1.2f;

    public GameObject _bulletPref;
    public Transform _bulletStartPosition;
    private float _bulletDamage = 5;

    public GameObject _minePref;
    public Transform _mineStartPosition;

    public Transform _isGroundedPosition; 

    public float force = 50;
    private float _jumpForce = 450;

    private Vector3 _direction;
    private Vector3 _rotation;
    [SerializeField] private float _mouseSpeed = 250f;

    [SerializeField] public bool keyOne = false;
    [SerializeField] public bool keyTwo = false;

    public const float _maxReloadTime = 0.5f;

    private bool _isReloaded = true;
    private bool _isGrounded = true;

    [SerializeField] private GameObject boxes;
    private List<Vector3> boxesPositionSave = new List<Vector3>();
    private List<Quaternion> boxesRotationSave = new List<Quaternion>();

    private Animator _animator;

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
            Jump();
        
        if (Input.GetKeyDown(KeyCode.Y)) 
            RestartBoxes();
        
        if (Input.GetKeyDown(KeyCode.G)) 
            CreateMine();
        
        Move();
        cameraMove();
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
