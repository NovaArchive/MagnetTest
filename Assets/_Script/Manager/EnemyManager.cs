using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private EnemyBrain enemyPrefab;
    
    [Header("Settings")]
    [SerializeField] private int enemyTargetCount;
    [SerializeField] private float spawnRange = 50f;
    
    private int _totalEnemyKilled;
    private int _totalEnemySpawn;
    
    // Start is called before the first frame update
    void Start()
    {
        text.text = $"Enemy Left: {enemyTargetCount}";
        _totalEnemyKilled = 0;
        _totalEnemySpawn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_totalEnemySpawn < enemyTargetCount)
        {
            if (NavigationExtension.GetRandomPoint(Vector3.zero, spawnRange, out Vector3 position))
            {
                Debug.Log("Enemy Spawned");
                EnemyBrain enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
                enemy.Init(this);
                _totalEnemySpawn += 1;
            }
        }
    }

    public void OnEnemyKilled()
    {
        _totalEnemyKilled += 1;
        text.text = $"Enemy Left: {enemyTargetCount - _totalEnemyKilled}";
    }

    public bool HasTargetGoalReached()
    {
        return _totalEnemyKilled == enemyTargetCount;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Vector3.zero, spawnRange);
    }
}
