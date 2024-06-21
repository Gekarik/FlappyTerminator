using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _bulletSpeed = 5f;
    [SerializeField] private float _delayBeforeShooting = 1f;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Transform _target;
    [SerializeField] private ObjectPool _pool;

    private List<Bullet> _bulletsOnScene = new List<Bullet>();
    private Coroutine _shootingCoroutine;

    private void OnEnable()
    {
        if (TryGetComponent(out Enemy enemy))
            _shootingCoroutine = StartCoroutine(nameof(SpawnBullets));
    }

    private void OnDisable()
    {
        if (_shootingCoroutine != null)
        {
            StopCoroutine(_shootingCoroutine);
            _shootingCoroutine = null;
        }
    }

    private IEnumerator SpawnBullets()
    {
        var wait = new WaitForSeconds(_delayBeforeShooting);

        while (true)
        {
            Shoot();
            yield return wait;
        }
    }

    public void Shoot()
    {
        var direction = new Vector2(_target.position.x - _shootPoint.position.x, _target.position.y - _shootPoint.position.y).normalized;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
        var bullet = ObjectPool.Instance.GetBulletObject(_shootPoint.position, rotation, transform.parent);

        bullet.SpeedInitialize(_bulletSpeed, direction);
    }
}
