using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class PlayerMy : MonoBehaviour
{
    public float _speed = 5f;
    public float _explosionTime = 5f;
    private float _maxHp = 10;
    private float _currentHp;

    public GameObject _bombPref;
    public Transform _bombStartPosition;

    public GameObject _bulletPref;
    public Transform _bulletStartPosition;

    private Vector3 _direction;
    private Transform _enemyPosition;
    private Vector3 _rotation;
    [SerializeField] private float _mouseSpeed = 250f;

    [SerializeField] public bool keyOne = false;
    [SerializeField] public bool keyTwo = false;

    public const float _maxReloadTime = 0.2f;
    private float _currentReloadTime = 0;
    private float _currentTimeFromLastShot = 0;


    private void Awake()
    {
        _enemyPosition = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            CreateBoom();
        
        _currentReloadTime += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && _currentReloadTime >= _currentTimeFromLastShot)
        {
            CreateBullet();
            _currentTimeFromLastShot = _currentReloadTime + _maxReloadTime;
            Debug.Log($"_currentTimeFromLastShot [{_currentTimeFromLastShot}]   - _currentReloadTime[{_currentReloadTime}]");
        }
        Move();
        cameraMove();

    }

    private void CreateBullet()
    {
        GameObject bullet = Instantiate(_bulletPref, _bulletStartPosition.position, transform.rotation);
    }
    private void CreateBoom()
    {
        Instantiate(_bombPref, _bombStartPosition.position, transform.rotation)
                                .GetComponent<Bomb>().Init(_explosionTime);
        
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
        if(_currentHp <= 0)
        {
            Debug.Log("YOU ARE DEAD");
        }
        _currentHp -= damage;
    }
    public void Heal(float heal)
    {
        if(_currentHp + heal > _maxHp)
            _currentHp = _maxHp;
        else 
            _currentHp += heal;
    }
}
