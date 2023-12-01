using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BallObjectPooling : MonoBehaviour
{
    [SerializeField] private GameObject _defaultBall;
    public static IObjectPool<GameObject> _ballPool;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _objectPoolParent;

    public Transform SpawnPoint => _spawnPoint;

    private void Awake()
    {
        _ballPool = new ObjectPool<GameObject>(CreatePoolBall, OnGetPoolBall, OnReleasePoolObject, OnDestroyFromPool);
    }

    private void OnDestroyFromPool(GameObject obj)
    {
        Destroy(obj);
    }

    private void OnReleasePoolObject(GameObject obj)
    {
        if (obj.TryGetComponent<Ball>(out var ball))
        {
            ball.enabled = false;
        }
    }

    private void OnGetPoolBall(GameObject obj)
    {
        if (obj.TryGetComponent<Ball>(out var ball))
        {
            ball.enabled = true;
            ball.ResetSpawnTime();
        }
    }

    private GameObject CreatePoolBall()
    {   
     
        var inst = Instantiate(_defaultBall, _spawnPoint.transform.position, Quaternion.identity);
        
        inst.transform.parent = _objectPoolParent?.transform;
        

        if (inst.TryGetComponent<Ball>(out var ball))
        {
            ball.DestroyRequested += () => { _ballPool.Release(inst); };
        }
        
        return inst;
    }

    public GameObject GetBall()
    {
        return _ballPool.Get();
    }
}