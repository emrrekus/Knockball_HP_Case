using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShot : MonoBehaviour
{
    [SerializeField] private float firePower;
    [SerializeField] private BallObjectPooling _ballObjectPooling;
    [SerializeField] private CannonAnim _cannonAnim;


    private bool CanShoot;

    private void Awake()
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

    void Shoot()
    {
        if (!GameManager.Instance.CanShoot) return;
        var inst = _ballObjectPooling.GetBall();
        inst.transform.position = _ballObjectPooling.SpawnPoint.position;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 dir = ray.direction.normalized;

        GameManager.Instance.CurrentBall--;
        _cannonAnim.ShootAnim();
        if (inst.TryGetComponent<Ball>(out var ball))
        {
            ball.Rigibody.isKinematic = false;
            ball.Rigibody.AddForce(dir * firePower, ForceMode.Impulse);
        }
    }
}