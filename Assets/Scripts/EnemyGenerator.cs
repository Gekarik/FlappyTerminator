using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private float _lowerBound;
    [SerializeField] private float _upperBound;
    [SerializeField] private ObjectPool _pool;
    [SerializeField] private ScoreCounter _scoreCounter;

    private List<Enemy> _enemiesOnScene = new List<Enemy>();

    private void Start()
    {
        StartCoroutine(GenerateEnemy());
    }

    private IEnumerator GenerateEnemy()
    {
        var wait = new WaitForSeconds(_delay);

        while (enabled) 
        {
            Spawn();
            yield return wait;
        }
    }

    private void ProcessEnemyDie(Enemy enemy)
    {
        enemy.Die -= ProcessEnemyDie;
        _enemiesOnScene.Remove(enemy);

        _pool.PutObject(enemy);
        _scoreCounter.Add();
    }

    private void Spawn()
    {
        float spawnPositionY = Random.Range(_upperBound, _lowerBound);
        Vector3 spawnPoint = new Vector3(transform.position.x, spawnPositionY, transform.position.z);

        var enemy = _pool.GetObject();
        enemy.Die += ProcessEnemyDie;
        _enemiesOnScene.Add(enemy);

        enemy.gameObject.SetActive(true);
        enemy.transform.position = spawnPoint;
    }

    public void Reset()
    {
        foreach (Enemy enemy in _enemiesOnScene)
            Destroy(enemy.gameObject);
            
        _enemiesOnScene.Clear();
        _pool.Reset();
    }
}
