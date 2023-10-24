using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMy : MonoBehaviour
{
    private Rigidbody rigidbody;

    public float _speed = 5f;
    public float _explosionTime = 5f;
    private float _maxHp = 10;
    private float _currentHp;

    public GameObject _bombPref;
    public Transform _bombStartPosition;

    public GameObject _bulletPref;
    public Transform _bulletStartPosition;
    private float _bulletDamage = 5;

    public Transform _isGroundedPosition; 

    public float force = 50;

    private Vector3 _direction;
    private Vector3 _rotation;
    [SerializeField] private float _mouseSpeed = 250f;

    [SerializeField] public bool keyOne = false;
    [SerializeField] public bool keyTwo = false;

    public const float _maxReloadTime = 0.5f;

    private bool _isReloaded = true;
    private bool _isGrounded = true;
    [SerializeField] private List<Transform> boxes;
    private List<Vector3> boxesSave = new List<Vector3>();

    private void Start()
    {
        foreach(var box in boxes)
        {
            boxesSave.Add(box.position);
        }
        rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) CreateBoom();
        if (Input.GetButtonDown("Fire1") && _isReloaded == true) Fire();
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
        if (Input.GetKeyDown(KeyCode.Y)) RestartBoxes();
        Move();
        cameraMove();
    }
    private void Reload()
    {
        _isReloaded = true;
    }

    private void Fire()
    {
        //var bullet = Instantiate(_bulletPref, _bulletStartPosition.position, transform.rotation).GetComponent<Rigidbody>();
        //bullet.AddForce(Vector3.forward * force);
        //bullet.AddTorque(Vector3.left * 80);
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
        Instantiate(_bombPref, _bombStartPosition.position, transform.rotation);
    }
    public void Move()
    {
        _direction.x = Input.GetAxis("Horizontal");
        _direction.z = Input.GetAxis("Vertical");
        var speed = _direction * _speed * Time.deltaTime;
        transform.Translate(speed);
    }
    public void cameraMove()
    {
        _rotation.y = Input.GetAxis("Mouse X");
        transform.Rotate(_rotation * _mouseSpeed);
    }
    public void TakeDamage(float damage)
    {
        if (_currentHp <= 0)
        {
            Debug.Log("YOU ARE DEAD");
        }
        _currentHp -= damage;
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

        if (rayCast) _isGrounded = true;
        else _isGrounded = false;
        
        if (_isGrounded == true) rigidbody.AddForce(transform.up * 300, ForceMode.Impulse);
    }

    public void RestartBoxes()
    {
        Debug.Log("Restart Boxes");
        int index = 0;
        foreach(var box in boxes)
        {
            Debug.Log($"Box{box.name} current position {box.transform.position} to {boxesSave[index]} position");
            box.transform.position = boxesSave[index];
            index++;
        }
    }
}
