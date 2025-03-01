using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    [SerializeField] private ObjectPool _pool;
    [SerializeField] private int _maxCount = 10;
    [SerializeField] private float _minDistance = 2f;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float baseRadius = 5f;

    private float _currentCount = 0;

    private float _positionY = 0.5f;
    private float _minMapRangeX = -50f;
    private float _maxMapRangeX = 50f;
    private float _minMapRangeZ = -50f;
    private float _maxMapRangeZ = 50f;

    private Vector3 basePosition;

    private WaitForSeconds _delay;

    private List<Vector3> _spawnedPositions;

    private void Awake()
    {
        _spawnedPositions = new List<Vector3>();

        basePosition = Vector3.zero;

        _delay = new WaitForSeconds(spawnInterval);
    }

    private void Start()
    {
        StartCoroutine(GenerateObjects());
    }

    private IEnumerator GenerateObjects()
    {
        while (_currentCount < _maxCount)
        {
            Vector3 randomPosition = GetRandomPositionExcludingBase();

            if (IsTooCloseOthers(randomPosition))
            {
                var resource = _pool.GetObject();
                resource.transform.position = randomPosition;
                _spawnedPositions.Add(randomPosition);
                
                _currentCount++;

                yield return _delay;
            }
        }
    }

    private Vector3 GetRandomPositionExcludingBase()
    {
        Vector3 randomPosition;

        do
        {
            randomPosition = new Vector3(
                Random.Range(_minMapRangeX, _maxMapRangeX),
                _positionY,
                Random.Range(_minMapRangeZ, _maxMapRangeZ));            
        } while (IsInsideBaseArea(randomPosition));

        return randomPosition;
    }

    private bool IsInsideBaseArea(Vector3 position)
    {
        return Vector3.Distance(position, basePosition) <= baseRadius;
    }

    private bool IsTooCloseOthers(Vector3 position)
    {
        foreach (Vector3 spawnedPosition in _spawnedPositions)
            if (Vector3.Distance(position, spawnedPosition) < _minDistance)
                return false;

        return true;
    }
}
