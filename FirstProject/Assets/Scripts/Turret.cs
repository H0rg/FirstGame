using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform _playerPosition;
    [SerializeField] private float _rotationSpeed = 1f;
    [SerializeField] private float _minDistance = 7f;

    void Update()
    {
        if ((Vector3.Distance(transform.position, _playerPosition.position) < _minDistance))
        {
            var newDir = Vector3.RotateTowards(transform.forward, transform.position - _playerPosition.position, _rotationSpeed * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(newDir);

        }
    }
}
