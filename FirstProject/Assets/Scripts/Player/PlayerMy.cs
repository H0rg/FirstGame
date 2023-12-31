using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerMy : MonoBehaviour
{
    private Slider slrHP;
    private Slider slrReload;
    private GameObject hud;
    private TMP_Text textReload;

    private Animator _animator;
    private Rigidbody rigidbody;
    
    [Header("Health Point")] 
    private float _maxHp = 10;
    [SerializeField] public float _currentHp;

    [SerializeField] private float _boostDuration = 5;
    [SerializeField] private float _boostPower = 8f;

    // [Header("Bomb")] 
    // [SerializeField] private GameObject _bombPref;
    // [SerializeField] private Transform _bombStartPosition;
    // private float _throwForce = 1.2f;

    [Header("Bullet")] 
    [SerializeField] private GameObject _bulletPref;
    [SerializeField] private Transform _bulletStartPosition;
    [SerializeField] private float _bulletDamage = 5;

    // [Header("Bullet")] 
    // [SerializeField] private GameObject _minePref;
    // [SerializeField] private Transform _mineStartPosition;

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
    private int maxBulletsInMag = 10;
    private int currentBulletsInMag = 0;
    private const float _maxReloadTime = 3f;
    private bool _isReloaded = true;

    [Header("Audio")] 
    private AudioSource _audioSource;

    private Light _light;

    private void Awake()
    {
        hud = GameObject.FindGameObjectWithTag("HUD");
        slrHP = hud.transform.Find("HealthBar").GetComponent<Slider>();
        slrReload = hud.transform.Find("ReloadBar").GetComponent<Slider>();
        textReload = hud.transform.Find("ReloadText").GetComponent<TMP_Text>();

        _audioSource = GetComponent<AudioSource>();
            
        _animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;

        slrReload.maxValue = _maxReloadTime;
        slrReload.value = 0;

        
        slrHP.maxValue = _maxHp;
        _currentHp = _maxHp;
        slrHP.value = _currentHp;

        currentBulletsInMag = maxBulletsInMag;
        textReload.text = $"{currentBulletsInMag} / {maxBulletsInMag} ";

        _light = GetComponentInChildren<Camera>().GetComponentInChildren<Light>();

    }
    void Update()
    {
        if (!PauseMenu.GameIsPaused)
        {

            if (Input.GetButtonDown("Fire1") && _isReloaded)
                Fire();
            
            if (!_isReloaded)
                slrReload.value += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.R))
            {
                Reload();
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
                Jump();

            if (Input.GetKeyDown(KeyCode.F))
            {
                _light.enabled = OffOn(_light.enabled);
                Debug.Log("TurnOffOn");
            }
            

            Move();
            cameraMove();
        }
    }

    public void ColaBoost()
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

    private bool OffOn(bool value)
    {
        Debug.Log("Clakcs");
        return !value;
    }

    private void Reload()
    {
        textReload.text = "Reload";
        slrReload.value = 0;
        _isReloaded = false;
        Invoke("Reloading", _maxReloadTime);
    }
    private void Reloading()
    {
        currentBulletsInMag = maxBulletsInMag;
        textReload.text = $"{currentBulletsInMag} / {maxBulletsInMag} ";
        _isReloaded = true;
    }

    private void Fire()
    {
        // var bullet = Instantiate(_bulletPref, _bulletStartPosition.position, transform.rotation).GetComponent<Rigidbody>();
        // bullet.AddForce(Vector3.forward * force);
        // bullet.AddTorque(Vector3.left * force);
        
        //_audioSource.Play();
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

        currentBulletsInMag--;
        textReload.text = $"{currentBulletsInMag} / {maxBulletsInMag} ";
        if (currentBulletsInMag == 0)
            Reload();
    }
    
    public void Move()
    {
        _direction.x = Input.GetAxis("Horizontal");
        _direction.z = Input.GetAxis("Vertical");
        var speed = _direction * _speed * Time.deltaTime;
        // if (speed != Vector3.zero)
        //     _animator.SetBool("Go", true);
        // else
        //     _animator.SetBool("Go", false);
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
            Debug.Log("YOU ARE DEAD");
        }
        slrHP.value = _currentHp;
    }

    public void Heal(float heal)
    {
        if (_currentHp + heal > _maxHp)
            _currentHp = _maxHp;
        else
            _currentHp += heal;
        slrHP.value = _currentHp;
    }
}
