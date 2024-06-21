using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Enemy _prefab;
    [SerializeField] private Bullet _bulletPrefab;

    private static ObjectPool _instance;

    private Queue<Enemy> _enemyPool;
    private Queue<Bullet> _bulletPool;

    public IEnumerable<Enemy> PooledObjects => _enemyPool;

    private void Awake()
    {
        _enemyPool = new Queue<Enemy>();
        _bulletPool = new Queue<Bullet>();
    }

    public static ObjectPool Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<ObjectPool>();

            return _instance;
        }
    }

    public Enemy GetObject()
    {
        if (_enemyPool.Count == 0)
        {
            var enemy = Instantiate(_prefab);
            enemy.transform.parent = _container;

            return enemy;
        }

        return _enemyPool.Dequeue();
    }

    public Bullet GetBulletObject(Vector3 _shootPointPosition, Quaternion rotation, Transform parrent)
    {
        if (_bulletPool.Count == 0)
        {
            var bullet = Instantiate(_bulletPrefab, _shootPointPosition, rotation);
            bullet.transform.parent = parrent;

            return bullet;
        }

        return _bulletPool.Dequeue();
    }

    public void PutObject(Enemy enemy)
    {
        _enemyPool.Enqueue(enemy);
        enemy.gameObject.SetActive(false);
    }

    public void Reset()
    {
        _enemyPool.Clear();
        _bulletPool.Clear();
    }
}
