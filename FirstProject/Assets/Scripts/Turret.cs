using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform _playerPosition;
    [SerializeField] private float _rotationSpeed = 1f;
    [SerializeField] private float _minDistance = 7f;
    private float _nextTime = 0;
    private float _interval = 1;

    [SerializeField] private GameObject _prepTurretBullet;
    [SerializeField] private Transform _TurretBulletPosition;

    void Update()
    {
        if ((Vector3.Distance(transform.position, _playerPosition.position) < _minDistance))
        {
            var newDir = Vector3.RotateTowards(transform.forward, _playerPosition.position - transform.position,
                                                _rotationSpeed * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(newDir);

            if(Time.time >= _nextTime)
            {
                Fire();
                _nextTime += _interval;
            }
        }
    }

    private void Fire()
    {
        Instantiate(_prepTurretBullet, _TurretBulletPosition.position, transform.rotation);
    }
}
