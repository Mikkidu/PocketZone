using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Vector2 _sizeCol = Vector2.one * 3;
    [SerializeField] private int _numberEnemies = 3;
    [SerializeField] private float _leftBound;
    [SerializeField] private float _rightBound;
    [SerializeField] private float _upBound;
    [SerializeField] private float _downBound;

    private Collider2D[] _colliders = new Collider2D[1];

    void Start()
    {
        int endWhile;
        Vector2 spawnPos;
        for (int i = 0; i < _numberEnemies; i += 1)
        {
            endWhile = 0;
            do
            {
                endWhile += 1;
                spawnPos = new Vector3(Random.Range(_leftBound, _rightBound), Random.Range(_downBound, _upBound));
            }
            while (CheckSpawnPos(spawnPos) && endWhile < 5);
            Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
        }
    }

    bool CheckSpawnPos(Vector2 pos)
    {
        Physics2D.OverlapBox(pos, _sizeCol, 0, new ContactFilter2D(), _colliders);
        if (_colliders.Length > 0) return true;
        else return false;
    }
}
