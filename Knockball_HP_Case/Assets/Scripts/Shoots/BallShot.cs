using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShot : SingletonDerivedClasses
{
    [SerializeField] private float firePower;
    [SerializeField] private BallObjectPooling _ballObjectPooling;
    [SerializeField] private CannonAnim _cannonAnim;

    [SerializeField] private LevelController _levelController;
    private bool CanShoot;

    private void Start()
    {
        _ballObjectPooling = GetComponent<BallObjectPooling>();
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    //Both PC and mobile compatible shoot mechanics. We play shooting mechanics, animations and sounds that should play in shooting mechanics.

    void Shoot()
    {
        if (!_gameManagerInstance.CanShoot) return;

        var inst = _ballObjectPooling.GetBall();
        inst.transform.position = _ballObjectPooling.SpawnPoint.position;

        Ray ray;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        }
        else
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        Vector3 dir = ray.direction.normalized;

        _levelController.CurrentBall--;
        _cannonAnim.ShootAnim();
       

        if (inst.TryGetComponent<Ball>(out var ball))
        {
            _audioManagerInstance.PlayOneShotClip(ball.BallShotClip);
            ball.Rigibody.isKinematic = false;
            ball.Rigibody.AddForce(dir * firePower, ForceMode.Impulse);
        }
    }

    protected override void OnAwake()
    {
        
    }
}